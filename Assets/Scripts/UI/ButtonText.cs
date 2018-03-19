using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonText : MonoBehaviour, 
IPointerDownHandler, IPointerClickHandler, IPointerExitHandler {

	public Text text;
	private bool isDown;

	new private RectTransform transform;
	private float defaultOffset;

	void Awake()
	{	
		transform = text.transform as RectTransform;
		defaultOffset = transform.offsetMin.y;		
	}

	public void OnPointerDown(PointerEventData eventData)
	{			
		transform.offsetMax = new Vector2(transform.offsetMax.x, -defaultOffset);
		transform.offsetMin = new Vector2(transform.offsetMin.x, 0);
		isDown = true;
	}

	public void OnPointerClick(PointerEventData eventData){
		if (isDown){Release();};
	}

	public void OnPointerExit(PointerEventData eventData){
		if (isDown){Release();};
	}

	public void Release()
	{
		transform.offsetMax = new Vector2(transform.offsetMax.x, 0);
		transform.offsetMin = new Vector2(transform.offsetMin.x, defaultOffset);
		isDown = false;
	}
}
