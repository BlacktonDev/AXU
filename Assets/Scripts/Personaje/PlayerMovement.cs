﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	private float horizontal;
	public float speed = 8f;
	public float jumpingPower = 16f;
	private bool isFacingRight = true;
	
	public float cooldownTime = 0.1f;
	private float cooldownTimer = 0f;

	public int vidaMaxima = 3; // Valor máximo de vida
	public int vida;
	public GameObject menuDerrota;
	public Transform spawnPoint; // Punto de reaparición del jugador

	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private GameObject projectilePrefab;
	public float projectileSpeed = 10f;
	public float projectileLifetime = 2f;
	
	private Vector2 posicionGuardada;

	public bool recibiendodano = false;
	private float damageAnimationDuration = 1f; // Duración de la animación de recibir daño en segundos
	private float damageAnimationTimer = 0f; // Temporizador para controlar la duración de la animación de recibir daño

	private bool isAttacking = false;
	private float attackAnimationDuration = 1.5f; // Duración de la animación de ataque en segundos
	private float attackAnimationTimer = 0f; // Temporizador para controlar la duración de la animación de ataque

	private Animator animator;

	private bool isJumping = false;
	private bool isGrounded = false;
	private int jumpsRemaining = 0; // Cantidad de saltos restantes
	private bool doubleJumpUnlocked = false; // Indica si el doble salto está desbloqueado

	public int damageBase = 1; // Daño base
	public int currentDamage; // Daño actual

	// Power ups
	public bool PUVida = false;
	public bool PUDobleSalto = false;
	public bool PUDobleDanio = false;
	public GameObject objetoAMover;
	
	public ControladorDatosJuego controlador;
	
	
	private void Start()
	{
		vida = vidaMaxima; // Asignar vida máxima al iniciar el juego
		animator = GetComponent<Animator>();
		currentDamage = damageBase; // Inicializar el daño actual con el daño base
		controlador = FindObjectOfType<ControladorDatosJuego>();
	}

	private void Update()
	{
		horizontal = Input.GetAxisRaw("Horizontal");

		isGrounded = IsGrounded();

		if (isGrounded)
		{
			jumpsRemaining = 1; // Restablecer la cantidad de saltos restantes cuando toca el suelo
		}

		if (Input.GetButtonDown("Jump"))
		{
			if (jumpsRemaining > 0)
			{
				isJumping = true;
				jumpsRemaining--;
			}
			else if (doubleJumpUnlocked && PUDobleSalto)
			{
				isJumping = true;
				jumpsRemaining--;
			}
		}
		
		if (cooldownTimer > 0)
		{
			cooldownTimer -= Time.deltaTime;
		}
		
		if (Input.GetKeyDown(KeyCode.F) && cooldownTimer <= 0f)
		{
			isAttacking = true;
			attackAnimationTimer = 0f;
			LaunchProjectile();

			// Establecer el tiempo de enfriamiento
			cooldownTimer = cooldownTime;
		}

		if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
		{
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
		}
		animator.SetFloat("X", Mathf.Abs(horizontal));
		animator.SetBool("saltando", isJumping);
		animator.SetBool("lastimado", recibiendodano);
		if (recibiendodano)
		{
			damageAnimationTimer += Time.deltaTime;
			if (damageAnimationTimer >= damageAnimationDuration)
			{
				recibiendodano = false;
			}
		}
		animator.SetBool("atacando", isAttacking);
		if (isAttacking)
		{
			attackAnimationTimer += Time.deltaTime;
			if (attackAnimationTimer >= attackAnimationDuration)
			{
				isAttacking = false;
			}
		}

		Flip();
	}

	private void FixedUpdate()
	{
		rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

		if (isJumping)
		{
			rb.AddForce(new Vector2(0f, jumpingPower), ForceMode2D.Impulse);
			isJumping = false;
			animator.SetBool("saltando", isJumping);
		}
	}

	private bool IsGrounded()
	{
		Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, groundLayer);
		return colliders.Length > 0;
	}

	private void Flip()
	{
		if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
		{
			isFacingRight = !isFacingRight;
			Vector3 localScale = transform.localScale;
			localScale.x *= -1f;
			transform.localScale = localScale;
		}
	}

	private void LaunchProjectile()
	{
		Vector3 spawnPosition = transform.position + new Vector3(isFacingRight ? 1f : -1f, 0f, 0f);
		GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
		Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
		projectileRb.velocity = new Vector2(isFacingRight ? projectileSpeed : -projectileSpeed, 0f);
		Destroy(projectile, projectileLifetime);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.collider.CompareTag("Enemy"))
		{
			if (collision.contacts[0].normal.y > 0.5f)
			{
				rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
			}
			PerderVida(1);
		}
		else if (collision.collider.CompareTag("Muerte"))
		{
			PerderVida(vida); // Pierde todos los puntos de vida al colisionar con una barrera de "Muerte"
		}

		if (collision.gameObject.tag == "Plataforma")
		{
			Debug.Log("Toca plataforma");
			transform.SetParent(collision.transform);
		}


	}

	private void PerderVida(int cantidad)
	{
		vida -= cantidad * currentDamage;
		recibiendodano = true;

		if (vida <= 0)
		{
			Muerte();
		}
	}

	private void Muerte()
	{

		// Reiniciar nivel
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name);

		// Reiniciar vida máxima
		vida = vidaMaxima;
		transform.position = posicionGuardada;
		
		if (objetoAMover != null)
		{
			Vector2 posicionObjetoAMover = objetoAMover.transform.position;
			posicionObjetoAMover = posicionGuardada;
			objetoAMover.transform.position = posicionObjetoAMover;
		}
		
		/*
		if (menuDerrota != null)
		{
			menuDerrota.SetActive(true);
		}
		*/
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Plataforma")
		{
			transform.parent = null;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("PUVida"))
		{
			PUVida = true;
			vidaMaxima = vidaMaxima + 2;
			vida = vidaMaxima;
		}
		if (collision.CompareTag("PUDobleDanio"))
		{
			PUDobleDanio = true;
			currentDamage = currentDamage *2;
		}
		if (collision.CompareTag("Control"))
		{
			posicionGuardada = transform.position;
			Debug.Log("Posición guardada: " + posicionGuardada);
		}
	}


}
