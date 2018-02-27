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

            if(Mathf.Abs( temp1 - temp2) > maxRep)
            {
                GameManager.Instance.feed.PushUpdate( new Change() { 
                    madeNewConnection = false, 
                    countryA = country,
                    countryB = neighbour 
                });
                country.RemoveConnection( this );
                neighbour.RemoveConnection( this );
                Destroy(gameObject);
            }
        }

        float Search(Country country, Country neighbour)
        {
            for (int i = 0; i < country.neighbours.Length; i++)
            {
                if (country.neighbours[i].neighbour == neighbour)
                {
                    if(catagorie == Catagorie.A)
                    {
                        return country.neighbours[i].reputation.x;
                    }
                    else if (catagorie == Catagorie.B)
                    {
                        return country.neighbours[i].reputation.y;
                    }
                    else if (catagorie == Catagorie.C)
                    {
                        return country.neighbours[i].reputation.z;
                    }
                }
            }
            return 0;
        }

        public void Init(Country neighbourGet, Country countryGet, Catagorie cat)
        {            
            GameManager.Instance.feed.PushUpdate(new Change()
            {
                madeNewConnection = true,
                countryA = countryGet,
                countryB = neighbourGet
            }); 

            connection = GetComponent<LineRenderer>();
            connection.positionCount = 2;

            Vector3 core1 = countryGet.transform.GetChild(0).position;
            Vector3 core2 = neighbourGet.transform.GetChild(0).position;
            connection.SetPosition(0, new Vector3(core1.x, core1.y, core1.z + 0.1f));
            connection.SetPosition(1, new Vector3(core2.x, core2.y, core2.z + 0.1f));

            country = countryGet;
            neighbour = neighbourGet;
            catagorie = cat;
            
            country.NewConnection( this );
            neighbour.NewConnection( this );
        }

        public bool CountryPresent(Country country, Country neighbour)
        {
            return ( country == this.country || country == this.neighbour ) && (neighbour == this.country || neighbour == this.neighbour);
        }
    }
}
