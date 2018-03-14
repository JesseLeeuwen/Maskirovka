using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slideChange : MonoBehaviour {


	private LineRenderer lr;
	public GameObject handle;
	public GameObject marker;

	// Use this for initialization
	void Start () {
		lr = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mPos = marker.transform.position;
		mPos.y=handle.transform.position.y;
		lr.SetPosition(0,mPos);
		lr.SetPosition(1,handle.transform.position);
	}
}
