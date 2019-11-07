using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LivesCounter : MonoBehaviour {

	private Text lives;

	// Use this for initialization
	void Start () {
		lives = GetComponent<Text> ();
	}

	
	// Update is called once per frame
	void Update () {
		lives.text = "LIVES: " + GameScript.RemainingLives; //Updates the lives left to be displayed on UI
	}
}
