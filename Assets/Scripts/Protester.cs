using UnityEngine;
using System.Collections;

public class Protester : MonoBehaviour {

	/*
	public float jumpTimer = 2f;
	public float jumpHeight = 1f;
	public float jumpCountdown = 0f;
	public bool canJump = false;
	public bool canDrop = false;
	*/

	public Transform followTarget;
	public float smoothTime = .3f;
	public float xOffset = 1f;
	public float yOffset = 1f;
	public bool canFollow = false;
	public bool canHop = false;
	public bool playerGrounded = false;
	public float speedBoost = 2f;
	public float hopBoost = 10f;

	private Transform thisTransform;
	private Vector2 thisPosition;
	//private Vector2 velocity;
	private Vector2 hopPosition;
	 

	private float jumpStart;
	private float jumpDest;

	void Start () {

		thisTransform = transform;
		thisPosition = thisTransform.position;
	
	}

	void Update () {

		/*
		if(jumpCountdown >= jumpTimer){
			canJump = true;
			jumpStart = transform.position.y;
			jumpDest = transform.position.y + jumpHeight;
			jumpCountdown = 0f;
		}else if(!canJump && !canDrop){
			jumpCountdown += Time.deltaTime;
		}
		*/


		if(canFollow && playerGrounded){
			thisPosition.x = Mathf.Lerp( thisTransform.position.x, followTarget.position.x + xOffset, Time.deltaTime * speedBoost);
			thisPosition.y = Mathf.Lerp( thisTransform.position.y, followTarget.position.y + yOffset,  Time.deltaTime * speedBoost);
			thisTransform.position = thisPosition;
		}

		if(canHop){

			thisPosition.x = Mathf.Lerp( thisTransform.position.x, hopPosition.x, Time.deltaTime * hopBoost);
			thisPosition.y = Mathf.Lerp( thisTransform.position.y, hopPosition.y,  Time.deltaTime * hopBoost);
			thisTransform.position = thisPosition;

			if((Vector2)transform.position == hopPosition){
				canHop = false;
				gameObject.layer = 9;
				rigidbody2D.isKinematic = false;
			}

		}


	
	}

	void FixedUpdate(){

		/*
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
		*/


		//thisTransform.position.x = Mathf.Lerp( thisTransform.position.x, followTarget.position.x + xOffset, Time.deltaTime * smoothTime);
		//thisTransform.position.y = Mathf.Lerp( thisTransform.position.y, followTarget.position.y + yOffset, Time.deltaTime * smoothTime);
		


		/*
		if(playerGrounded == false){
			
			gameObject.layer = 9;
			
		}else{
			
			gameObject.layer = 11;
			
		}
		*/








	}

	void LateUpdate(){

	}

	public void setLayer(int layerToMoveProtesterTo){
		gameObject.layer = layerToMoveProtesterTo;
	}

	public void hopToLocation(Vector2 hopLoc){

		hopPosition = hopLoc;
		canHop = true; 
		gameObject.layer = 11;
		rigidbody2D.isKinematic = true;
	}
}
