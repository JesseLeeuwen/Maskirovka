using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  Maskirovka.UI;
using Maskirovka.Utility;

namespace Maskirovka.News
{
    public class NewsManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject newsPrefab;
        [SerializeField]
        private CountryList countries;
        private Queue<News> activeNewsItems;
        public GameObject SuccessAnimation;
        public GameObject FailedAnimation;
        public Canvas canvas;
        public Vector3 bias;
        private bool result;
        public NewsPanel newsPanel;
        [SerializeField]
        private 


        void Start()
        {            
            activeNewsItems = new Queue<News>();
            CreateNews();
            bias = new Vector3(50, 50, 50);

        }

        // create news message object for player to interact with
        public void CreateNews()
        {
            activeNewsItems.Clear();

            while( activeNewsItems.Count < 3 )
            {       
                // Get Random country that has no news on him
                Country c;
                do{         
                    c = countries[ Random.Range(0, countries.Length) ];
                }while( IsCountryAvailable( c ) == false );

                // position of news message
                Vector3 position = c.transform.GetChild(0).position - Vector3.forward;

                // spawn a news message object
                News news = Instantiate( newsPrefab, position, Quaternion.identity).GetComponent<News>();
                news.country = c; // set country of message                


                // get news message from country
                NewsData newsMessage = c.GetNews();                
                news.catagorie = newsMessage.catagorie;
                news.value = newsMessage.value;

                SpriteRenderer fill = news.transform.GetChild(1).GetComponent<SpriteRenderer>();
                fill.color= CatagorieSettings.GetColor(news.catagorie);
               

                // add news message to list of active news messages
                activeNewsItems.Enqueue( news );
            }
        }

        // Send all news to neighbours
        public void SendNews()
        {
            bool temp1 = true;
            foreach (News news in activeNewsItems)
            {
                //if(temp1 == true)
                //{
                //    Debug.Log(newsPanel.currentNews.catagorie);
                //   result = newsPanel.currentNews.Send(false);
                //    temp1 = false;
                //}               
                result = news.Send(false);
                if( news.playerChanged == true)
                {
                    if (result){ 
                        Instantiate(SuccessAnimation,canvas.transform);
                    }else{
                        Instantiate(FailedAnimation,canvas.transform);
                    }
                }
            }
        }

        // check if a news message with given country already exists
        private bool IsCountryAvailable(Country country)
        {
            foreach( News news in activeNewsItems)
            {
                if( news.country == country )
                    return false;
            }
            return true;
        }
    }
}