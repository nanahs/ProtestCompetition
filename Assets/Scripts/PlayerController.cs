using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 5f;
	public int numFollowers = 0;
	public float followerOffset = .25f;

	private ArrayList followerList;
	private ArrayList followAnim;
	private float h;
	private Animator anim;
	private bool facingRight = true;
	
	void Start () {

		anim = GetComponent<Animator>();
		followerList = new ArrayList();
		followAnim = new ArrayList();
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

		//Update all the follower animations
		foreach(Animator anima in followAnim){
			anima.SetFloat("Speed", Mathf.Abs(h));
		}



	}

	private void FlipSprite(){

		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;

		if(followerList.Count != 0){
			foreach(GameObject gameObj in followerList){
				gameObj.transform.parent = null;
				gameObj.transform.localScale = theScale;
			}
		}

		transform.localScale = theScale;

		if(followerList.Count != 0){

			foreach(GameObject gameObj in followerList){
				gameObj.transform.parent = gameObject.transform;
			}

		}
	}

	void OnCollisionEnter2D(Collision2D col){

		if(col.gameObject.tag == "Protester"){
			col.gameObject.layer = 11;

			followerList.Add(col.gameObject);
			followAnim.Add(col.gameObject.GetComponent<Animator>());

			//Destroy(col.gameObject.rigidbody2D);
			//Destroy(col.collider);
			//Destroy(col.gameObject.GetComponent<Protester>());


			col.transform.parent = this.gameObject.transform;

			if(numFollowers%2 == 0){
				col.transform.position = new Vector3(transform.position.x + followerOffset +(followerOffset*numFollowers) , transform.position.y, 0);
			}else{
				col.transform.position = new Vector3(transform.position.x - followerOffset - (followerOffset*numFollowers) , transform.position.y, 0);
			}

			numFollowers++;

		

		}

	}





















}
