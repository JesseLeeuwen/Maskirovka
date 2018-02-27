using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Maskirovka.UI
{

	public class ReputationTable : MonoBehaviour 
	{
		[SerializeField]
		private Image country;
		[SerializeField]
		private float[] reputation;
		[SerializeField]
		private RectTransform[] bars;

		public void Init( Country country )
		{
			for(int i = 0; i < 3; ++i)
				reputation[i] = Random.value * 100;
		}
		
		void Update () 
		{
			for(int i = 0; i < 3; ++i)
			{
				float height = bars[i].sizeDelta.y;
				Vector2 target = new Vector2( reputation[i] * 1.80f, height );
				bars[i].sizeDelta = Vector2.MoveTowards( bars[i].sizeDelta, target, Time.deltaTime * 50.0f );
			}
		}
	}
}