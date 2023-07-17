using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diavlo : MonoBehaviour
{
	public Vector2 direction = Vector2.right;
	public int vida =10;
	public float speed = 1f;
	private GameObject player;
	private Vector2 targetPosition;
	private Vector2 initialPosition;
	private int estado = 0;
	private float ime1 = 0;
	private float ime2 = 3;
	private int mover = 0;
	private int caer = 0;
	private float elapsedTime = 0;
	private float fallspeed = 20;
	private bool noObtenido = false;
	private Vector2 resetPosition;
	private bool resetTriggered = false;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		resetPosition = new Vector2(645.4f, 82.56f); // Asigna la posición deseada del objeto para el reinicio
	}

	void Update()
	{
		if (player == null)
		{
			return;
		}
		if (vida <= 0)
		{
			gameObject.SetActive(false);
		}

		float distance = Vector2.Distance(transform.position, player.transform.position);

		if (distance <= 8)
		{
			mover = 1;
		}
		if (distance <= 30)
		{
			estado = 1;
		}
		else
		{
			estado = 0;
		}
		if (estado == 1 && mover != 1)
		{
			moverse();
		}
		if (mover == 1)
		{
			posicionarse();
		}
		if (caer == 1)
		{
			aplatar();
		}
	}

	private void moverse()
	{
		transform.Translate(direction * speed * Time.deltaTime);
	}

	private void posicionarse()
	{
		if (!noObtenido)
		{
			targetPosition = new Vector2(player.transform.position.x, player.transform.position.y+5);
			initialPosition = transform.position;
			noObtenido = true;
		}

		ime1 += Time.deltaTime;

		if (ime1 < ime2)
		{
			ime1 += Time.deltaTime;
			float t = Mathf.Clamp01(ime1 / ime2);
			Vector2 newPosition = Vector2.Lerp(initialPosition, targetPosition, t);
			transform.position = newPosition;
		}

		if (ime1 >= ime2)
		{
			caer = 1;
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("balaPlayer")) // Verifica si el objeto que ingresó al trigger tiene el tag "Player"
		{
			vida--;
		}
	}
	private void aplatar()
	{
		elapsedTime += Time.deltaTime;

		if (elapsedTime < 1f)
		{
			transform.Translate(Vector2.down * fallspeed * Time.deltaTime);
		}

		if (elapsedTime >= 1)
		{
			ime1=0;
			elapsedTime=0;
			estado = 0;
			caer = 0;
			mover = 0;
			noObtenido = false;
			resetTriggered = false;
			Reset();
		}
	}

	private void Reset()
	{
		if (!resetTriggered)
		{
			transform.position = resetPosition;
			resetTriggered = true;
		}
	}
}