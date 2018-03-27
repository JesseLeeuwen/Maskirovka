using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationManager : MonoBehaviour {

	public Object tutorial;

	public void RestartMain(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void LoadTutorial(){
		SceneManager.LoadScene(tutorial.name);
	}
	public void ExitGame(){
		Application.Quit();
	}
}
