using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rana : MonoBehaviour
{
	public float jumpForce = 5f; // Fuerza de salto
	public float movementSpeed = 3f; // Velocidad de movimiento horizontal
	public float detectionRange = 10f; // Rango de detección para seguir al jugador
	public LayerMask groundLayer; // Layer que representa el suelo
	public string playerTag = "Player"; // Tag del objeto jugador

	private Rigidbody2D rb;
	private GameObject player;
	private bool isJumping = false;
	private bool isOnGround = false;
	private bool isFalling = false;
	[SerializeField]Animator animator;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag(playerTag);
	}

	private void Update()
	{
		if (player != null)
		{
			float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

			// Comprobar si la distancia al jugador es menor que el rango de detección
			if (distanceToPlayer < detectionRange)
			{
				// Calcular dirección hacia el jugador
				Vector2 direction = player.transform.position - transform.position;
				direction.Normalize();

				// Verificar si hay obstáculos a la izquierda o derecha
				RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left, 1f, groundLayer);
				RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right, 1f, groundLayer);

				// Moverse hacia el jugador en el eje X solo si no hay obstáculos en los lados
				if (!hitLeft && !hitRight)
				{
					rb.velocity = new Vector2(direction.x * movementSpeed, rb.velocity.y);
				}
				else
				{
					rb.velocity = new Vector2(0f, rb.velocity.y);
				}

				// Saltar si se encuentra en el suelo y no está cayendo
				if (isOnGround && !isFalling)
				{
					isJumping = true;
					rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
					animator.SetBool("direc", true);
				}
			}
			else
			{
				// Detener el movimiento si la distancia al jugador es mayor que el rango de detección
				rb.velocity = new Vector2(0f, rb.velocity.y);
			}
		}

		// Verificar si el objeto está cayendo
		if (rb.velocity.y < 0f)
		{
			isFalling = true;
			
		}
		else
		{
			isFalling = false;
			
			
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// Detección de colisión con el suelo
		if (((1 << collision.gameObject.layer) & groundLayer) != 0)
		{
			isOnGround = true;
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		// Detección de salida de colisión con el suelo
		if (((1 << collision.gameObject.layer) & groundLayer) != 0)
		{
			isOnGround = false;
			animator.SetBool("direc", false);
		}
	}
}