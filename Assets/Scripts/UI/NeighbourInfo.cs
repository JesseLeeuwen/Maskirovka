using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Maskirovka.UI
{
	public class NeighbourInfo : MonoBehaviour 
	{
		[SerializeField]
		private Image icon;
		[SerializeField]
		private RectTransform[] bars;
		
		private Neighbour neighbour = new Neighbour();
		private Vector3 reputation;

		[SerializeField]
		private float animationSpeed = 400;

		public void Init(Neighbour neighbour, Country country)
		{
			icon.sprite = neighbour.neighbour.GetSprite();
			reputation = country.avarage;
			this.neighbour = neighbour;
			reputation = reputation - neighbour.reputation;
		}

		void Update () 
		{
			LerpBar(bars[0], reputation.x);
			LerpBar(bars[1], reputation.y);
			LerpBar(bars[2], reputation.z);
		}

		private void LerpBar(RectTransform bar, float value)
		{			
			value = Mathf.Clamp(value * 0.5f, -50, 50);
			if( value < 0)
			{
				Vector2 target = new Vector2( 50 - Mathf.Abs(value), bar.offsetMin.y );
				bar.offsetMin = Vector2.MoveTowards( bar.offsetMin, target, Time.deltaTime * animationSpeed );
			}
			else
			{
				Vector2 target = new Vector2( -50 + value, bar.offsetMax.y );
				bar.offsetMax = Vector2.MoveTowards( bar.offsetMax, target, Time.deltaTime * animationSpeed );
			}	
		}
	}
}