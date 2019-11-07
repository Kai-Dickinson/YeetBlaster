using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossOneScript : MonoBehaviour {

	public int hitsTaken = 0; //Count the amount of times it has been hit
	Animator animator; //Call animator
	public float DeathShake = 0.1f; //Set value of screen shake
	cameraShake camShake; //Call the camera shake to be used later
	Transform IdleSound;

	public float timeToSpawnFire = 0.0f; //Setting variable for fireball
	public float spawnRate = 1f;
	public Transform FireBallPrefab;
	Transform firePointBoss;

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator>();

		IdleSound = transform.Find ("IdleSound"); //Find the firePos object
		if (IdleSound == null) {
			Debug.LogError ("No Boss Idle Sound"); //Return an error if not found
		}
	}

	void Start () {
		camShake = GameScript.gm.GetComponent<cameraShake> ();
		if (camShake == null) {
			Debug.LogError ("No Camera Shake"); //Checks if camShake has a value
		}

		firePointBoss = transform.Find ("firePointBoss"); //Find the firePos object
		if (firePointBoss == null) {
			Debug.LogError ("No Fire Point for boss"); //Return an error if not found
		}
	}

	void Update () {
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			if (Vector3.Distance (GameObject.FindGameObjectWithTag ("Player").transform.position, transform.position) < 10) { //Move towards player if within distance
				if (hitsTaken <= 20) { //Only move or shoot if not dead
					moveToward (); //Call move
					Shoot (); //Call shoot
				}

			}		
		}

	}

	void OnCollisionEnter2D(Collision2D coll) { //Checks for collisions
		if (coll.gameObject.tag == "bullet") {
			hitsTaken += 1; //Add to times hit by player

			if (hitsTaken >= 20) { //Check if dead
				camShake.Shake (DeathShake, 3.0f);
				animator.SetBool("dyingBoss", true);
				IdleSound.GetComponent<AudioSource> ().Play (); //Play dead sound
				GameScript.KillBoss (this); //Call the kill function
			}

		}
	}

	void moveToward () {
		Vector3 playerPos = new Vector3 (GameObject.FindGameObjectWithTag ("Player").transform.position.x, GameObject.FindGameObjectWithTag ("Player").transform.position.y, 0f);
		Vector3 currentPos = new Vector3 (transform.position.x, transform.position.y, 0f); //Changing position toward player

		Vector3 finalVec = playerPos - currentPos;

		transform.position += finalVec * Time.deltaTime * 0.5f;
	}

	void Shoot () {
		if (Time.time >= timeToSpawnFire) {
			Effect ();
			timeToSpawnFire = Time.time + 1/spawnRate; //Shooting with intervals
		}
	}

	void Effect () {
		Instantiate (FireBallPrefab, firePointBoss.position, firePointBoss.rotation); //Fireball prefab is called
	}
}
