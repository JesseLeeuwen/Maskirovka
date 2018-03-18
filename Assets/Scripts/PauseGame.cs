using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour {

	public bool paused = false;
	public GameObject pauseMenu;
	public GameObject pauseBackground;

	void Start(){

	}
	
    void TaskOnClick()
    {
        TogglePause();
    }

	public void TogglePause(){
		SetPaused(!paused);
	}


	public void SetPaused(bool state){
		paused = state;
		pauseBackground.SetActive(state);
		pauseMenu.SetActive(state);
		if (state){
			Time.timeScale=0f;
		}else{
			Time.timeScale=1f;
		}

	}
}
