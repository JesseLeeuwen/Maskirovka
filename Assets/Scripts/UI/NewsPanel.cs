using UnityEngine;
using UnityEngine.UI;

using Maskirovka.News;
using Maskirovka.Utility;

namespace Maskirovka.UI
{
    public class NewsPanel : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        private News.News currentNews;
        private int chanceOfSucces;
        private float startValue, currentValue;


        public GameObject[] backgrounds;
        public GameObject[] texts;

        public GameObject countryCircle;
        public GameObject[] neighborCircle;


        public void Init( News.News news)
        {
            gameObject.SetActive( true );
            
            currentNews = news;
            chanceOfSucces = news.chanceOfSucces;
            startValue = currentValue = news.value;
            slider.value = startValue;

            
            Color c = CatagorieSettings.GetColor(news.catagorie);
            foreach (GameObject bg in backgrounds){
                bg.GetComponent<Image>().color = c;
            }
            foreach (GameObject t in texts){
                t.GetComponent<Text>().color = c;
            }
            countryCircle.transform.GetChild(0).GetComponent<Image>().sprite = news.country.GetSprite();

           Vector3 markerPos = countryCircle.GetComponent<RectTransform>().anchoredPosition;
           markerPos.x = GetReputation(news.catagorie, news.country);
           countryCircle.GetComponent<RectTransform>().anchoredPosition = markerPos;

            foreach (GameObject n in neighborCircle){
                n.SetActive(false);
            }
            int i =0;
            
            
            
            foreach (Neighbour n in news.country.neighbours){
                float v = 0;
                neighborCircle[i].SetActive(true);
                Image img = neighborCircle[i].transform.GetChild(0).GetComponent<Image>();
                img.sprite = news.country.neighbours[i].neighbour.GetSprite();
                if (news.catagorie==Catagorie.A){
                    v = n.reputation.x;
                }else if(news.catagorie==Catagorie.B){
                    v = n.reputation.y;
                }else if (news.catagorie==Catagorie.C){
                    v = n.reputation.z;
                }
                markerPos = neighborCircle[i].transform.position;
                markerPos.x = v * slider.GetComponent<RectTransform>().sizeDelta.x/100;
                neighborCircle[i].transform.position=markerPos;

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
            chanceOfSucces = Mathf.RoundToInt(80 - ( Mathf.Abs( value - startValue ) * 1.5f ) );
        }

        public void OnSend()        
        {
            currentNews.playerChanged = true;
            currentNews.value = currentValue;
            currentNews.chanceOfSucces = chanceOfSucces;
        }
    }
}