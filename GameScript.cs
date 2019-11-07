using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScript : MonoBehaviour {

	Transform GameOverSound;
	public static GameScript gm;

	private static int _remainingLives = 3;
	public static int RemainingLives {
		get {return _remainingLives;} //Gets the remaining lives
	}

	void Awake () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameScript>(); //Finds the Game manager
		}

		GameOverSound = transform.Find ("GameOverSound"); //Find the sound 
		if (GameOverSound == null) {
			Debug.LogError ("No Game over Sound"); //Return an error if not found
		}
	}

	[SerializeField]
	private GameObject gameOverUI;
	public GameObject WinGameUI;

	private static bool BossOneDead;
	private static bool BossTwoDead;
		
	public Transform playerPrefab;
	public Transform spawnPoint;
	public int SpawnDelay = 5;

	public void EndGame () {
		Debug.Log ("Game over");
		GameOverSound.GetComponent<AudioSource> ().Play ();
		gameOverUI.SetActive (true); //When game move the UI comes up and plays sound
	}

	public void WinGame () {
		Debug.Log ("You Won");
		WinGameUI.SetActive (true);
	}


	public IEnumerator RespawnPlayer () {
		yield return new WaitForSeconds (SpawnDelay); //Waits for a few seconds before respawn
		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation); //Spawn back in
	}

	public static void KillPlayer (Player player) { //Kill player when too much damage or fall off map
		Destroy (player.gameObject);
		_remainingLives -= 1;
		if (_remainingLives <= -1) {
			gm.EndGame ();
		} else {
			gm.StartCoroutine(gm.RespawnPlayer ());
		}
	}

	public static void KillEnemy (enemyProperties enemy) { //Destroys enemy
		Destroy (enemy.gameObject, 0.2f);
	}

	public static void KillBoss (bossOneScript boss) { //Kills boss and decides if game has been won
		Destroy (boss.gameObject, 3);
		BossOneDead = true;
		if (BossOneDead == true && BossTwoDead == true) {
			gm.WinGame (); //Brings up win UI
		}
	}

	public static void KillBossTwo (bossTwoScript boss) { //Kills boss and decides if game has been won
		Destroy (boss.gameObject, 6);
		BossTwoDead = true;

		if (BossOneDead == true && BossTwoDead == true) {
			gm.WinGame (); //Brings up win UI
		}
	}
}
