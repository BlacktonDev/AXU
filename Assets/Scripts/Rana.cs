using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rana : MonoBehaviour
{
	public float jumpForce = 7f; // Fuerza de salto
	public float movementSpeed = 3f; // Velocidad de movimiento horizontal
	public LayerMask groundLayer; // Layer que representa el suelo

	private Rigidbody2D rb;
	private bool isJumping = false;
	private bool isOnGround = false;
	private GameObject jugador;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		BuscarJugador();
	}

	private void Update()
	{
		if (jugador != null && isJumping)
		{
			// Movimiento horizontal hacia la dirección del jugador
			Vector2 direction = (jugador.transform.position - transform.position).normalized;
			rb.velocity = new Vector2(direction.x * movementSpeed, rb.velocity.y);
		}
		else
		{
			rb.velocity = Vector2.zero; // Detener el movimiento si el jugador no está asignado o no está saltando
		}
	}

	private void FixedUpdate()
	{
		if (isOnGround && isJumping)
		{
			// Salto
			rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			isOnGround = false; // Restablecer el estado de estar en el suelo
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

	private void BuscarJugador()
	{
		GameObject jugadorObj = GameObject.FindGameObjectWithTag("Player");
		if (jugadorObj != null)
		{
			jugador = jugadorObj;
			isJumping = true;
		}
	}
}