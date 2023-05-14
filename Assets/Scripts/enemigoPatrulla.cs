using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigoPatrulla : MonoBehaviour
{
	
	public float speed = 1f; 
	public float distance = 1f;
	public int saludMaxima = 100;
	private int saludActual;

	private Vector3 startPosition; 

	private void Start()
	{
		startPosition = transform.position;
		saludActual = saludMaxima;
	}

	private void Update()
	{
		
		transform.position = startPosition + new Vector3(Mathf.PingPong(Time.time * speed, distance), 0, 0);

		
		if (transform.position.x < startPosition.x)
		{
			
			MoveLeftAnim();
		}

		
		if (transform.position.x > startPosition.x)
		{
			
			MoveRightAnim();
		}
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
