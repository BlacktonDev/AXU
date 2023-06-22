using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seguimiento : MonoBehaviour
{
	[SerializeField] private GameObject fondo;
	[SerializeField] private GameObject fondo1;
	[SerializeField] private GameObject fondo2;
	[SerializeField] private GameObject fondo3;
	[SerializeField] private GameObject fondo4;
	[SerializeField] private GameObject fondo5;
	[SerializeField] private GameObject fondo6;
	[SerializeField] private GameObject fondo7;
	[SerializeField] private float velocidad;
	
	private Renderer fondor,fondo1r, fondo2r,fondo3r,fondo4r,fondo5r,fondo6r,fondo7r;
	private float incam,difcam;
	public Vector2 cammin,cammax;
	public GameObject seguir;
    // Start is called before the first frame update
    void Start()
    {
	    fondor = fondo.GetComponent<Renderer>();
	    fondo1r = fondo1.GetComponent<Renderer>();
	    fondo2r = fondo2.GetComponent<Renderer>();
	    fondo3r = fondo3.GetComponent<Renderer>();
	    fondo4r = fondo4.GetComponent<Renderer>();
	    fondo5r = fondo5.GetComponent<Renderer>();
	    fondo6r = fondo6.GetComponent<Renderer>();
	    fondo7r = fondo7.GetComponent<Renderer>();
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
		Vector2 offfon1 = new Vector2(difcam * velocidad * -2, 0.0f);
		Vector2 offfon2 = new Vector2(difcam * (velocidad * 1f) * -1, 0.0f);
		Vector2 offfon3 = new Vector2(difcam * (velocidad * 0.8f) * -1, 0.0f);
		Vector2 offfon4 = new Vector2(difcam * (velocidad * 0.4f) * -1, 0.0f);
		Vector2 offfon5 = new Vector2(difcam * (velocidad * 0.4f) * -1, 0.0f);
		Vector2 offfon6 = new Vector2(difcam * (velocidad * 0.2f) * -1, 0.0f);
		Vector2 offfon7 = new Vector2(difcam * (velocidad * 0.1f) * -1, 0.0f);
		
		fondor.material.mainTextureOffset = offfon;
		fondo1r.material.mainTextureOffset = offfon1;
		fondo2r.material.mainTextureOffset = offfon2;
		fondo3r.material.mainTextureOffset = offfon3;
		fondo4r.material.mainTextureOffset = offfon4;
		fondo5r.material.mainTextureOffset = offfon5;
		fondo6r.material.mainTextureOffset = offfon6;
		fondo7r.material.mainTextureOffset = offfon7;
		
		fondor.transform.position = new Vector3(transform.position.x,fondor.transform.position.y,fondor.transform.position.z);
		fondo1r.transform.position = new Vector3(transform.position.x,fondo1r.transform.position.y,fondo1r.transform.position.z);
		fondo2r.transform.position = new Vector3(transform.position.x,fondo2r.transform.position.y,fondo2r.transform.position.z);
		fondo3r.transform.position = new Vector3(transform.position.x,fondo3r.transform.position.y,fondo3r.transform.position.z);
		fondo4r.transform.position = new Vector3(transform.position.x,fondo4r.transform.position.y,fondo4r.transform.position.z);
		fondo5r.transform.position = new Vector3(transform.position.x,fondo5r.transform.position.y,fondo5r.transform.position.z);
		fondo6r.transform.position = new Vector3(transform.position.x,fondo6r.transform.position.y,fondo6r.transform.position.z);
		fondo7r.transform.position = new Vector3(transform.position.x,fondo7r.transform.position.y,fondo7r.transform.position.z);
		
    }
}
