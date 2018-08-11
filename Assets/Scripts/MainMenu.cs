using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void PlayGame () 
	{
		Debug.Log("Moving to playgame");
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void QuitGame ()
	{ 
		Debug.Log("Thank you for playing Wing Commander");
		Application.Quit();
	}

	public void MoveOptions () 
	{

	}
}
