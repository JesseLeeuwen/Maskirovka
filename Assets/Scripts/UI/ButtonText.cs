using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonText : MonoBehaviour, 
IPointerDownHandler, IPointerClickHandler, IPointerExitHandler {

	public Text text;
	public int buttonHeight = 11;
	private Vector3 offset;
	private bool isDown;

	void Awake(){
		offset = new Vector3(0,-buttonHeight,0);
	}

	public void OnPointerDown(PointerEventData eventData){
		text.transform.position= text.transform.position + offset;
		isDown = true;
	}

	public void OnPointerClick(PointerEventData eventData){
		if (isDown){Release();};
	}

	public void OnPointerExit(PointerEventData eventData){
		if (isDown){Release();};
	}


	public void Release(){
		isDown = false;
		text.transform.position= text.transform.position - offset;
	}
 


}
