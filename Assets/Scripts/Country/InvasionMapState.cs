using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maskirovka
{
	public class InvasionMapState : MonoBehaviour 
	{
		[SerializeField]
		private CountryList list;
		[SerializeField]
		private Animator animator;		
		[SerializeField]
		private Russia russia;
		[SerializeField]
		private ChaosMeter chaos;
		[SerializeField]
		private Gradient invadeAbleOutline;

		public void UpdateInvasionMapState(bool active)
		{
			animator.SetBool( "Hide", active );
			transform.GetChild( transform.childCount - 1 ).gameObject.SetActive( !active );

			Country country;
			for( int i = 0; i < list.Length; ++i)
			{
				country = list[i];
				int isNeighbour = System.Convert.ToInt16( russia.IsNeighbour( country ) );
				int canInvade = System.Convert.ToInt16( chaos.CanInvade( country ) ) * 2;
				// get colors for gradients				
				Color colorInline = invadeAbleOutline.colorKeys[isNeighbour+canInvade].color;
				// Set invasion mode for country
				country.InvadeMode(colorInline, active );
			}
		}
	}
}