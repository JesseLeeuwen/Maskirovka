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

        [SerializeField]
        private MaterialPropertyBlock block;

        [Header("Reputation settings")]
        public float minRep;
        public float maxRep;

        private Vector3 total;
        private Vector4 targetValues;
        private Vector4 values;

        private float previousSum; 
        private LineRenderer background;

        public GameObject AppearParticles;
        public GameObject DisappearParticles;

        private void Awake()
        {
            block = new MaterialPropertyBlock();
            block.SetColor("_ColorA", CatagorieSettings.GetColor( Catagorie.A ));
            block.SetColor("_ColorB", CatagorieSettings.GetColor( Catagorie.B ));
            block.SetColor("_ColorC", CatagorieSettings.GetColor( Catagorie.C ));
        }

        void Update()
        {
            Vector3 temp1 = Search(country, neighbour);
            Vector3 temp2 = Search(neighbour, country);

            total = new Vector3(temp1.x - temp2.x, temp1.y - temp2.y, temp1.z - temp2.z);

            colorUpdate();

            Vector3 core1 = country.transform.GetChild(0).position;
            Vector3 core2 = neighbour.transform.GetChild(0).position;

            if (Mathf.Abs(total.x) + Mathf.Abs(total.y) + Mathf.Abs(total.z) > maxRep)
            {                
                GameManager.Instance.feed.PushUpdate( new Change() { 
                    madeNewConnection = false, 
                    countryA = country,
                    countryB = neighbour 
                });
                Instantiate(DisappearParticles,(core1+core2)/2,transform.rotation);
                Destroy(gameObject);
            }
            connection.SetPosition(0, core1);
            connection.SetPosition(1, core2);
            background.SetPosition(0, core1);
            background.SetPosition(1, core2);
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
            values = Vector4.MoveTowards( values, targetValues, Time.deltaTime / 8);
            block.SetColor("_Values", values);
            connection.SetPropertyBlock(block);
            
            float _Sum = Mathf.Abs(total.x) + Mathf.Abs(total.y) + Mathf.Abs(total.z);
            if( _Sum == previousSum )
                return;
            
            float a = 1 - (Mathf.Abs(total.x) / _Sum);
            float b = 1 - (Mathf.Abs(total.y) / _Sum);
            float c = 1 - (Mathf.Abs(total.z) / _Sum);
            
            float sum = a + b + c;
            a /= sum;
            b /= sum;

            float distance = Vector3.Distance( connection.GetPosition(0), connection.GetPosition(1));
            targetValues = new Vector4(a, b);
            
            block.SetFloat("_Length", distance );
            connection.SetPropertyBlock(block);
            previousSum = _Sum;
        }

        public void Init(Country neighbourGet, Country countryGet)
        {    
            // set country and neighbour        
            country = countryGet;
            neighbour = neighbourGet;

            // register new connection
            country.NewConnection( this );   
            neighbour.NewConnection( this ); 

            GameManager.Instance.feed.PushUpdate(new Change()
            {
                madeNewConnection = true,
                countryA = countryGet,
                countryB = neighbourGet
            }); 

            connection = GetComponent<LineRenderer>();
            background = GetComponentsInChildren<LineRenderer>()[1];            

            Vector3 core1 = country.transform.GetChild(0).position;
            Vector3 core2 = neighbour.transform.GetChild(0).position;
            Vector3 countryAnchor = new Vector3(core1.x, core1.y, core1.z + 0.1f);
            Vector3 neighbourAnchor = new Vector3(core2.x, core2.y, core2.z + 0.1f);
            
            connection.SetPosition(0, core1);
            connection.SetPosition(1, core2);
            background.SetPosition(0, core1);
            background.SetPosition(1, core2);
            
            Instantiate(AppearParticles,(core1+core2)/2,transform.rotation);

            // update connection graphics
            Vector3 temp1 = Search(country, neighbour);
            Vector3 temp2 = Search(neighbour, country);

            total = new Vector3(temp1.x - temp2.x, temp1.y - temp2.y, temp1.z - temp2.z);
            colorUpdate();
            values = targetValues;

        }

        public bool CountryPresent(Country country, Country neighbour)
        {
            return ( country == this.country || country == this.neighbour ) && (neighbour == this.country || neighbour == this.neighbour);
        }

    }
}
    
