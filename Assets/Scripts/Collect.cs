using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
	[SerializeField] private GameObject objeto;
	[SerializeField] private float cantidadpuntos;
	[SerializeField] private Puntuacion puntaje;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player"))
		{
			puntaje.SumarPuntos(cantidadpuntos);
			Destroy(gameObject);
		}
	}
}
