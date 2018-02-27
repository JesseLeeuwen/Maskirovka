using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maskirovka
{
	public class ChaosMeter : MonoBehaviour 
	{
		[SerializeField]
		private CountryList countries;
		
		[SerializeField, Range(0, 100)]
		private float chaos;

		private int countConnections;

		void Update () 
		{
			
		}
	}
}