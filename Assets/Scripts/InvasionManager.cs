using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Maskirovka.News;
using Maskirovka;

public class InvasionManager : MonoBehaviour {

	public GameObject WinScreen;
	public GameObject LoseScreen;
	public bool canInvade;
	public int turnsToInvade;

	[SerializeField]
	private Russia russia;
	[SerializeField]
	private CountryList countries;

	private NewsManager manager;
	private ChaosMeter chaos;

	public void Start(){
		manager = GetComponent<NewsManager>();
		chaos = GetComponent<ChaosMeter>();
	}

	public void CheckTurns(int turns){
		LoseScreen.SetActive(!canInvade && turns>=turnsToInvade && turnsToInvade>0);
	}

	public void InvadeAttempt(Country country)
	{
		canInvade = chaos.CanInvade( country ) && russia.InvadeCountry( country );

		// invade selected country
		if( canInvade == true )
		{
			// conquered feedback
			manager.ConqueredFeedback(canInvade);
			countries.Remove( country );
		}
		
		// spawn feedback win 
		if( countries.Length == 0 )
		{
			for (int i=0; i<10;i++)
			{
				GameObject anim = manager.StateAnimation(canInvade);
				anim.transform.localScale = new Vector3(.5f,.5f,.5f);
				anim.transform.position += Random.insideUnitSphere * 5;
			}
			WinScreen.SetActive(canInvade);
		}
	


	}

	
}
