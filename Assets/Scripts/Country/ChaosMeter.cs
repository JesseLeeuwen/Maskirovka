using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maskirovka
{	
	[System.Serializable]
	public struct Cluster
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
				
				if( result == true)
					break;
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
		[SerializeField]
		private int countConnections;
		[SerializeField]
		private Achievement achievement;

		private int biggestCluster;
		private bool clusterExist;

		[Header("Invade Settings")]
		[SerializeField, Range(0, 10)]
		private int maxNeighbourClusterSize = 1;
		[SerializeField, Range(0, 10)]
		private int maxBigCluster = 3;
		[SerializeField, Range(0, 100)]
		private int chaosMinimum = 50;

		[Header("Cluster Debug")]
		[SerializeField]
		private List<Cluster> clusters;
		[SerializeField]
		private List<Connection> connections;
		private int maxConnections;

		void Start()
		{
			print(null);
			clusters = new List<Cluster>();

			for(int i = 0; i < countries.Length; ++i)
				maxConnections += countries[i].neighbours.Length;

			GameManager.Instance.feed.SubToNewsEvents( OnReceiveChange );
		}

		void OnReceiveChange( Change change )
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
                change.countryB.RemoveConnection( current );				
			}
			CheckForWorldPeace();
		}		

		public bool CanInvade(Country country)
		{
			bool canInvade = chaos < chaosMinimum && biggestCluster <= maxBigCluster;
			Cluster cluster = country.GetCluster();

			bool neighbourIsInCluster = false;
			foreach( Neighbour n in country.neighbours)
			{
				if( n.neighbour.GetClusterSize() > maxNeighbourClusterSize)
					neighbourIsInCluster = true;
			}

			canInvade = neighbourIsInCluster == false && canInvade && cluster.Count < 1;
			return canInvade;
		}

		private void UpdateChoas()
		{
			int elementCount = 0;			
			biggestCluster = 0;
			foreach( Cluster c in clusters)
			{
				elementCount += c.Count * 2;
				
				if( c.Count > biggestCluster )
					biggestCluster = c.Count;

				if( c.Count >= 3)
					elementCount += 5 * c.Count;
			}
			chaos = elementCount;
		}		

		private void Update()
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
			UpdateChoas();
		}

		public bool IsInCluster(Connection c)
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
					if( c.Equals(null) == false )
					{
						cluster.Add( c );
						c.SetCluster( cluster );
						SeekCluster( c.neighbour == country? c.country : c.neighbour, cluster);
					}
				}
			}
		}

		private void CheckForWorldPeace()
		{
			if( maxConnections == connections.Count)
				achievement.gameObject.SetActive(true);
		}
	}
}