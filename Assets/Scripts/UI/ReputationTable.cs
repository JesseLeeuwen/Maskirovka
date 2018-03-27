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
		public Text charName;
		[SerializeField]
		private float[] reputation;
		[SerializeField]
		private RectTransform[] bars;		
		[SerializeField]
		private float animationTime;

		private float time;

		public void Init( Country country )
		{	
			Sprite icon = country.GetSprite();
			if(icon == this.country.sprite) return;

			reputation = new float[3];
			time = 0;
			
			reputation[0] = country.avarage.x;
			reputation[1] = country.avarage.y;
			reputation[2] = country.avarage.z;

			for(int i = 0; i < neighbourContainer.childCount; ++i)
			{
				Destroy(neighbourContainer.GetChild(i).gameObject);
			}

			GameObject neighbour;
			for(int i = 0; i < country.neighbours.Length; ++i)
			{
				neighbour = Instantiate(neighbourInfoPrefab, Vector3.zero, Quaternion.identity, neighbourContainer );
				neighbour.transform.localPosition = Vector3.zero;				
				neighbour.GetComponent<NeighbourInfo>().Init( country.neighbours[i], country );
			}
			
			this.country.sprite = icon;
			this.charName.text = country.characterName;
		}
		
		void Update () 
		{
			time += Time.deltaTime;
			for(int i = 0; i < 3; ++i)
			{
				float height = bars[i].anchoredPosition.y;
				Vector2 target = new Vector2( reputation[i] * 1.80f, height );				
				bars[i].anchoredPosition = Vector2.Lerp( bars[i].anchoredPosition, target, time / animationTime );
			}
		}
	}
}