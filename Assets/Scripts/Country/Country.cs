using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Maskirovka.Selector;

namespace Maskirovka
{
	public class Country : MonoBehaviour, ISelectable {

        public Neighbour[] neighbours;
        public Vector3 wantedReputation;


		void Update () {
		
		}

        public NewsData GetNews() {
            return new NewsData();
        }

        public void UpdateRepu( Neighbour neighbour, Vector3 newReputation ) {

        }



		public SelectableType GetType() {
			return SelectableType.Country;
		}
	}


}