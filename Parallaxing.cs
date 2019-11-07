using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgrounds; //Array of backgrounds
	private float[] pScales;
	public float parallaxAmount = 1;

	private Transform cam;
	private Vector3 previousCamPos;

	//Before Start()
	void Awake () {
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		previousCamPos = cam.position; //Set position at this moment in time

		pScales = new float[backgrounds.Length];
		//Assigning parallax scales
		for (int i = 0; i < backgrounds.Length; i++) {
			pScales [i] = backgrounds [i].position.z * -1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		for (int i = 0; i < backgrounds.Length; i++) {
			float parallax = (previousCamPos.x - cam.position.x) * pScales [i]; //Changes how the camera moves against each background
			float backgroundTargetPosX = backgrounds [i].position.x + parallax;
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds [i].position.y, backgrounds [i].position.z);

			//Gradual change between positions
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, parallaxAmount * Time.deltaTime);
		}

		previousCamPos = cam.position;
	}

}
