using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seguimiento : MonoBehaviour
{
	[SerializeField] private GameObject fondo;
	[SerializeField] private float velocidad;
	
	private Renderer fondor;
	private float incam,difcam;
	public Vector2 cammin,cammax;
	public GameObject seguir;
    // Start is called before the first frame update
    void Start()
    {
	    fondor = fondo.GetComponent<Renderer>();
	    incam = transform.position.x;
    }

    // Update is called once per frame
    void Update()
	{
		//seguimiento de la camara
		float X = seguir.transform.position.x;
		float Y = seguir.transform.position.y;
		transform.position = new Vector3(Mathf.Clamp(X,cammin.x,cammax.x),Mathf.Clamp(Y,cammin.y,cammax.y),transform.position.z);
		
		//Velocidad de seguimiento de cada fondo
		difcam = incam - transform.position.x;
		Vector2 offfon =  new Vector2(difcam * velocidad * -1, 0.0f);
		
		fondor.material.mainTextureOffset = offfon;
		
		fondor.transform.position = new Vector3(transform.position.x,fondor.transform.position.y,fondor.transform.position.z);
        
    }
}
