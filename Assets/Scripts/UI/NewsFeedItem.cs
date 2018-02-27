using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Maskirovka.UI
{
	public class NewsFeedItem : MonoBehaviour 
	{
		[SerializeField]
		private Image[] countries;
		[SerializeField]
		private Image connection;

		public void Init(Change change, Sprite connection)
		{
			// setup images
			countries[0].sprite = change.countryA.GetSprite();
			countries[1].sprite = change.countryB.GetSprite();
			this.connection.sprite = connection;
		}
	}
}