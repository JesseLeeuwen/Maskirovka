using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class pulsatingText : MonoBehaviour {


	private Text text;
	private Color color;
	private float time;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		color=text.color;
	}
	
	// Update is called once per frame
	void Update () {
		
		Color a = color;
		a.a = (Mathf.Cos(3*Time.fixedTime)+3)/4;
		text.color=a;
		
	}
}
