using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour {

    public Object nextScene;
	
	public void loadNext(){
		SceneManager.LoadScene(nextScene.name);
	}
}
