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
        private int valToGive;
        private Catagorie catToGive;
        private Sprite sprite;

        void Awake()
        {
            //sprite = GetComponent<SpriteRenderer>().sprite;

            //Generate random wanted reputation withing range of given floats
            float A = Random.Range(randomMin, randomMax);
            float B = Random.Range(randomMin, randomMax);
            float C = Random.Range(randomMin, randomMax);
            wantedReputation = new Vector3(A,B,C);
        }

        void Start()
        {
            //Fill neigbours with the random generated wanted reputation
            for (int i = 0; i < neighbours.Length; i++)
            {
                neighbours[i].neighbour.UpdateRepu(this, wantedReputation.x, Catagorie.A);
                neighbours[i].neighbour.UpdateRepu(this, wantedReputation.y, Catagorie.B);
                neighbours[i].neighbour.UpdateRepu(this, wantedReputation.z, Catagorie.C);
            }
        }


        public NewsData GetNews()
        {
            SearchNeighbours(); //Find the avarage values of A B and C

            float A = wantedReputation.x - avarageA;
            float B = wantedReputation.y - avarageB;
            float C = wantedReputation.z - avarageC;

            float max = Mathf.Max(A, B, C);
            float min = Mathf.Min(A, B, C);

            float sum = max - min;

            if(sum < 0)
            {
                if (min == A)
                {
                    catToGive = Catagorie.A;
                }
                if (min == B)
                {
                    catToGive = Catagorie.B;
                }
                if (min == C)
                {
                    catToGive = Catagorie.C;
                }
                //if the difference in '-' you need to add it 
                valToGive = Mathf.RoundToInt(min) *+1; 
            }
            else if(sum >= 0)
            {
                if (max == A)
                {
                    catToGive = Catagorie.A;
                }
                if (max == B)
                {
                    catToGive = Catagorie.B;
                }
                if (max == C)
                {
                    catToGive = Catagorie.C;
                }
                //if the difference in '+' you need to substract it 
                valToGive = Mathf.RoundToInt(max) *-1;
            }
            return new NewsData { value = valToGive, catagorie = catToGive};
        }


        public void UpdateRepu( Country country, float newReputation, Catagorie catagorie)
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
                            spawnConnection();
                        }
                    }
                    else if (catagorie == Catagorie.B)
                    {
                        neighbours[i].reputation.y = newReputation;
                        if (neighbours[i].reputation.y > this.wantedReputation.y + minValue && neighbours[i].reputation.y < this.wantedReputation.y + maxValue)
                        {
                            spawnConnection();
                        }
                    }
                    else if (catagorie == Catagorie.C)
                    {
                        neighbours[i].reputation.z = newReputation;
                        if (neighbours[i].reputation.z > this.wantedReputation.z + minValue && neighbours[i].reputation.z < this.wantedReputation.z + maxValue)
                        {
                            spawnConnection();
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

        private void spawnConnection()
        {
            //spanw new Connection
            var connection = (GameObject)Instantiate(
                connectionPrefab);
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