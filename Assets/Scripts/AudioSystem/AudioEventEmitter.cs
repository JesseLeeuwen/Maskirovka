using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FMODCLONE
{

	public class AudioEventEmitter : MonoBehaviour 
	{
		[SerializeField]
		private string eventToPlay;		
		[SerializeField]
		private AudioEvent audioEvent;

		void Start () 
		{
			AudioManager.PlayClip( eventToPlay, ref audioEvent);
		}		
	}
}