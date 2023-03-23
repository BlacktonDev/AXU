using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//Velocidad horizontal del personaje
	public float speed = 5f;

	//Velocidad de salto del personaje
	public float jumpSpeed = 10f;

	//Fuerza de gravedad que afecta al personaje
	public float gravity = 2f;

	//Referencia al componente Rigidbody2D del personaje
	private Rigidbody2D rb;

	//Variable que indica si el personaje está en contacto con el suelo o no
	private bool isGrounded = false;

	//Referencia al transform del objeto que indica si el personaje está en contacto con el suelo
	public Transform groundCheck;

	//Radio del círculo que se usa para comprobar si el personaje está en contacto con el suelo
	public float groundCheckRadius = 0.1f;

	//Layer del suelo
	public LayerMask groundLayer;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		//Comprobar si el personaje está en contacto con el suelo
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

		//Mover horizontalmente el personaje
		float move = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2(move * speed, rb.velocity.y);

		//Hacer que el personaje salte si está en contacto con el suelo
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
		}

		//Aplicar la fuerza de la gravedad al personaje
		rb.velocity += new Vector2(0, -gravity * Time.deltaTime);
	}
}
