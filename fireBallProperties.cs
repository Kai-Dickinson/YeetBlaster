using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBallProperties : MonoBehaviour {

public int bulletSpeed = 1;

// Update is called once per frame
void Update () {

	Vector3 playerPos = new Vector3 (GameObject.FindGameObjectWithTag ("Player").transform.position.x, GameObject.FindGameObjectWithTag ("Player").transform.position.y, 0f);
	Vector3 currentPos = new Vector3 (transform.position.x, transform.position.y, 0f); //Changing position toward player

	Vector3 finalVec = playerPos - currentPos;
	transform.Translate (finalVec * Time.deltaTime * bulletSpeed * 0.1f);
	Destroy (gameObject, 5.0f);
}


void OnCollisionEnter2D(Collision2D coll) { //Checks for collisions
	if (coll.gameObject.tag == "Player") {
		Destroy (gameObject);
	}
}
}
