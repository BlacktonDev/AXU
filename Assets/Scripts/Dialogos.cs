using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogos : MonoBehaviour
{
	[SerializeField] GameObject txtdialogos;
	[SerializeField] GameObject dialogos;
	private Rigidbody2D rb;
	private bool isInsideCollider = false;
    // Start is called before the first frame update
    void Start()
    {
	    rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
	    if (isInsideCollider)
	    {
		    txtdialogos.SetActive(true);
		    dialogos.SetActive(true);
	    }
	    else
	    {
		    txtdialogos.SetActive(false);
		    dialogos.SetActive(false);
	    }
    }
    
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isInsideCollider = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			isInsideCollider = false;
		}
	}
}
