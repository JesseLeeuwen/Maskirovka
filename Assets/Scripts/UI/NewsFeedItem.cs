using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using Maskirovka.News;
using Maskirovka;
using Maskirovka.Utility;
using Maskirovka.Selector;

namespace Maskirovka.UI
{
    public class NewsFeedItem : MonoBehaviour, ISelectable, IPointerEnterHandler, IPointerExitHandler
    {
        public Image icon_L;
        public Image icon_R;
        public Image portrait;
        public Slider valueSlider;
        public int chanceOfSucces;
        public bool playerChanged;
        public Text headline;
        public Text contentText;
        public float biasChanger;
        public Image ValueBar;

        //PUT THESE IN INIT
        public Country subject;
        public Catagorie catagorie;
        public float value;

        public Dictionary<Connection, bool> cache;

        public void Init(Country country, NewsData data)
        {
            this.subject = country;
            catagorie = data.catagorie;
            this.value = data.value;
            this.valueSlider.value = data.value;
            this.portrait.sprite = country.GetSprite();
            this.contentText.text = getCopy(data.catagorie, Mathf.RoundToInt(value), country, Random.Range(7,10));

            this.ValueBar.color = CatagorieSettings.GetColor(catagorie);
            this.icon_L.sprite = CatagorieSettings.GetIconLeft(catagorie);
            this.icon_R.sprite = CatagorieSettings.GetIconRight(catagorie);
            this.headline.text = country.characterName.ToUpper();
        }

		string getCopy(Catagorie cat, int value, Country country, int length){
			string[] left  = CatagorieSettings.GetKeywordsLeft(cat);
			string[] right = CatagorieSettings.GetKeywordsRight(cat);
			string line = getWord(left,right,value);
			line = char.ToUpper(line[0]) + line.Substring(1); //capitalize first letter
			for (int i = 1; i < length;i++){
				line += " ";
				line += getWord(left,right,value);
			};
            if (length>5)
			    line += ".";
			return line;
		}

		string getWord(string[] left, string[] right, int value){
			if (Random.Range(0,100) < value){
				return left[Random.Range(0,left.Length)];
			}
			return right[Random.Range(0,right.Length)];
        }

        public new SelectableType GetType()
        {
            return SelectableType.NewsFeedItem;
        }

        public bool Send(bool youcandie)
        {            
            bool result = GameManager.Instance.processor.ProccesNews(this);
            gameObject.layer = 0;
            Color c = new Color( 0.77f, 0.77f, 0.77f, 1);
            
            if( playerChanged == true )
            {
                if (result ) c.g=1;
                else        c.r=1;
            }
            GetComponent<Image>().color = c;
            return result;      
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Color c = CatagorieSettings.GetColor(catagorie);
            c += Color.white;
            subject.Highlight( (c)/2, true);

            cache = new Dictionary<Connection, bool>();
            foreach( Connection con in subject.GetConnections() )
            {
                cache.Add( con, con.IsActive() );
                con.Activate(true);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            subject.Highlight( Color.white, false);
            
            foreach( Connection con in cache.Keys )
                con.Activate( cache[con] );
        }
    }
}