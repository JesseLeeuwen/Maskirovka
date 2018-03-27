﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Maskirovka.Selector;
using Maskirovka.Utility;

namespace Maskirovka
{
	public class Country : MonoBehaviour, ISelectable {

        public string characterName;
        public Neighbour[] neighbours;
        public Vector3 wantedReputation;
        public GameObject connectionPrefab;
        public SpriteRenderer inline;

        public float returnValue;

        [Header("Start reputation values")]
        public float randomMin;
        public float randomMax;

        [Header("Avarage reputation in all neighbours")]
        public Vector3 avarage;

        [Header("Min and Max to spawn new Connection")]
        public float minValue;
        public float maxValue;

        //The value to give to the NewsManager
        [SerializeField]
        private int valToGive;
        private Catagorie catToGive;
        [SerializeField]        
        private Sprite sprite;
        
        [SerializeField]
        private Animator person;
        
        [SerializeField]
        private Cluster cluster;
        [SerializeField]
        private List<Connection> connections;
        private Country tempLand;
        private bool invaded;
        new private SpriteRenderer renderer;
        private OnClickAnim clickAnim;
        private Color countryColor;

        void Awake()
        {      
            clickAnim = GetComponent<OnClickAnim>();
            renderer = GetComponent<SpriteRenderer>();
            connections = new List<Connection>();     
            //Generate random wanted reputation withing range of given floats
            float A = Random.Range(randomMin, randomMax);
            float B = Random.Range(randomMin, randomMax);
            float C = Random.Range(randomMin, randomMax);
            wantedReputation = new Vector3(A,B,C);
            cluster.Init();
            
            countryColor = renderer.color;
            avarage = new Vector3(wantedReputation.x, wantedReputation.y, wantedReputation.z);
        }

        void Start()
        {
            person = GetComponentInChildren<Animator>();
            //Fill neigbours with the random generated wanted reputation
            for (int i = 0; i < neighbours.Length; i++)
            {
                neighbours[i].neighbour.UpdateRepu(this, wantedReputation.x, Catagorie.A, false);
                neighbours[i].neighbour.UpdateRepu(this, wantedReputation.y, Catagorie.B, false);
                neighbours[i].neighbour.UpdateRepu(this, wantedReputation.z, Catagorie.C, false);
            } 
        }

        void Update()
        {   
            for( int i = connections.Count -1; i >= 0; --i)
                if( connections[i].Equals(null) )
                    connections.RemoveAt(i);

            if( connections.Count == 0 ) cluster.Init();

            person.SetInteger("IntMood", cluster.Count);
        }

        public Cluster GetCluster()
        {
           return cluster;
        }

        public NewsData GetNews()
        {
            SearchNeighbours(); //Find the avarage values of A B and C

            float A = wantedReputation.x =- avarage.x;
            float B = wantedReputation.y =- avarage.y;
            float C = wantedReputation.z =- avarage.z;

            Catagorie[] cats = new Catagorie[] {Catagorie.A,Catagorie.B,Catagorie.C};
            float[] vals = new float[] {A,B,C};
            int r = Random.Range(0,3);
            catToGive = cats[r];
            valToGive = Mathf.Abs(Mathf.RoundToInt(vals[r]));
            return new NewsData { value = valToGive, catagorie = catToGive};
        }

        public void UpdateRepu( Country country, float newReputation, Catagorie catagorie, bool spawn = true)
        {  
            //Find the neighbour that wants a new reputation and update his reputation in your own list
            for (int i = 0; i < neighbours.Length; i++)
            {
                if(neighbours[i].neighbour == country)
                {
                    tempLand = neighbours[i].neighbour;
                    if(catagorie == Catagorie.A)
                    {
                        neighbours[i].reputation.x = newReputation;
                    }
                    else if (catagorie == Catagorie.B)
                    {
                        neighbours[i].reputation.y = newReputation;
                    }
                    else if (catagorie == Catagorie.C)
                    {
                        neighbours[i].reputation.z = newReputation;
                    }
                    Vector3 difference = neighbours[i].reputation - avarage;
                    float sum = Mathf.Abs(difference.x) + Mathf.Abs(difference.y) + Mathf.Abs(difference.z);

                    if(sum < maxValue && spawn)
                    {
                        spawnConnection();
                    }
                }
            }
        }

