using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maskirovka.UI
{
	public class NewsFeedUI : MonoBehaviour 
	{
		[Header("References")]
		[SerializeField]
		private NewsFeed feed;					// the newsFeed scriptableObject
		[SerializeField]
		private GameObject listItemPrefab;		// the prefab of the listItem found in "Prefabs/UI"

		[Header("list settings")]
		[SerializeField, Range(0, 10)]
		private int maxListItems;

		[Header("image options")]
		[SerializeField]
		private Sprite[] connections; 			// the images diplayed when a new connectino is made or is broken
		
		private Queue<Change> newChanges;		// contains the newest changes from the previous turn
		private Change tempChange;				// used for spawning newsfeedlist items
		private GameObject tempObject;			// the gameObject of the spawned listItem 
		private NewsFeedItem tempItem;			// the NewsFeedItem ref of the newest spawned listItem
		
		void Update () 
		{
			newChanges = feed.PullUpdates();
			if( newChanges.Count > 0)
			{
				while(newChanges.Count > 0 )
				{	
					tempChange = newChanges.Dequeue();					
					tempObject = Instantiate( listItemPrefab, Vector3.zero, Quaternion.identity );
					
					tempObject.transform.SetParent(transform, false);
					tempObject.transform.SetAsFirstSibling();
					
					tempItem = tempObject.GetComponent<NewsFeedItem>();	
					
					int index = System.Convert.ToInt16( tempChange.madeNewConnection );			
					tempItem.Init(tempChange, connections[ index ]);
				}
			}
			for(int i = transform.childCount - 1; i >= maxListItems; --i )
			{
				Destroy( transform.GetChild( i ).gameObject );
			}						
		}
	}
}