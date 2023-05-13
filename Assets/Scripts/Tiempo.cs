using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tiempo : MonoBehaviour
{
	[SerializeField] int min,seg;
	[SerializeField] TextMeshProUGUI tiempo;
	[SerializeField] GameObject menuderrota;
	
	private float restante;
	private bool enmarcha;
	
	private void Awake()
    {
	    restante = (min * 60) + seg;
	    enmarcha = true;
    }

    // Update is called once per frame
    void Update()
    {
	    if(enmarcha)
	    {
	    	restante -= Time.deltaTime;
	    	if(restante < 1)
	    	{
	    		enmarcha = false;
		    	menuderrota.SetActive(true);
		    	Time.timeScale = 0f;
	    	}
	    	int tempmin = Mathf.FloorToInt(restante / 60);
	    	int tempseg = Mathf.FloorToInt(restante % 60);
	    	tiempo.text = string.Format("{00:00}:{01:00}", tempmin, tempseg);
	    }
    }
}
