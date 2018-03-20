using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideShow : MonoBehaviour {

	public Sprite[] slides;
	private Image canvas;
	private int currentSlide = 0;

	// Use this for initialization
	void Start () {
		ResetTutorial();
	}
	
	public void NextSlide()
    {
		currentSlide ++;
		if (currentSlide<slides.Length){
			canvas.sprite= slides[currentSlide];
		}else{
			gameObject.SetActive(false);
		}
		
	}

	public void ResetTutorial(){
		gameObject.SetActive(true);
		canvas = GetComponent<Image>();
		currentSlide=0;
		canvas.sprite= slides[0];
		canvas.color= new Color(1,1,1,.9f);
	}
}
