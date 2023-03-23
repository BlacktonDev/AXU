using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	//Objeto que el personaje puede equipar y usar para atacar
	public GameObject weapon;

	//Fuerza del ataque
	public float attackForce = 10f;

	//Duración del tiempo de espera después de un ataque
	public float attackCooldown = 1f;

	//Referencia al componente Rigidbody2D del personaje
	private Rigidbody2D rb;

	//Variable que indica si el personaje puede atacar o no
	private bool canAttack = true;

	//Referencia al componente Animator del personaje
	private Animator anim;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	void Update()
	{
		//Equipar o desequipar el objeto de ataque
		if (Input.GetKeyDown(KeyCode.E))
		{
			EquipWeapon();
		}
		else if (Input.GetKeyDown(KeyCode.Q))
		{
			UnequipWeapon();
		}

		//Atacar si se presiona el botón de ataque y el personaje puede atacar
		if (Input.GetKeyDown(KeyCode.F) && canAttack)
		{
			Attack();
		}
	}

	//Equipar el objeto de ataque
	void EquipWeapon()
	{
		weapon.SetActive(true);
	}

	//Desequipar el objeto de ataque
	void UnequipWeapon()
	{
		weapon.SetActive(false);
	}

	//Atacar
	void Attack()
	{
		//Comprobar si el objeto de ataque está equipado
		if (weapon.activeSelf)
		{
			//Obtener la dirección del ataque
			Vector2 attackDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

			//Aplicar la fuerza del ataque al objeto de ataque
			Rigidbody2D weaponRB = weapon.GetComponent<Rigidbody2D>();
			weaponRB.velocity = attackDirection * attackForce;

			//Desactivar el objeto de ataque después de un tiempo corto para simular que se rompe
			StartCoroutine(DisableWeapon());

			//Iniciar la animación de ataque
			anim.SetTrigger("attack");

			//Establecer el tiempo de espera después del ataque
			canAttack = false;
			StartCoroutine(AttackCooldown());
		}
	}

	//Desactivar el objeto de ataque después de un tiempo corto para simular que se rompe
	IEnumerator DisableWeapon()
	{
		yield return new WaitForSeconds(0.5f);
		weapon.SetActive(false);
	}

	//Establecer el tiempo de espera después del ataque
	IEnumerator AttackCooldown()
	{
		yield return new WaitForSeconds(attackCooldown);
		canAttack = true;
	}
}
