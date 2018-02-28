using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Maskirovka.Selector;


namespace Maskirovka
{
	public class Country : MonoBehaviour, ISelectable {

        public Neighbour[] neighbours;
        public Vector3 wantedReputation;
        public GameObject connectionPrefab;

        public float returnValue;

        [Header("Start reputation values")]
        public float randomMin;
        public float randomMax;

        [Header("Avarage reputation in all neighbours")]
        public float avarageA;
        public float avarageB;
        public float avarageC;

        [Header("Min and Max to spawn new Connection")]
        public float minValue;
        public float maxValue;

        //The value to give to the NewsManager
        [SerializeField]
        private int valToGive;
        private Catagorie catToGive;
        [SerializeField]        
        private Sprite sprite;

        private List<Connection> connections;
        private Country tempLand;

        void Awake()
        {      
            connections = new List<Connection>();     
            //Generate random wanted reputation withing range of given floats
            float A = Random.Range(randomMin, randomMax);
            float B = Random.Range(randomMin, randomMax);
            float C = Random.Range(randomMin, randomMax);
            wantedReputation = new Vector3(A,B,C);

            avarageA = wantedReputation.x;
            avarageB = wantedReputation.y;
            avarageC = wantedReputation.z;
        }

        void Start()
        {
            //Fill neigbours with the random generated wanted reputation
            for (int i = 0; i < neighbours.Length; i++)
            {
                neighbours[i].neighbour.UpdateRepu(this, wantedReputation.x, Catagorie.A, false);
                neighbours[i].neighbour.UpdateRepu(this, wantedReputation.y, Catagorie.B, false);
                neighbours[i].neighbour.UpdateRepu(this, wantedReputation.z, Catagorie.C, false);
            } 
        }

        public NewsData GetNews()
        {
            SearchNeighbours(); //Find the avarage values of A B and C

            float A = wantedReputation.x =- avarageA;
            float B = wantedReputation.y =- avarageB;
            float C = wantedReputation.z =- avarageC;

            Catagorie[] cats = new Catagorie[] {Catagorie.A,Catagorie.B,Catagorie.C};
            float[] vals = new float[] {A,B,C};
            int r = Random.Range(0,3);
            catToGive= cats[r];
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
                    if(catagorie == Catagorie.A)
                    {
                        neighbours[i].reputation.x = newReputation;
                        if(neighbours[i].reputation.x > this.wantedReputation.x + minValue && neighbours[i].reputation.x < this.wantedReputation.x + maxValue)
                        {
                            tempLand = neighbours[i].neighbour;
                            if( spawn == true) spawnConnection(Catagorie.A);
                        }
                    }
                    else if (catagorie == Catagorie.B)
                    {
                        neighbours[i].reputation.y = newReputation;
                        if (neighbours[i].reputation.y > this.wantedReputation.y + minValue && neighbours[i].reputation.y < this.wantedReputation.y + maxValue)
                        {
                            tempLand = neighbours[i].neighbour;
                            if( spawn == true) spawnConnection(Catagorie.B);
                        }
                    }
                    else if (catagorie == Catagorie.C)
                    {
                        neighbours[i].reputation.z = newReputation;
                        if (neighbours[i].reputation.z > this.wantedReputation.z + minValue && neighbours[i].reputation.z < this.wantedReputation.z + maxValue)
                        {
                            tempLand = neighbours[i].neighbour;
                            if( spawn == true) spawnConnection(Catagorie.C);
                        }
                    }
                }
            }
        }

        public void SearchNeighbours()
        {
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
            avarageA = A.Average();
            avarageB = B.Average();
            avarageC = C.Average();
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

        private void spawnConnection(Catagorie cat)
        {
            foreach( Connection c in connections )
                if(c.CountryPresent( this, tempLand) )
                    return;

            //spanw new Connection
            var connection = (GameObject)Instantiate(connectionPrefab);
            connection.GetComponent<Connection>().Init(this, tempLand, cat);
        }

        public void NewConnection(Connection connection)
        {
            connections.Add(connection);
        }

        public void RemoveConnection(Connection connection)
        {
            connections.Remove(connection);
        }

        public Sprite GetSprite()
        {
            return sprite;
        }

        new public SelectableType GetType() {
			return SelectableType.Country;
		}
	}


}