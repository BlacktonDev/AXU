using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataformadestruible : MonoBehaviour
{
	private bool playerDetected = false;
	private bool isPlatformActive = true;
	public float deactivationTime = 5f;
	public float reactivationTime = 5f;
	private float timer = 0f;
	private Material originalMaterial;
	public Material deactivationMaterial;
	private Renderer platformRenderer;
	
	void Start()
	{
		// Obtener el componente Renderer
		platformRenderer = GetComponent<Renderer>();

		// Almacenar el material original
		originalMaterial = platformRenderer.material;
	}
    
	void Update()
	{
		if (playerDetected && isPlatformActive)
		{
			timer += Time.deltaTime;

			if (timer >= deactivationTime)
			{
				isPlatformActive = false;
				timer = 0f;
				// Desactivar la plataforma (por ejemplo, deshabilitar el Rigidbody2D)
				GetComponent<Rigidbody2D>().simulated = false;
			}
		}
		else if (!isPlatformActive)
		{
			timer += Time.deltaTime;

			if (timer >= reactivationTime)
			{
				isPlatformActive = true;
				timer = 0f;
				// Volver a activar la plataforma (por ejemplo, habilitar el Rigidbody2D)
				GetComponent<Rigidbody2D>().simulated = true;
			}
		}
		
		if (isPlatformActive)
		{
			platformRenderer.material = originalMaterial;
			// Resto del código...
		}
		else
		{
			platformRenderer.material = deactivationMaterial;
			// Resto del código...
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			playerDetected = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			playerDetected = false;
		}
	}
}
