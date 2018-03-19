using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maskirovka.UI
{
	public class TwitterFeed : MonoBehaviour 
	{
		[SerializeField]
		private GameObject prefab;
		[SerializeField]
		private RectTransform content;

		public void NewTweet(NewsData newMessage)
		{
			GameObject n = Instantiate(prefab);
			n.transform.SetParent( transform, false);
			n.transform.SetAsFirstSibling( );

			content.sizeDelta = new Vector2( content.sizeDelta.x, content.childCount * 140 );			
		}

		void Update()
		{
			
		}
	}
}