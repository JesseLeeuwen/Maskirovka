using UnityEngine;
using UnityEngine.UI;

using System.Collections.Generic;

using Maskirovka.News;
using Maskirovka.Utility;

using FMODCLONE;

namespace Maskirovka.UI
{
    public class NewsPanel : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        public Image iconLeft;
        public Image iconRight;

        public NewsFeedItem currentNews;
        public int chanceOfSucces;
        public float startValue, currentValue;


        public GameObject[] backgrounds;
        public GameObject[] texts;

        public Text ChanceDisplay;

        public GameObject countryCircle;
        public GameObject[] neighborCircle;

        private List<float> prevV = new List<float>();


        public void Init( NewsFeedItem news)
        {
            gameObject.SetActive( true );
            
            currentNews = news;
            chanceOfSucces = news.chanceOfSucces;
            startValue = currentValue = news.value;
            slider.value = startValue;
            iconLeft.sprite = CatagorieSettings.GetIconLeft(news.catagorie);
            iconRight.sprite = CatagorieSettings.GetIconRight(news.catagorie);

            
            Color c = CatagorieSettings.GetColor(news.catagorie);
            foreach (GameObject bg in backgrounds){
                bg.GetComponent<Image>().color = c;
            }
            foreach (GameObject t in texts){
                t.GetComponent<Text>().color = c;
            }
            countryCircle.transform.GetChild(0).GetComponent<Image>().sprite = news.subject.GetSprite();

           Vector3 markerPos = countryCircle.GetComponent<RectTransform>().anchoredPosition;
           markerPos.x = GetReputation(news.catagorie, news.subject);
           countryCircle.GetComponent<RectTransform>().anchoredPosition = markerPos;

            foreach (GameObject n in neighborCircle){
                n.SetActive(false);
            }
            int i =0;
            
            
            prevV.Clear();
            
            foreach (Neighbour n in news.subject.neighbours){
                float v = 0;
                neighborCircle[i].SetActive(true);
                Image img = neighborCircle[i].transform.GetChild(0).GetComponent<Image>();
                img.sprite = news.subject.neighbours[i].neighbour.GetSprite();
                if (news.catagorie==Catagorie.A){
                    v = n.reputation.x;
                }else if(news.catagorie==Catagorie.B){
                    v = n.reputation.y;
                }else if (news.catagorie==Catagorie.C){
                    v = n.reputation.z;
                }

                markerPos = img.GetComponent<RectTransform>().anchoredPosition;
                markerPos.y=15;
                bool move = false;
                foreach(float value in prevV){
                    if (Mathf.Abs(value-v)<7){
                        markerPos.y=35;
                        move = true;
                        break;
                    }
                }
                img.GetComponent<RectTransform>().anchoredPosition = markerPos;
                if (!move) prevV.Add(v);



                markerPos = neighborCircle[i].GetComponent<RectTransform>().anchoredPosition;
                markerPos.x = v * slider.GetComponent<RectTransform>().sizeDelta.x/100;
                
                
                neighborCircle[i].GetComponent<RectTransform>().anchoredPosition = markerPos;
                i++;
            }
            
        }

        public float GetReputation(Catagorie cat,  Country country){
            float output = 0;
            if (cat==Catagorie.A){
                output = country.avarage.x;
            }else if(cat==Catagorie.B){
                output = country.avarage.y;
            }else if (cat==Catagorie.C){
                output = country.avarage.z;
            }
            //return 300;
            return output * slider.GetComponent<RectTransform>().sizeDelta.x/100;
        }

        public void OnChangeSlider( float value )
        {
            currentValue = value;
            chanceOfSucces = Mathf.Max(0,Mathf.RoundToInt(100 - ( Mathf.Abs( value - startValue ) * 2f ) ));
            ChanceDisplay.text=chanceOfSucces.ToString()+"%";
        }

        public void OnSend()        
        {
            AudioManager.PlayClip("Send_news"); 
            currentNews.playerChanged = true;
            currentNews.value = currentValue;
            if (chanceOfSucces==69)
                chanceOfSucces=100; //nice
            currentNews.chanceOfSucces = chanceOfSucces;
        }
    }
}