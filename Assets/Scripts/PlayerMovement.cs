using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	private float horizontal;
	public float speed = 8f;
	public float jumpingPower = 16f;
	private bool isFacingRight = true;

	public int vidaMaxima = 3; // Valor máximo de vida
	private int vida;
	public GameObject menuDerrota;
	public Transform spawnPoint; // Punto de reaparición del jugador

	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private Transform groundCheck;
	[SerializeField] private LayerMask groundLayer;
	[SerializeField] private GameObject projectilePrefab;
	public float projectileSpeed = 10f;
	public float projectileLifetime = 2f;

	private bool isJumping = false;

	private void Start()
	{
		vida = vidaMaxima; // Asignar vida máxima al iniciar el juego
	}

	private void Update()
	{
		horizontal = Input.GetAxisRaw("Horizontal");

		if (Input.GetButtonDown("Jump") && IsGrounded())
		{
			isJumping = true;
		}

		if (Input.GetKeyDown(KeyCode.F))
		{
			LaunchProjectile();
		}

		if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
		{
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
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
			else
			{
				PerderVida(1); // Pierde 1 punto de vida al colisionar con un enemigo
			}
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
		vida -= cantidad;

		if (vida <= 0)
		{
			Muerte();
		}
	}

	private void Muerte()
	{
		// Reiniciar nivel
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);

		// Regresar al punto de spawn
		//transform.position = spawnPoint.position;

		// Reiniciar vida máxima
		vida = vidaMaxima;

		if (menuDerrota != null)
		{
			menuDerrota.SetActive(true);
		}
	}
	

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Plataforma")
		{
			transform.parent = null;
		}
	}
}
