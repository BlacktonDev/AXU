using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertirGravedad : MonoBehaviour
{
	private bool isInsideCollider = false;
	private Rigidbody2D rb;
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
		    // Cambiar la gravedad a la dirección opuesta
		    rb.gravityScale = -1f;
	    }
	    else
	    {
		    // Restaurar la gravedad normal
		    rb.gravityScale = 1f;
	    }
    }
    
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.CompareTag("Corriente"))
		{
			isInsideCollider = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Corriente"))
		{
			isInsideCollider = false;
		}
	}
}
