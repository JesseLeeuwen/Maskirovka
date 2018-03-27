using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maskirovka.UI
{
	public class TwitterFeed : MonoBehaviour 
	{
		[SerializeField]
		private GameObject normalPrefab;
        [SerializeField]
        private GameObject tuturialPrefab;
		[SerializeField]
		private RectTransform content;

        private int Amount;
        [Header("Max to spawn of set categorie")]
        public int maxToSpawn;

		public NewsFeedItem NewTweet(NewsData newMessage, Country country)
		{
            GameObject n;
            if(Amount < maxToSpawn)
                n = Instantiate(tuturialPrefab);
            else
                n = Instantiate(normalPrefab);

            n.transform.SetParent(transform, false);
            n.transform.SetAsFirstSibling();

            NewsFeedItem item = n.GetComponent<NewsFeedItem>();
            item.Init(country, newMessage);

            content.anchoredPosition = Vector2.zero;
            content.sizeDelta = new Vector2(content.sizeDelta.x, content.childCount * 140);
            Amount++;
            return item;
		}
	}
}