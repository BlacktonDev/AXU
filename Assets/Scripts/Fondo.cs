using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fondo : MonoBehaviour
{
	[SerializeField] private Vector2 velocidad;
	private Vector2 offset;
	private Material material;
	private Rigidbody2D jugador;

	private void Awake()
	{
		material = GetComponent<SpriteRenderer>().material;
		jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (jugador != null)
		{
			offset = (jugador.velocity.x * 0.01f) * velocidad * Time.deltaTime;
			material.mainTextureOffset += offset;
		}
	}
}

