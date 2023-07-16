using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala_izq : MonoBehaviour
{
	public Vector2 direction = Vector2.left;
	public float ime =0;
	public float speed = 1f; // Velocidad de movimiento

	private void Update()
	{
		ime = ime + Time.deltaTime;
		if(ime>3){
			Destroy(gameObject);
		}
		// Mueve el objeto en la dirección especificada a la velocidad especificada
		transform.Translate(direction * speed * Time.deltaTime);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player")) // Verifica si el objeto que ingresó al trigger tiene el tag "Player"
		{
			Destroy(gameObject); // Llama a la función "Restar"
		}
	}
}
