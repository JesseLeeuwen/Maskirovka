using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maskirovka
{	
	[System.Serializable]
	struct Cluster
	{		
		// connection that are part of the connections
		public List<Connection> connections;

		// constructor for cluster
		public void Init()
		{
			connections = new List<Connection>();
		}

		// add a connection to this cluster
		public void Add( Connection c ) { connections.Add( c ); }
		// look wether a connection is part of this cluster
		public bool Contains( Connection c ) {return connections.Contains( c ); }		
		// look wehter a country is part of this cluster
		public bool ContainsCountry( Country country )
		{
			bool result = false;
			foreach( Connection c in connections )
			{
				result = (country == c.neighbour || country == c.country);
			}
			return result;
		}

		// count of connection in cluster
		public int Count{ get{ return connections.Count; } }
	}


	public class ChaosMeter : MonoBehaviour 
	{
		[SerializeField]
		private CountryList countries;
		
		[SerializeField, Range(0, 100)]
		private float chaos;
		private bool clusterExist;
		[SerializeField]
		private int countConnections;

		[SerializeField]
		private List<Cluster> clusters;
		[SerializeField]
		private List<Connection> connections;

		void Start()
		{
			clusters = new List<Cluster>();
			GameManager.Instance.feed.SubToNewsEvents( OnReceiveChange );
		}

		public void OnReceiveChange( Change change )
		{
			countConnections += change.madeNewConnection? 1 : -1;

			Connection current = null;
			foreach( Connection c in change.countryA.GetConnections())
			{
				if(c.neighbour == change.countryB )
				{
					current = c;
					break;
				}
			}	
			int index = connections.IndexOf(current );

			if(change.madeNewConnection == true)
				connections.Add(current);
			else if( index > 0 )
			{							
				connections.RemoveAt(index);
				change.countryA.RemoveConnection( current );
                change.countryA.RemoveConnection( current );				
			}

			UpdateClusters();
			UpdateChoas();
		}		

		private void UpdateChoas()
		{
			
		}		

		private void UpdateClusters()
		{
			clusters = new List<Cluster>();

			foreach( Connection c in connections )
			{
				if( IsInCluster( c ) == true )
					continue;
				
				Cluster current = new Cluster();
				current.Init();
				clusters.Add(current);
				SeekCluster( c.country, current);
			}
		}

		private bool IsInCluster(Connection c)
		{
			foreach( Cluster cluster in clusters)
			{
				if( cluster.Contains(c) )
					return true;
			}
			return false;
		}

		private void SeekCluster(Country country, Cluster cluster)
		{
			foreach( Connection c in country.GetConnections() )
			{
				if( cluster.Contains(c) == false )
				{
					if( c != null )
					{
						cluster.Add( c );
						SeekCluster( c.neighbour == country? c.country : c.neighbour, cluster);
					}
				}
			}
		}
	}
}