using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maskirovka
{
    public class Connection : MonoBehaviour 
    {     
        [Header("Country connections")]
        public Country neighbour;
        public Country country;
        public LineRenderer connection;
        public Catagorie catagorie;

        [Header("Reputation settings")]
        public float minRep;
        public float maxRep;


        void Update()
        {
            float temp1 = Search(country, neighbour);
            float temp2 = Search(neighbour, country);

            if(temp1 < minRep || temp1 > maxRep)
            {
                if (temp2 < minRep || temp2 > maxRep)
                {
                    GameManager.Instance.feed.PushUpdate( new Change() { 
                        madeNewConnection = false, 
                        countryA = country,
                        countryB = neighbour 
                    });
                    Destroy(gameObject);
                }
            }
        }


        float Search(Country country, Country neighbour)
        {
            for (int i = 0; i < neighbour.neighbours.Length; i++)
            {
                if (country.neighbours[i].neighbour == neighbour)
                {
                    if(catagorie == Catagorie.A)
                    {
                        return neighbour.neighbours[i].reputation.x;
                    }
                    else if (catagorie == Catagorie.B)
                    {
                        return neighbour.neighbours[i].reputation.y;
                    }
                    else if (catagorie == Catagorie.C)
                    {
                        return neighbour.neighbours[i].reputation.z;
                    }
                }
            }
            return 0;
        }

        public void Init(Country neighbourGet, Country countryGet)
        {
            /*
             GameManager.Instance.feed.PushUpdate(new Change()
            {
                madeNewConnection = true,
                countryA = country,
                countryB = neighbour
            }); 
            */

            connection = GetComponent<LineRenderer>();
            connection.positionCount = 2;
            connection.SetPosition(0, new Vector3(countryGet.transform.position.x, countryGet.transform.position.y, countryGet.transform.position.z -.1f));
            connection.SetPosition(1, new Vector3(neighbourGet.transform.position.x, neighbourGet.transform.position.y, neighbourGet.transform.position.z -.1f));

            country = countryGet;
            neighbour = neighbourGet;
        }
    }
}
