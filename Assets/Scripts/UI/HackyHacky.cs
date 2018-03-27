using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Maskirovka.News;
using Maskirovka;
using Maskirovka.Utility;

using Maskirovka.Selector;

namespace Maskirovka.UI
{
    public class HackyHacky : MonoBehaviour {
        public Catagorie whatYouWant;
        private NewsFeedItem news;

        // Update is called once per frame
        void Start() {
            news = GetComponent<NewsFeedItem>();
            news.catagorie = whatYouWant;
            news.contentText.text = getCopy(whatYouWant, Mathf.RoundToInt(news.value), news.subject, Random.Range(7, 10));
            news.ValueBar.color = CatagorieSettings.GetColor(whatYouWant);
            news.icon_L.sprite = CatagorieSettings.GetIconLeft(whatYouWant);
            news.icon_R.sprite = CatagorieSettings.GetIconRight(whatYouWant);
        }

        string getCopy(Catagorie cat, int value, Country country, int length)
        {
            string[] left = CatagorieSettings.GetKeywordsLeft(cat);
            string[] right = CatagorieSettings.GetKeywordsRight(cat);
            string line = getWord(left, right, value);
            line = char.ToUpper(line[0]) + line.Substring(1); //capitalize first letter
            for (int i = 1; i < length; i++)
            {
                line += " ";
                line += getWord(left, right, value);
            };
            if (length > 5)
                line += ".";
            return line;
        }

        string getWord(string[] left, string[] right, int value)
        {
            if (Random.Range(0, 100) < value)
            {
                return left[Random.Range(0, left.Length)];
            }
            return right[Random.Range(0, right.Length)];
        }

    }
}
