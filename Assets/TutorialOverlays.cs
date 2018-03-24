using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Maskirovka.UI;

public class TutorialOverlays : MonoBehaviour {


	public GameObject[] canvas;
	public GameObject newspanel;
	public UImanager invasion;
	public GameObject endScreen;

	void Update () {
		if (newspanel.activeInHierarchy){
			setCanvas(1);
		}else if(invasion.IsInvasionMode()){
			setCanvas(2);

		}else{
			setCanvas(0);
		}
		if (endScreen.activeInHierarchy)
			Destroy(gameObject);
	}


	void setCanvas(int c){
		if (!canvas[c].activeInHierarchy){
			for (int i=0; i < canvas.Length; i++){
				canvas[i].SetActive(false);
			}
			canvas[c].SetActive(true);
		}
	}
}
