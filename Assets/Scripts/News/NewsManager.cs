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
            IssueNews();
        }

        public void IssueNews()
        {
            activeNewsItems.Clear();

            while( activeNewsItems.Count < 1 )
            {       
                Country c;
                do{         
                    c = countries[ Random.Range(0, countries.Length) ];
                }while( IsCountryAvailable( c ) == false );

                News news = Instantiate( newsPrefab, c.transform.position, Quaternion.identity).GetComponent<News>();
                news.country = c;
                news.catagorie = (Catagorie)Random.Range( 0, 3 );
                news.value = Random.value * 100;
                activeNewsItems.Enqueue( news );
            }
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