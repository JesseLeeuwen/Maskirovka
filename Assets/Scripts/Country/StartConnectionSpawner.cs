using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maskirovka
{
	public class StartConnectionSpawner : MonoBehaviour 
	{
		[SerializeField]
		private CountryList countries;

		IEnumerator Start () 
		{
			yield return new WaitForSeconds(0.1f);

			for(int i = 0; i < countries.Length; ++i)
			{
				Country country = countries[i];
				foreach(Neighbour n in country.neighbours)
				{
					n.neighbour.UpdateRepu( country, country.avarage.x, Catagorie.A);
					n.neighbour.UpdateRepu( country, country.avarage.y, Catagorie.B);
					n.neighbour.UpdateRepu( country, country.avarage.z, Catagorie.C);
				}
			}			
		}
	}
}