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
		
		private Neighbour neighbour;

		public void Init(Neighbour neighbour)
		{
			this.neighbour = neighbour;
		}
		
		void Update () 
		{
			
		}
	}
}