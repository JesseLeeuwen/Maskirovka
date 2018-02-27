using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Maskirovka.UI
{
	public class ReputationTable : MonoBehaviour 
	{
		[Header("neighbour Settings")]
		[SerializeField]
		private GameObject neighbourInfoPrefab;
		[SerializeField]
		private RectTransform neighbourContainer;

		[Header("country Settings")]
		[SerializeField]
		private Image country;
		[SerializeField]
		private float[] reputation;
		[SerializeField]
		private RectTransform[] bars;

		public void Init( Country country )
		{	
			reputation = new float[3];
			reputation[0] = country.avarageA;
			reputation[1] = country.avarageB;
			reputation[2] = country.avarageC;

			for(int i = 0; i < neighbourContainer.childCount; ++i)
			{
				Destroy(neighbourContainer.GetChild(i).gameObject);
			}

			GameObject neighbour;
			for(int i = 0; i < country.neighbours.Length; ++i)
			{
				neighbour = Instantiate(neighbourInfoPrefab, Vector3.zero, Quaternion.identity, neighbourContainer );
				neighbour.GetComponent<NeighbourInfo>().Init( country.neighbours[i], country );
			}
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