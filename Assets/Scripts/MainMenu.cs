using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		
	}
 
	// Update is called once per frame
	void Update()
	{
      
	}
	
	public void Jugar(string escena)
	{
		SceneManager.LoadScene(escena);
	}

	public void Salir()
	{
		Debug.Log("saliendo");
		Application.Quit();
	}
}
