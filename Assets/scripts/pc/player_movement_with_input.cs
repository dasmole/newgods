using UnityEngine;
using System.Collections;
using System;


public class player_movement_with_input : MonoBehaviour {
	public string player = "player1";
	private Rigidbody2D rb;
	public float Speed = 2;
	public Vector3 Direction = Vector3.zero;
	Animator anim;
	bool shouldFlip = false;



	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D> ();
	}


	void Update ()
	{
		Move();
		updateAnimation ();
	}

	private void Move()
	{
		shouldFlip = false;
		Direction = Vector3.zero;

		if (Input.GetAxis("Horizontal_" + player) < 0) {
			Direction += Vector3.left;
			shouldFlip = true;
		}

		if (Input.GetAxis("Horizontal_" + player) > 0) {
			if (shouldFlip) {
				Direction -= Vector3.right;
			} else {
				Direction += Vector3.right;
			}
		}

		if (Input.GetAxis("Vertical_" + player) < 0) {
			Direction += Vector3.down;
		}

		if (Input.GetAxis("Vertical_" + player) > 0) {
			Direction += Vector3.up;
		} 

		rb.velocity = Speed * Direction;

		//fix depth
		Vector3 temp = transform.position;
		temp.z = transform.position.y;
		transform.position = temp;

	}



	private void updateAnimation() {

		//set walking as appropriate
		if (Direction == Vector3.zero) {
			anim.SetBool ("isWalking", false);
		} else {
			anim.SetBool ("isWalking", true);
		}

		//flip if we're going left
		if (shouldFlip && (Direction != Vector3.zero)) {
			transform.localRotation = Quaternion.Euler(0, 180, 0);
		} else if (Direction != Vector3.zero) {
			transform.localRotation = Quaternion.Euler(0, 0, 0);
		}


		//set front/back/side
		if ( (Direction == Vector3.right ) || (Direction == Vector3.left)) {
			anim.SetBool ("isSide", true);
			anim.SetBool ("isBack", false);
			anim.SetBool ("isFront", false);
		} else if (Direction == Vector3.up) {
			anim.SetBool ("isSide", false);
			anim.SetBool ("isBack", true);
			anim.SetBool ("isFront", false);
		} else if (Direction == Vector3.down) {
			anim.SetBool ("isSide", false);
			anim.SetBool ("isBack", false);
			anim.SetBool ("isFront", true);
		}
	}
}
