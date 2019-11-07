using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[System.Serializable]
	public class PlayerStats {
		public int playerHealth = 100; //Set health stat
	}
		
	public PlayerStats playerStats = new PlayerStats(); //Create instance of my class

	void Update() {
		if (transform.position.y <= -10) {
			DamagePlayer (999);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) { //Checks for collisions
		if (coll.gameObject.tag == "Enemy") { //If tag is Enemy, set off trigger GotHit
			DamagePlayer (10);
		}
		if (coll.gameObject.tag == "boss1") { //If tag is Enemy, set off trigger GotHit
			DamagePlayer (20);
		}
		if (coll.gameObject.tag == "boss2Fire") { //If tag is Enemy, set off trigger GotHit
			DamagePlayer (25);
		}
	}

	public void DamagePlayer (int damage) {
		playerStats.playerHealth -= damage;
		if (playerStats.playerHealth <= 0) { //If health is 0 player dies
			GameScript.KillPlayer (this);

		}
	}
}
