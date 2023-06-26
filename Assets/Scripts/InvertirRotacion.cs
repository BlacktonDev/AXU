using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertirRotacion : MonoBehaviour
{
	private bool invertido = false;
	private Quaternion rotacionNormal;

	private void Start()
	{
		rotacionNormal = transform.rotation;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("ObjetoInvertido") && !invertido)
		{
			// Invertir la rotación en el eje X
			Vector3 nuevaRotacion = transform.rotation.eulerAngles;
			nuevaRotacion.x += 180f;
			transform.rotation = Quaternion.Euler(nuevaRotacion);

			invertido = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("ObjetoInvertido") && invertido)
		{
			// Restaurar la rotación normal
			transform.rotation = rotacionNormal;

			invertido = false;
		}
	}
}
