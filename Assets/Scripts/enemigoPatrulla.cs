using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigoPatrulla : MonoBehaviour
{
	
	public float speed = 1f; 
	public float distance = 1f;
	public int saludMaxima = 100;
	private int saludActual;
	private Collider2D[] objectsOnTop;
	private string[] validTags = { "Player", "Enemy" };

	private Vector3 startPosition; 
	
	
	private bool isMovingLeft = true;
	private Collider2D objectOnTop;

	private void Start()
	{
		startPosition = transform.position;
		saludActual = saludMaxima;
	}

	private void Update()
	{
		
		if (isMovingLeft)
		{
			transform.Translate(Vector3.left * speed * Time.deltaTime);
		}
		else
		{
			transform.Translate(Vector3.right * speed * Time.deltaTime);
		}

		// Cambio de dirección cuando alcanza los límites
		if (transform.position.x <= startPosition.x - 2f)
		{
			isMovingLeft = false;
		}
		else if (transform.position.x >= startPosition.x + 2f)
		{
			isMovingLeft = true;
		}

		// Movimiento del objeto sobre el objeto principal
		if (objectOnTop != null)
		{
			objectOnTop.transform.position = transform.position + Vector3.up;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Detección de colisión con otro objeto
		if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
		{
			collision.transform.parent = transform;
			UpdateObjectsOnTop();
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		// Detección de salida de colisión con otro objeto
		if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
		{
			collision.transform.parent = null;
			UpdateObjectsOnTop();
		}
	}

	private void UpdateObjectsOnTop()
	{
		objectsOnTop = GetComponentsInChildren<Collider2D>();
	}

	private void MoveLeftAnim()
	{
		
	}

	private void MoveRightAnim()
	{
		
	}
	
	public void RecibirDanio(int cantidad)
	{
		saludActual -= cantidad;

		if (saludActual <= 0)
		{
			Morir();
		}
	}
	
	void Morir()
	{
		// Agregar aquí la lógica para la muerte del enemigo
		Destroy(gameObject);
	}
		
}
