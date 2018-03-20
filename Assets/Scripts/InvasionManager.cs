﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Maskirovka.News;
using Maskirovka;

public class InvasionManager : MonoBehaviour {

	public GameObject WinScreen;
	public bool win;
	
	[SerializeField]
	private CountryList countries;

	[SerializeField]
	private Color russiaColor;

	private NewsManager manager;
	private ChaosMeter chaos;

	public void Start(){
		manager = GetComponent<NewsManager>();
		chaos = GetComponent<ChaosMeter>();
	}

	public void InvadeAttempt(Country country)
	{
		win = chaos.CanInvade( country );

		if( win )
		{			
			countries.Remove( country );
			country.Invaded(russiaColor);

			/*for (int i=0; i<10;i++)
			{
				GameObject anim = manager.StateAnimation(win);
				anim.transform.localScale = new Vector3(.5f,.5f,.5f);
				anim.transform.position += Random.insideUnitSphere * 5;
			}
			WinScreen.SetActive(win);*/
		}
	}
}
