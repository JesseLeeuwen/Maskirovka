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

		string getCopy(Catagorie cat, int value, Country country){
			string[] left  = CatagorieSettings.GetKeywordsLeft(cat);
			string[] right = CatagorieSettings.GetKeywordsRight(cat);
			string line = getWord(left,right,value);
			line = char.ToUpper(line[0]) + line.Substring(1); //capitalize first letter
			for (int i = 0; i < Random.Range(12,18);i++){
				line += " ";
				line += getWord(left,right,value);
			};
			line += ".";
			return line;

		}

		string getWord(string[] left, string[] right, int value){
			if (Random.Range(0,100) < value){
				return left[Random.Range(0,left.Length)];
			}
			return right[Random.Range(0,right.Length)];
		}
	}


	
}