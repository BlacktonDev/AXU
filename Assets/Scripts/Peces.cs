using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peces : MonoBehaviour
{
	public GameObject prefabparticula;
	public float tiempo = 0.5f;
	public float tiempodestruir = 0f;
	public float velocidad = 1f;
	private float tiempoactual = 0f;
	Camera camara;
	
    // Start is called before the first frame update
	private void Start()
	{
		camara = Camera.main;
	}
    
	private void Update()
	{
		tiempoactual += Time.deltaTime;
		if (tiempoactual >= tiempo)
		{
			GenerarParticula();
			tiempoactual = 0f;
		}
	}

	void GenerarParticula()
	{
		Vector3 PosicionGeneracion = CalcularPosicion();
		PosicionGeneracion.z = 0f;
		GameObject particula = Instantiate(prefabparticula,PosicionGeneracion,Quaternion.identity);
		Rigidbody2D rgb2d = particula.GetComponent<Rigidbody2D>();
		rgb2d.velocity = Vector2.left * velocidad;
		
		Destroy(particula, tiempodestruir);
    }
    
	private Vector3 CalcularPosicion()
	{
		float alto = camara.orthographicSize * 2f;
		float ancho = alto * camara.aspect;
		
		Vector3 PosicionGeneracion = camara.transform.position;
		PosicionGeneracion.y += Random.Range(-alto/2f,alto / 2f);
		PosicionGeneracion.x = camara.transform.position.x + ancho / 2f;
		
		return PosicionGeneracion;
	}
}
