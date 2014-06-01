using UnityEngine;
using System.Collections;

public class Protester : MonoBehaviour {
	
	public float jumpTimer = 2f;
	public float jumpHeight = 1f;

	public float jumpCountdown = 0f;
	public bool canJump = false;
	public bool canDrop = false;


	private float jumpStart;
	private float jumpDest;

	void Start () {
	
	}

	void Update () {


		if(jumpCountdown >= jumpTimer){
			canJump = true;
			jumpStart = transform.position.y;
			jumpDest = transform.position.y + jumpHeight;
			jumpCountdown = 0f;
		}else if(!canJump && !canDrop){
			jumpCountdown += Time.deltaTime;
		}




	
	}

	void FixedUpdate(){

		if(canJump){

			
			rigidbody2D.MovePosition(new Vector2(transform.position.x, Mathf.MoveTowards(transform.position.y, jumpDest, .2f)));
			
			if(transform.position.y == jumpDest){
				canDrop = true;
				canJump = false;
				
				
				
			}
			
		}
		
		if(canDrop){
			rigidbody2D.MovePosition(new Vector2(transform.position.x, Mathf.MoveTowards(transform.position.y, jumpStart, .2f)));
			
			if(transform.position.y == jumpStart){
				canDrop = false;
			}
		}


	}
}
