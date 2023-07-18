using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class ControladorDatosJuego : MonoBehaviour
{
	public GameObject jugador;
	public string archivoDeGuardado;
	public DatosJuego datosJuego = new DatosJuego();
	
	public Vector3 playerPosition;
	public int vidaMaxima;
	public int vida;
	public bool PUDobleDanio;
	public bool PUVida;
	
	private void Start()
	{
		archivoDeGuardado = Application.dataPath + "/datosJuego.json";
		
		jugador = GameObject.FindGameObjectWithTag("Player");
		GuardarDatos();
		CargarDatos();
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.C))
		{
			CargarDatos();
		}
		if (Input.GetKeyDown(KeyCode.G))
		{
			GuardarDatos();
		}
	}
	
	public void CargarDatos()
	{
		if(File.Exists(archivoDeGuardado))
		{
			string contenido = File.ReadAllText(archivoDeGuardado);
			datosJuego = JsonUtility.FromJson<DatosJuego>(contenido);
			
			//jugador.transform.position = datosJuego.playerPosition;
			jugador.GetComponent<PlayerMovement>().vida = datosJuego.vida;
			jugador.GetComponent<PlayerMovement>().PUVida = datosJuego.PUvida;
			jugador.GetComponent<PlayerMovement>().PUDobleDanio = datosJuego.PUDobleDanio;
			jugador.GetComponent<PlayerMovement>().vidaMaxima = datosJuego.vidaMaxima;
			
			
			Debug.Log("Posicion del jugador : " + datosJuego.playerPosition);
			Debug.Log("PUDanio : " + datosJuego.PUDobleDanio);
			Debug.Log("PUVida : " + datosJuego.PUvida);
			Debug.Log("Vida : " + datosJuego.vida);
			Debug.Log("VidaMaxima : " + datosJuego.vidaMaxima);
			Debug.Log("Posicion del jugador : " + datosJuego.vida);
			

			if (datosJuego.PUvida)
			{
				DestruirObjetosPorTag("PUVida");
			}

			if (datosJuego.PUDobleDanio)
			{
				DestruirObjetosPorTag("PUDobleDanio");
			}
			
		}
		else
		{
			Debug.Log("El archivo no existe");
		}
		
	}
	
	public void GuardarDatos()
	{
		DatosJuego nuevosDatos = new DatosJuego()
		{
			//playerPosition = jugador.transform.position,
			vida = jugador.GetComponent<PlayerMovement>().vida,
			PUvida = jugador.GetComponent<PlayerMovement>().PUVida,
			PUDobleDanio = jugador.GetComponent<PlayerMovement>().PUDobleDanio,
			vidaMaxima = jugador.GetComponent<PlayerMovement>().vidaMaxima
		};
		
		string cadenaJSON = JsonUtility.ToJson(nuevosDatos);
		
		File.WriteAllText(archivoDeGuardado, cadenaJSON);
		
		Debug.Log("Archivo Guardado");
	}
	
	private void DestruirObjetosPorTag(string tag)
	{
		GameObject[] objetos = GameObject.FindGameObjectsWithTag(tag);
		foreach (GameObject objeto in objetos)
		{
			Destroy(objeto);
		}
	}
	
}
