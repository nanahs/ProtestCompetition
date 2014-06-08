using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 5f;
	public float jumpForce = 3f;
	public int numFollowers = 0;
	public float followerOffset = .25f;
	
	private List<GameObject> followerList;
	private List<Protester> followerScripts;
	private List<Animator> followAnim;
	private float h;
	private float v;
	private float tempVert;
	private Animator anim;
	private bool facingRight = true;

	private Transform groundCheck;
	private Transform ceilingCheck;
	private float groundedRadius = .2f;
	private float ceilingRadius = .1f;
	public bool grounded = false;
	public bool specialGrounded = false;
	public LayerMask whatIsGround;
	public LayerMask whatIsSpecialGround;
	private bool prevGroundedState;

	private bool jump = false;

	
	void Start () {

		// Setting up references.
		groundCheck = transform.Find("GroundCheck");
		ceilingCheck = transform.Find("CeilingCheck");
		anim = GetComponent<Animator>();

		//Setting up Lists
		followerList = new List<GameObject>();
		followerScripts = new List<Protester>();
		followAnim = new List<Animator>();

	}

	void Update () {

		if(Input.GetKeyDown(KeyCode.Space) && (grounded || specialGrounded) ){
			jump = true;
		}

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);

		//Special grounded includes standing on a follower or other unique things
		specialGrounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsSpecialGround);

		//Prevent looping through each list every frame
		if(prevGroundedState != grounded){

			if(grounded){
				foreach(Protester prot in followerScripts){
					prot.setLayer(11);
					prot.playerGrounded = true;
				}
			}else{
				foreach(Protester prot in followerScripts){
					prot.setLayer(9);
					prot.playerGrounded = false;
				}
			}

		}
		prevGroundedState = grounded;

		//TODO organize people that are used into their own group and remove them from the arraylist
		//Remember to add them back in to the followerScript list later
		//dont forget to update the current num of followers to make sure it doesnt grab the wrong person(or have it start grabbing people from 0)
		//Starts a pyramid of people
		if(Input.GetKeyDown(KeyCode.UpArrow) && specialGrounded && (numFollowers > 3)){

			followerScripts[numFollowers-1].hopToLocation(this.transform.position);

		}

	}

	void FixedUpdate(){

		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);

		//Sets the direction that the player needs to face based on the movement
		if(h > 0 && !facingRight){
			FlipSprite();
		}else if(h < 0 && facingRight){
			FlipSprite();
		}

		//Set Play animations
		anim.SetFloat("Speed", Mathf.Abs(h));
		rigidbody2D.velocity = new Vector2(h * moveSpeed, rigidbody2D.velocity.y);

		//Update all the followers
		updateFollowers(h, grounded);


		// If the player should jump...
		if ((grounded || specialGrounded) && jump) {
			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			jump = false;
			specialGrounded = false;
		}

	}

	private void FlipSprite(){

		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;

		if(followerList.Count != 0){
			foreach(GameObject gameObj in followerList){
				//gameObj.transform.parent = null;
				gameObj.transform.localScale = theScale;
			}
		}

		transform.localScale = theScale;

		/*
		if(followerList.Count != 0){

			foreach(GameObject gameObj in followerList){
				gameObj.transform.parent = gameObject.transform;
			}

		}
		*/
	}

	void updateFollowers(float playerMovement, bool isPlayerGrounded){

		foreach(Animator anima in followAnim){
			anima.SetFloat("Speed", Mathf.Abs(playerMovement));
		}

		/*
		foreach(Protester prot in followerScripts){
			prot.playerGrounded = isPlayerGrounded;
		}
		*/
	}

	void OnCollisionEnter2D(Collision2D col){

		if(col.gameObject.tag == "Protester"){

			//Prevents collision with the protester
			//col.gameObject.layer = 11;

			followerList.Add(col.gameObject);
			followerScripts.Add(col.gameObject.GetComponent<Protester>());
			followAnim.Add(col.gameObject.GetComponent<Animator>());

			col.gameObject.GetComponent<Protester>().followTarget = this.transform;
			col.gameObject.GetComponent<Protester>().canFollow = true;
			col.gameObject.GetComponent<Protester>().playerGrounded = grounded;
			col.gameObject.GetComponent<Protester>().setLayer(11);
			col.gameObject.tag = "Follower";

			//Destroy(col.gameObject.rigidbody2D);
			//Destroy(col.collider);
			//Destroy(col.gameObject.GetComponent<Protester>());

			/*
			Set protester as a child to follow the player directly
			col.transform.parent = this.gameObject.transform;

			if(numFollowers%2 == 0){
				col.transform.position = new Vector3(transform.position.x + followerOffset +(followerOffset*numFollowers) , transform.position.y, 0);
			}else{
				col.transform.position = new Vector3(transform.position.x - followerOffset - (followerOffset*numFollowers) , transform.position.y, 0);
			}
			*/


			numFollowers++;

		

		}

	}





















}
