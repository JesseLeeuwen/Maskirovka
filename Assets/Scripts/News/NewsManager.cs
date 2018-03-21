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
        private Queue<NewsFeedItem> activeNewsItems;
        public GameObject SuccessAnimation;
        public GameObject FailedAnimation;
        public Canvas canvas;
        public Vector3 bias;
        private bool result;
        public NewsPanel newsPanel;

        [SerializeField]
        private TwitterFeed feed;


        void Start()
        {            
            activeNewsItems = new Queue<NewsFeedItem>();
            CreateNews();
            bias = new Vector3(50, 50, 50);
        }

        // create news message object for player to interact with
        public void CreateNews()
        {
            activeNewsItems.Clear();
            
            int length = 3;
            if( countries.Length < 3 )
                length = countries.Length;


            while( activeNewsItems.Count < length )
            {       
                // Get Random country that has no news on him
                Country c;
                do{         
                    c = countries[ Random.Range(0, countries.Length) ];
                }while( IsCountryAvailable( c ) == false );

                // spawn a news message object
                //NewsFeedItem news = Instantiate(new NewsFeedItem());
                //news.subject = c; // set country of message                


                // get news message from country
                NewsData newsMessage = c.GetNews();                
                //news.catagorie = newsMessage.catagorie;
                //news.value = newsMessage.value;

                // add news message to list of active news messages
                activeNewsItems.Enqueue( feed.NewTweet(newsMessage, c) );
            }
        }

        // Send all news to neighbours
        public void SendNews()
        {
            bool temp1 = true;
            foreach (NewsFeedItem news in activeNewsItems)
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
                   StateAnimation(result);
                }
            }
        }

        // check if a news message with given country already exists
        private bool IsCountryAvailable(Country country)
        {
            foreach( NewsFeedItem news in activeNewsItems)
            {
                if( news.subject == country )
                    return false;
            }
            return true;
        }

        public GameObject StateAnimation(bool success){
            if (success){ 
                return Instantiate(SuccessAnimation,canvas.transform);
            }else{
                return Instantiate(FailedAnimation,canvas.transform);
            }
        }
    }
}