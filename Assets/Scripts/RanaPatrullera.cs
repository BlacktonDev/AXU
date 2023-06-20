using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RanaPatrullera : MonoBehaviour
{
	public float jumpForce = 5f; // Fuerza de salto
	public float movementSpeed = 3f; // Velocidad de movimiento horizontal
	public LayerMask groundLayer; // Layer que representa el suelo

	private Rigidbody2D rb;
	private bool isJumping = false;
	private bool isOnGround = false;
	private int jumpDirection = 1;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void Update()
	{
		if (isOnGround)
		{
			isJumping = true;
			rb.velocity = Vector2.zero;
			rb.AddForce(new Vector2(jumpDirection * movementSpeed, jumpForce), ForceMode2D.Impulse);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// Detección de colisión con el suelo
		if (((1 << collision.gameObject.layer) & groundLayer) != 0)
		{
			isOnGround = true;
			jumpDirection *= -1; // Cambiar la dirección del siguiente salto
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		// Detección de salida de colisión con el suelo
		if (((1 << collision.gameObject.layer) & groundLayer) != 0)
		{
			isOnGround = false;
		}
	}
}