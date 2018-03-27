using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement : MonoBehaviour 
{
	private bool unlocked;

	void OnEnable () 
	{
		if( unlocked == true )
			gameObject.SetActive(false);		

		unlocked = true;
	}
}
