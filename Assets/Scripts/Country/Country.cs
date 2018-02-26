using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Maskirovka.Selector;

namespace Maskirovka
{
	public class Country : MonoBehaviour, ISelectable {

        public Neighbour[] neighbours;
        public Vector3 wantedReputation;
        private Vector3 currentReputation;

        [Header("Start reputation values")]
        public float randomMin;
        public float randomMax;

        //The value to give to the NewsManager
        private int valToGive;
        private Catagorie catToGive;

        void awake()
        {
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
                neighbours[i].reputation = wantedReputation;
            }
        }


        NewsData GetNews()
        {
            float A = wantedReputation.x - currentReputation.x;
            float B = wantedReputation.y - currentReputation.y;
            float C = wantedReputation.z - currentReputation.z;
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
            //Find your self in the list of the neighbour.neighbours and change the reputation.
            for (int i = 0; i < neighbours.Length; i++)
            {
                if(neighbours[i].neighbour == country)
                {
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
                        neighbours[i].reputation.y = newReputation;
                    }
                    //Update your own current reputation 
                    currentReputation = neighbours[i].reputation;
                }
            }
        }

		public SelectableType GetType() {
			return SelectableType.Country;
		}
	}


}