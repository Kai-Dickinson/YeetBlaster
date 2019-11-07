using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {

	public void Quit () { //Quits the game if player chooses quit
		Debug.Log ("Quit");
		Application.Quit ();
	}

	public void Retry () {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex); //Loads same level if retry is chosen
	}

}
