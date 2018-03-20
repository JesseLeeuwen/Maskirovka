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

		public NewsFeedItem NewTweet(NewsData newMessage, Country country)
		{
			GameObject n = Instantiate(prefab);
			n.transform.SetParent( transform, false);
			n.transform.SetAsFirstSibling( );

            NewsFeedItem item = n.GetComponent<NewsFeedItem>();
            item.Init(country, newMessage);

            content.sizeDelta = new Vector2( content.sizeDelta.x, content.childCount * 140 );

            return item;
		}

		void Update()
		{
			
		}
	}
}