using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Maskirovka.UI;
using Maskirovka;

public class TutorialOverlays : MonoBehaviour {


	public GameObject[] canvas;
	
	public UImanager invasion;
	public GameObject endScreen;
	public GameObject nextButton;
	public GameObject newspanel;
	private NewsPanel panelScript;

	public ReputationTable reputationTable;

	private Connection[] connections;

	public int currentSlide = 0;

	void Start(){
		nextButton.SetActive(true);
		connections = FindObjectsOfType<Connection>();
		setCanvas(currentSlide);
		panelScript = newspanel.GetComponent<NewsPanel>();
	}

	void Update () {
		switch (currentSlide){
			case 0:
				if (connections.Length<1){
					connections = FindObjectsOfType<Connection>();
					for (int i =0; i<connections.Length;i++){
						connections[i].gameObject.SetActive(false);
					};
				};
				break;
			case 2:
				for (int i =0; i<connections.Length;i++){
					connections[i].gameObject.SetActive(true);
				};
				break;
			case 3:
				requiredAction(reputationTable.charName.text=="Baker Yellow");
				break;
			case 6:
				requiredAction(newspanel.activeInHierarchy);
				break;
			case 11:
				requiredAction(panelScript.currentValue>panelScript.startValue);
				break;
			case 12:
				requiredAction(!newspanel.activeInHierarchy);
				break;
			default:
			break;
		}
	}

	void requiredAction(bool action){
		nextButton.SetActive(false);
		if (action){
			nextButton.SetActive(true);
			nextSlide();
		}	
	}

	void setCanvas(int c){
		if (!canvas[c].activeInHierarchy){
			for (int i=0; i < canvas.Length; i++){
				canvas[i].SetActive(false);
			}
			canvas[c].SetActive(true);
		}
	}

	public void nextSlide(){
		currentSlide++;
		setCanvas(currentSlide);
	}
}
