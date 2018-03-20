using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Maskirovka;
using Maskirovka.Utility;

namespace Maskirovka.UI
{
	public class NewsFeedItem: MonoBehaviour 
	{



		public Image portrait;
		public Text copy;
		public Slider valueSlider;

		//PUT THESE IN INIT
		public Country subject;
		public Catagorie catagorie;
		public int value;

		public void Init(Change change, Sprite connection){
			

		}
	}
}