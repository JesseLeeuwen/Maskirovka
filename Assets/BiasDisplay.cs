using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Maskirovka.News;

public class BiasDisplay : MonoBehaviour {

	//REALLY QUICK AND DIRTY BIAS DISPLAY
	//DO NOT SHIP

	public NewsManager manager;
	public Slider A;
	public Slider B;
	public Slider C;
	
	// Update is called once per frame
	void Update () {
		A.value=0;
		setSlider(A,manager.bias.x);
		setSlider(B,manager.bias.y);
		setSlider(C,manager.bias.z);
	}

	void setSlider(Slider neg, float v){
		v-=50;
		Slider pos = neg.transform.GetChild(0).GetComponent<Slider>();
		neg.value = Mathf.Max(-v,0);
		pos.value = Mathf.Max( v,0);
		
	}

}
