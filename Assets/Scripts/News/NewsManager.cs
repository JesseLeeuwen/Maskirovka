using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maskirovka.News
{
    public class NewsManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject newsPrefab;
        [SerializeField]
        private Country[] countries;
        private Queue<News> activeNewsItems;

        void Start()
        {            
            activeNewsItems = new Queue<News>();
            CreateNews();
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

                // add news message to list of active news messages
                activeNewsItems.Enqueue( news );
            }
        }

        // Send all news to neighbours
        public void SendNews()
        {
            foreach( News news in activeNewsItems)
                news.Send();
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