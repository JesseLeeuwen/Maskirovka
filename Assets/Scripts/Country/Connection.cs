using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Maskirovka.Utility;

namespace Maskirovka
{
    public class Connection : MonoBehaviour 
    {     
        [Header("Country connections")]
        public Country neighbour;
        public Country country;
        public LineRenderer connection;
        public Catagorie catagorie;

        [Header("Material information")]
        public Material material;
        [SerializeField]
        private MaterialPropertyBlock block;

        [Header("Reputation settings")]
        public float minRep;
        public float maxRep;

        private Vector3 total;
        private float previousSum; 

        private void Awake()
        {
            block = new MaterialPropertyBlock();
        }


        void Update()
        {
            Vector3 temp1 = Search(country, neighbour);
            Vector3 temp2 = Search(neighbour, country);

            total = new Vector3(temp1.x - temp2.x, temp1.y - temp2.y, temp1.z - temp2.z);

            colorUpdate();

            if (Mathf.Abs(total.x) + Mathf.Abs(total.y) + Mathf.Abs(total.z) > maxRep)
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

            Vector3 core1 = country.transform.GetChild(0).position;
            Vector3 core2 = neighbour.transform.GetChild(0).position;
            connection.SetPosition(0, new Vector3(core1.x, core1.y, core1.z + 0.1f));
            connection.SetPosition(1, new Vector3(core2.x, core2.y, core2.z + 0.1f));
        }

        Vector3 Search(Country country, Country neighbour)
        {
            for (int i = 0; i < country.neighbours.Length; i++)
            {
                if (country.neighbours[i].neighbour == neighbour)
                {
                    return country.neighbours[i].reputation;
                }
            }
            return new Vector3();
        }


        void colorUpdate()
        {
            float _Sum = Mathf.Abs(total.x) + Mathf.Abs(total.y) + Mathf.Abs(total.z);
            if( _Sum == previousSum )
                return;
            
            float a = 1 - (Mathf.Abs(total.x) / _Sum);
            float b = 1 - (Mathf.Abs(total.y) / _Sum);
            float c = 1 - (Mathf.Abs(total.z) / _Sum);
            
            float sum = a + b + c;
            a /= sum;
            b /= sum;

            block.SetColor("_Values", new Vector4(a, b, 0));
            connection.SetPropertyBlock(block);
            previousSum = _Sum;
        }

        public void Init(Country neighbourGet, Country countryGet)
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

            //connection.material.color = CatagorieSettings.GetColor(cat);


            country.NewConnection( this );
            neighbour.NewConnection( this );
        }

        public bool CountryPresent(Country country, Country neighbour)
        {
            return ( country == this.country || country == this.neighbour ) && (neighbour == this.country || neighbour == this.neighbour);
        }
    }
}
