using UnityEngine;
using UnityEngine.UI;

using Maskirovka.News;

namespace Maskirovka.UI
{
    public class NewsPanel : MonoBehaviour
    {
        [SerializeField]
        private Slider slider;
        private News.News currentNews;
        private float startValue;

        void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Init( News.News news)
        {
            gameObject.SetActive( true );
            
            currentNews = news;
            startValue = news.value;
            slider.value = startValue;
        }

        public void OnChangeSlider( float value )
        {
            currentNews.value = value;
            currentNews.chanceOfSucces = Mathf.RoundToInt(80 - ( ( value - startValue ) * 1.5f ));
        }
    }
}