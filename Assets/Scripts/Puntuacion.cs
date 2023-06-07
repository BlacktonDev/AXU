using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Puntuacion : MonoBehaviour
{
	private float puntos;
	private TextMeshProUGUI texto;
    // Start is called before the first frame update
	private void Start()
    {
	    texto = GetComponent<TextMeshProUGUI>(); 
    }

    // Update is called once per frame
	private void Update()
    {
	    //puntos += Time.deltaTime;
	    texto.text = puntos.ToString("0");
    }
	
	public void SumarPuntos(float puntos_nuevos)
	{
		puntos += puntos_nuevos;
	}
}
