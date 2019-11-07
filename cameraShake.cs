using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour {
	public Camera mainCam;

	private float shakeAmount = 0; //Base amount

	void Awake () {
		if (mainCam == null) {
			mainCam = Camera.main; //Set the camera
		}
	}

	public void Shake (float amount, float length) {
		shakeAmount = amount;
		InvokeRepeating ("BeginShake", 0, 0.01f); //Repeats the beginShke until invoking stop shake
		Invoke ("StopShake", length);
	} 

	void BeginShake () {
		if (shakeAmount > 0) {
			Vector3 camPos = mainCam.transform.position;
			float shakeX = Random.value * shakeAmount * 2 - shakeAmount; //Shakes the camera in random directions
			float shakeY = Random.value * shakeAmount * 2 - shakeAmount;
			camPos.x += shakeX;
			camPos.y += shakeY;

			mainCam.transform.position = camPos; //Put camera back to start
		}
	}

	void StopShake () {
		CancelInvoke ("BeginShake");
		mainCam.transform.localPosition = Vector3.zero;
	}
}
