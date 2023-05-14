using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour
{
	public int damage = 10;
	public float attackRange = 0.5f;
	public LayerMask enemyLayer;
	public Transform attackPoint;

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			Attack();
		}
	}

	void Attack()
	{
		// Detecta los enemigos cercanos al personaje
		Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

		// Inflige daño a cada enemigo detectado
		foreach (Collider2D enemy in hitEnemies)
		{
			enemy.GetComponent<enemigoPatrulla>().RecibirDanio(damage);
		}
	}

	void OnDrawGizmosSelected()
	{
		if (attackPoint == null) return;

		Gizmos.DrawWireSphere(attackPoint.position, attackRange);
	}
}
