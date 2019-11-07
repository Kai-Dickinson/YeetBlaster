using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossTwoScript : MonoBehaviour {
	
	public int hitsTaken = 0;
	Animator animator;
	Transform MageDeath;
	Transform MageStage;
	public float DeathShake = 0.05f;
	cameraShake camShake;
	private int randomNum;

	public float timeToSpawnFire = 0.0f;
	public float spawnRate = 0.005f;
	public Transform MageStarPrefab;
	public Transform FireBallPrefab;
	Transform MageFirePointStar;
	Transform MageFirePointBall;

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator>();

		MageDeath = transform.Find ("MageDeath"); //Find the PlayerDamaged object
		if (MageDeath == null) {
			Debug.LogError ("No Mage death Sound"); //Return an error if not found
		}

		MageStage = transform.Find ("MageStage"); //Find the PlayerDamaged object
		if (MageStage == null) {
			Debug.LogError ("No Mage stage 1 Sound"); //Return an error if not found
		}

		MageFirePointBall = transform.Find ("MageFirePointBall"); //Find the firePos object
		if (MageFirePointBall == null) {
			Debug.LogError ("No Fire Point for boss"); //Return an error if not found
		}

		MageFirePointStar = transform.Find ("MageFirePointStar"); //Find the firePos object
		if (MageFirePointStar == null) {
			Debug.LogError ("No Fire Point for boss"); //Return an error if not found
		}
	} //Easier to debug

	void Start () {
		camShake = GameScript.gm.GetComponent<cameraShake> ();
		if (camShake == null) {
			Debug.LogError ("No Camera Shake"); //Get camera shake component, if not found, return error
		}
	}

		
	void Update () {
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			if (Vector3.Distance (GameObject.FindGameObjectWithTag ("Player").transform.position, transform.position) < 10) {
				if (hitsTaken <= 40) { //Find the player and the distance from it to decide when to shoot
					Shoot ();
				}

			}		
		}

	}

	void OnCollisionEnter2D(Collision2D coll) { //Checks for collisions
		if (coll.gameObject.tag == "bullet") {
			hitsTaken += 1; //If hit by player shot increase hit count

			if (hitsTaken == 20) { //When 20 hits taken, move into stage two
				camShake.Shake (DeathShake, 6.0f);
				MageStage.GetComponent<AudioSource> ().Play ();
			}

			if (hitsTaken >= 40) { //When 40 hits taken the boss will die
				camShake.Shake (DeathShake, 3.0f);
				MageDeath.GetComponent<AudioSource> ().Play ();
				animator.SetBool("dyingBoss", true); //Set death animation
				GameScript.KillBossTwo (this); //Kill the boss
			}
		}
	}

	void Shoot () {
		if (Time.time >= timeToSpawnFire) { //Spawn the effect for bullet and time till next
			Effect ();
			timeToSpawnFire = Time.time + 1/spawnRate;
		}
	}

	void Effect () {
		Instantiate (FireBallPrefab, MageFirePointBall.position, MageFirePointBall.rotation); //Shoot fireball at prefab

		if (hitsTaken >= 30) {
			Instantiate (MageStarPrefab, MageFirePointStar.position, MageFirePointStar.rotation); //If in second phase use star projectiles
		}
	}
}