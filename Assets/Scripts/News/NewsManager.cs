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

        public void CreateNews()
        {
            activeNewsItems.Clear();

            while( activeNewsItems.Count < 3 )
            {       
                Country c;
                do{         
                    c = countries[ Random.Range(0, countries.Length) ];
                }while( IsCountryAvailable( c ) == false );

                Vector3 position = c.transform.position - Vector3.forward;

                News news = Instantiate( newsPrefab, position, Quaternion.identity).GetComponent<News>();
                news.country = c;
                news.catagorie = (Catagorie)Random.Range( 0, 3 );
                news.value = Random.value * 100;
                activeNewsItems.Enqueue( news );
            }
        }

        public void SendNews()
        {
            foreach( News news in activeNewsItems)
                news.Send();
        }

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