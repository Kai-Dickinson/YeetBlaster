using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyProperties : MonoBehaviour {

	private int counter = 0;
	public float speed = 1f;
	Animator animator;
	private int hitsTaken = 0;
	Transform EnemyDeath;

	void Awake () {
		animator = GetComponent<Animator>();

		EnemyDeath = transform.Find ("EnemyDeath"); //Find the PlayerDamaged object
		if (EnemyDeath == null) {
			Debug.LogError ("No Enemy Death Sound"); //Return an error if not found
		}
	}

	// Update is called once per frame
	void Update () {
		moving ();
	}

	void moving() { //Moves the enemies left and right on a counter and changes the animations
		if (counter <= 50) {
			transform.position += Vector3.right * speed * Time.deltaTime *5;
			transform.eulerAngles = new Vector2(0,180);
			animator.SetBool("isMoving", true);
			counter += 1;
		}

		if (counter > 50 && counter <= 100) {
			transform.position += Vector3.left * 0 * Time.deltaTime *5;
			animator.SetBool("isMoving", false);
			counter += 1;
		}

		if (counter > 100 && counter <= 150) {
			transform.position += Vector3.left * speed * Time.deltaTime *5;
			transform.eulerAngles = new Vector2(0,0);
			animator.SetBool("isMoving", true);
			counter += 1;
		}

		if (counter > 150 && counter <= 200) {
			transform.position += Vector3.right * 0 * Time.deltaTime *5;
			animator.SetBool("isMoving", false);
			counter += 1;
		}
	
		if (counter > 200) {
			counter = 0; //Reset the counter
		}
	}

	void chasing() {
		
	}

	void OnCollisionEnter2D(Collision2D coll) { //Checks for collisions
		if (coll.gameObject.tag == "bullet") {
			hitsTaken += 1; //Increase hit counter if hit

			if (hitsTaken >= 2) {
				animator.SetTrigger("hasDied");
				EnemyDeath.GetComponent<AudioSource> ().Play ();
				GameScript.KillEnemy (this); //Kills enemy after 2 hits
			}

		}
	}
}
