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
		private bool cluster;
		private int countConnections;

		void Start()
		{
			GameManager.Instance.feed.SubToNewsEvents( OnReceiveChange );
		}

		public void OnReceiveChange( Change change )
		{
			countConnections += change.madeNewConnection? 1 : -1;
			cluster = SeekCluster( change.countryA );
		}

		private bool SeekCluster(Country country)
		{
			



			return false;
		}
	}
}