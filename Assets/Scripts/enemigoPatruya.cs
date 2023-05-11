using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigoPatruya : MonoBehaviour
{
	
	public float speed = 1f; 
	public float distance = 1f; 

	private Vector3 startPosition; 

	private void Start()
	{
		startPosition = transform.position;
	}

	private void Update()
	{
		
		transform.position = startPosition + new Vector3(Mathf.PingPong(Time.time * speed, distance), 0, 0);

		
		if (transform.position.x < startPosition.x)
		{
			
			MoveLeftAnim();
		}

		
		if (transform.position.x > startPosition.x)
		{
			
			MoveRightAnim();
		}
	}

	private void MoveLeftAnim()
	{
		
	}

	private void MoveRightAnim()
	{
		
	}
		
}
