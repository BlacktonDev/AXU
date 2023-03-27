using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	
	public float speed = 5f;
	public float jumpForce = 10f;
	public float gravity = 1f;
	
	private Rigidbody2D rb;
	private Animator anim;
	
	
	public bool isGrounded = false;
	public bool isUnderwater = false;
	
    // Start is called before the first frame update
    void Start()
    {
	    rb = GetComponent<Rigidbody2D>();
	    anim = GetComponent<Animator>();
    }
    
    
	// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);

		if (moveHorizontal < 0)
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}
		else if (moveHorizontal > 0)
		{
			transform.localScale = new Vector3(1, 1, 1);
		}

		if (Input.GetKey(KeyCode.Space) && isGrounded)
		{
			rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
			isGrounded = false;
		}

		rb.AddForce(new Vector2(0f, -gravity * rb.mass));

		if (Input.GetKeyDown(KeyCode.F))
		{
			anim.SetTrigger("Attack");
			Debug.Log("Ataque");
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			SkillTree skillTree = FindObjectOfType<SkillTree>();
			//skillTree.SkillOne();
		}
	}
	
	// Sent when an incoming collider makes contact with this object's collider (2D physics only).
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
			isGrounded = true;
		}
		else if(isUnderwater == true)
		{
			isGrounded = true;
		}
	}
}
