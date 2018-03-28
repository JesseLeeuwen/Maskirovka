using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FMODCLONE
{
	public class AudioEventEmitter : MonoBehaviour 
	{
		[SerializeField]
		private AudioEvent audioEvent;

		[SerializeField]
		private string identifier;

		void Start () 
		{
			if( identifier == string.Empty )
				AudioManager.PlayClip(ref audioEvent);
			else			
				AudioManager.PlayClip( identifier, ref audioEvent );
		}		
	}
}