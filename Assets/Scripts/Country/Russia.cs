using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maskirovka
{

	public class Russia : MonoBehaviour 
	{
		[SerializeField]
		private List<Neighbour> neighbours;
		private Color color;
				
		void Start () 
		{
			color = GetComponent<SpriteRenderer>().color;
		}

		public bool InvadeCountry( Country country )
		{
			bool isNeighbour = neighbours.FindAll( x => x.neighbour ==  country ).Count > 0;
			
			if( isNeighbour == true )
			{
				country.Invaded(this);
			}
			return isNeighbour;
		}

		public Color GetColor()
		{
			return color;
		}

		public void AddNeighbours( Neighbour[] newNeighbours )
		{
			foreach( Neighbour n in newNeighbours)
			{
				if( neighbours.FindIndex( x => x.neighbour == n.neighbour ) == -1 )
				{
					neighbours.Add( n );
				}
			}
		}
	}
}