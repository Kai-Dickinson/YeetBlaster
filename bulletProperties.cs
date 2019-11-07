using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletProperties : MonoBehaviour {

	public int bulletSpeed = 20; //Set speed
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * Time.deltaTime * bulletSpeed);
		Destroy (gameObject, 0.3f); //Destroy bullet after certain time
	}


	void OnCollisionEnter2D(Collision2D coll) { //Checks for collisions
		if (coll.gameObject.tag == "Enemy") {
			Destroy (gameObject); //If hits object destroy bullet
		}

		if (coll.gameObject.tag == "boss1") {
			Destroy (gameObject);
		}

		if (coll.gameObject.tag == "boss2") {
			Destroy (gameObject);
		}

		if (coll.gameObject.tag == "ground") {
			Destroy (gameObject);
		}
	}
}
