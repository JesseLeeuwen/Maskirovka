using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Maskirovka.News;

public class InvasionManager : MonoBehaviour {

public GameObject WinScreen;
private NewsManager manager;

public bool win;

public void Start(){
	manager = GetComponent<NewsManager>();
}

public void InvadeAttempt(){

		for (int i=0; i<10;i++){
		GameObject anim= manager.StateAnimation(win);
		anim.transform.localScale= new Vector3(.5f,.5f,.5f);
		anim.transform.position += Random.insideUnitSphere * 5;
		}
		WinScreen.SetActive(win);
}

}
