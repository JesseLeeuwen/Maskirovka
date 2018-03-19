using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maskirovka
{
	[CreateAssetMenu(menuName="CountryList")]
	public class CountryList : ScriptableObject 
	{
		private List<Country> countries;

		public void Init(Country[] countries)
		{
			this.countries = new List<Country>(countries);
		}

		public void Remove( Country country )
		{
			countries.Remove( country );
		}

		public int Length{
			get{ return countries.Count; }
		}

		public Country this[int key]{
			get { return countries[key]; }
		}
	}
}