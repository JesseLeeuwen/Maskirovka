using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Maskirovka.News;
using Maskirovka;
using Maskirovka.Utility;

namespace Maskirovka.UI
{
    public class NewsFeedItem : MonoBehaviour
    {
        public Image portrait;
        public Slider valueSlider;
        public int chanceOfSucces;
        public bool playerChanged;
        public Text contentText;
        public float biasChanger;

        //PUT THESE IN INIT
        public Country subject;
        public Catagorie catagorie;
        public float value;


        public void Init(Country country, NewsData data)
        {
            this.subject = country;
            this.catagorie = data.catagorie;
            this.value = data.value;
            this.valueSlider.value = data.value;
            this.portrait.sprite = country.GetSprite();
            this.contentText.text = getCopy(data.catagorie, Mathf.RoundToInt(value), country);

            Color a = CatagorieSettings.GetColor(catagorie);

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

        public bool Send(bool youcandie)
        {
            //if(youcandie == false)
            //{
            bool result = GameManager.Instance.processor.ProccesNews(this);
            Destroy(gameObject);
            return result;
            //}

            /*if(youcandie == true)
            {*/
            //}
            //return false;
        }
    }
}