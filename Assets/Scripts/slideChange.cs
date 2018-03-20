using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slideChange : MonoBehaviour {

	public GameObject marker;
	public GameObject handle;
	private RectTransform trans;

	void Start(){
		trans = GetComponent<RectTransform>();
	}
	// Update is called once per frame
	void Update () {
		float xMarker = marker.GetComponent<RectTransform>().position.x;
		float xHandle = handle.GetComponent<RectTransform>().position.x;

		Vector2 pos = trans.position;
		pos.x = Mathf.Min(xMarker,xHandle);
		trans.position=pos;	
	
		Vector2 size= trans.sizeDelta;
		size.x = Mathf.Max(xMarker,xHandle)-pos.x;
		trans.sizeDelta = size;

		/* 
		float x = Mathf.Max(marker.transform.position.x,)
		Vector3 mPos = marker.transform.position;
		mPos.y=handle.transform.position.y;
		lr.SetPosition(0,mPos);
		lr.SetPosition(1,handle.transform.position);
		*/
	}
}
