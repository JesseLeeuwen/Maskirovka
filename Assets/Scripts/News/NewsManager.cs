using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        public GameObject ConqueredAnimation;
        public GameObject FailedAnimation;
        public Canvas canvas;
        public Vector3 bias;
        private bool result;
        public NewsPanel newsPanel;
        public int turns = 0;
        public int turnsToInvade = 0;
        public Text TurnDisplay;

        [SerializeField]
        private TwitterFeed feed;

        [SerializeField]
        private GameObject loseScreen;


        void Start()
        {            
            activeNewsItems = new Queue<NewsFeedItem>();
            CreateNews();
            bias = new Vector3(50, 50, 50);
            setTurnDisplay();
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
            bool notInvading = false; 
            foreach (NewsFeedItem news in activeNewsItems)
            {                             
                result = news.Send(false);
                if( news.playerChanged == true)
                {
                    notInvading=true;
                   StateAnimation(result);
                }
            }
            if (notInvading) turns++;
            loseScreen.SetActive(turns>=turnsToInvade && turnsToInvade>0);
            setTurnDisplay();
            
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

        public GameObject ConqueredFeedback( bool success )
        {
            if (success){ 
                return Instantiate(ConqueredAnimation,canvas.transform);
            }else{
                return Instantiate(FailedAnimation,canvas.transform);
            }
        }
        void setTurnDisplay(){
            TurnDisplay.text = "TURNS: "+turns.ToString();
            if (turnsToInvade>0)
                 TurnDisplay.text+="/"+ turnsToInvade.ToString();
        }

    }
}