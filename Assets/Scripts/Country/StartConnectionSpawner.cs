using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maskirovka
{
	public class StartConnectionSpawner : MonoBehaviour 
	{
		[SerializeField]
		private Country[] countries;

		IEnumerator Start () 
		{
			yield return new WaitForSeconds(0.1f);
			
			foreach(Country country in countries)
			{
				foreach(Neighbour n in country.neighbours)
				{
					n.neighbour.UpdateRepu( country, country.avarageA, Catagorie.A);
					n.neighbour.UpdateRepu( country, country.avarageB, Catagorie.B);
					n.neighbour.UpdateRepu( country, country.avarageC, Catagorie.C);
				}
			}			
		}
	}
}