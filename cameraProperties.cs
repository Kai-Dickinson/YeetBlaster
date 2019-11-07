using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraProperties : MonoBehaviour {

	public Transform target;   
	public Camera mainCam;
	public float smoothSpeed = 0.125f;
	public Vector3 offset;
	public Vector3 cameraOut;

	private float nextTimeSearch = 0;

	void Awake() {
		if (mainCam == null) {
			mainCam = Camera.main;
		}
	}

	void LateUpdate() {
		if (target == null) {
			FindPlayer (); //If no target find the player
			return;
		}
		Vector3 cameraUp = new Vector3 (0f,0f,5f);

		Vector3 desiredPosition = target.position + cameraUp + offset;
		Vector3 smoothedPosition = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);

		transform.position = smoothedPosition; //Move the position to where the player is smoothly
	}
	void FindPlayer () {
		if (nextTimeSearch <= Time.time) {
			GameObject PlayerSearch = GameObject.FindGameObjectWithTag ("Player"); //Called at all times, even after player death to re-find
			if (PlayerSearch != null) {
				target = PlayerSearch.transform;
			}
			nextTimeSearch = Time.time + 0.5f;
		}
	}
}



