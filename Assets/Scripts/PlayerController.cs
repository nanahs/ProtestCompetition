using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 5f;

	public int numFollowers = 0;

	public float h;
	private Animator anim;
	private bool facingRight = true;
	
	void Start () {

		anim = GetComponent<Animator>();
	
	}

	void Update () {



	}

	void FixedUpdate(){

		h = Input.GetAxis("Horizontal");

		//Sets the direction that the player needs to face based on the movement
		if(h > 0 && !facingRight){
			FlipSprite();
		}else if(h < 0 && facingRight){
			FlipSprite();
		}

		anim.SetFloat("Speed", Mathf.Abs(h));
		rigidbody2D.velocity = new Vector2(h * moveSpeed, rigidbody2D.velocity.y);



	}

	private void FlipSprite(){

		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnCollisionEnter2D(Collision2D col){

		if(col.gameObject.tag == "Protester"){
			numFollowers++;
		}

	}
}
