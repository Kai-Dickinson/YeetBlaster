using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGameUI : MonoBehaviour {



	public void PlayAgain () {
		Debug.Log ("Started Again"); //Loads same level if play again has been chosen
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void Menu () { //Loads menu if menu is chosen
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex - 1);
	}
}
