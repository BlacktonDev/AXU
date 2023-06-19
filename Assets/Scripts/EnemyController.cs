using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
	public float speed = 5f;
	public float damage = 10f;
	public float attackRange = 1f;

	private Transform player;
	private Rigidbody2D rb;

	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		rb = GetComponent<Rigidbody2D>();
		rb.gravityScale = 1f; // Establecer la gravedad del enemigo
		gameObject.layer = LayerMask.NameToLayer("Ground"); // Establecer la capa del enemigo
	}

	// Update is called once per frame
	void Update()
	{
		float distanceToPlayer = Vector2.Distance(transform.position, player.position);

		if (distanceToPlayer < attackRange)
		{
			Vector2 direction = (player.position - transform.position).normalized;
			rb.velocity = direction * speed;
		}
		else
		{
			rb.velocity = Vector2.zero;
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			PlayerMovement playerController = collision.gameObject.GetComponent<PlayerMovement>();
			//playerController.TakeDamage(damage);
		}
	}
}