        public void SearchNeighbours()
        {
            if( neighbours.Length <= 0 ) return;

            List<float> A = new List<float>();
            List<float> B = new List<float>();
            List<float> C = new List<float>();

            for (int i = 0; i < neighbours.Length; i++)
            {
                for(int x = 0; x < neighbours[i].neighbour.neighbours.Length; x++)
                {                 
                    if (neighbours[i].neighbour.neighbours[x].neighbour == this)
                    {
                        A.Add(neighbours[i].neighbour.neighbours[x].reputation.x);
                        B.Add(neighbours[i].neighbour.neighbours[x].reputation.y);
                        C.Add(neighbours[i].neighbour.neighbours[x].reputation.z);
                    }
                }        
            }
            avarage.x = A.Average();
            avarage.y = B.Average();
            avarage.z = C.Average();
        }

        public void ReturnToNormal()
        {
            for(int i = 0; i < neighbours.Length; i++)
            {  
                //////////X
                if(neighbours[i].reputation.x > neighbours[i].neighbour.wantedReputation.x)
                {
                    neighbours[i].reputation.x -= returnValue;
                } else { neighbours[i].reputation.x += returnValue; }

                //////////Y
                if(neighbours[i].reputation.y > neighbours[i].neighbour.wantedReputation.y)
                {
                    neighbours[i].reputation.y -= returnValue;
                } else { neighbours[i].reputation.y += returnValue; }

                //////////Z
                if (neighbours[i].reputation.z > neighbours[i].neighbour.wantedReputation.z)
                {
                    neighbours[i].reputation.z -= returnValue;
                } else { neighbours[i].reputation.z += returnValue; }
            }
        }

        private void spawnConnection()
        {
            foreach( Connection c in connections )
                if(c.CountryPresent( this, tempLand) )
                    return;

            //spanw new Connection
            var connection = (GameObject)Instantiate(connectionPrefab);
            connection.GetComponent<Connection>().Init(this, tempLand);
        }

        public void NewConnection(Connection connection)
        {
            connections.Add(connection);
            person.SetTrigger( "TriggerWelcome" );
        }

        public void RemoveConnection(Connection connection)
        {
            connections.Remove(connection);

            cluster = new Cluster();
            cluster.Init();
        }

        public void Highlight(Color col, bool active)
        {
            inline.enabled = active;
            inline.color = col;
        }

        public void InvadeMode(Color lineColor, bool active)
        {
            if( invaded == true && active == true) return;

            Color mapColor = new Color( countryColor.grayscale, countryColor.grayscale, countryColor.grayscale, 1);
            renderer.color = active? mapColor : countryColor;
            clickAnim.SetColor( active? mapColor : countryColor );
            Highlight( lineColor, active && lineColor.grayscale > 0 );
        }

        public void Invaded(Russia russia)
        {            
            russia.AddNeighbours( neighbours );
            foreach( Neighbour n in neighbours )
            {
                Country c = n.neighbour;
                List<Neighbour> neighbourList = c.neighbours.ToList();
                neighbourList.RemoveAll( x => x.neighbour == this );
                c.neighbours = neighbourList.ToArray();
            }
            
            for(int i = connections.Count -1; i >= 0; --i)
            {                
                if( connections[i].Equals(null) == false )
                { 
                    connections[i].Delete();
                }
            }

            neighbours = new Neighbour[0];
            person.gameObject.SetActive(false);
            invaded =  true;
            renderer.color = russia.GetColor();
            GetComponent<OnClickAnim>().SetColor( russia.GetColor() );
        }

        public void SetCluster( Cluster cluster )
        {
            this.cluster = cluster;
        }

        public List<Connection> GetConnections()
        {
            return connections;
        }

        public void Select(object arg)
		{
            foreach( Connection c in connections)
                c.Activate( System.Convert.ToBoolean( (int)arg ));
        }

        public Sprite GetSprite()
        {
            return sprite;
        }

        public int GetClusterSize()
        {
            return cluster.Count;
        }

        new public SelectableType GetType() {
			return SelectableType.Country;
		}
	}
}