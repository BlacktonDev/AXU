using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
	[SerializeField] GameObject menu_pausa;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			menu_pausa.SetActive(true);	
			Pausar();
			Debug.Log("123");
		}
    }
    
	public void Pausar()
	{
		Time.timeScale = 0f;
	}
	
	public void Reanudar()
	{
		Time.timeScale = 1f;
		menu_pausa.SetActive(false);
	}
	
	public void Salir()
	{
		Debug.Log("saliendo");
		Application.Quit();
	}
}
