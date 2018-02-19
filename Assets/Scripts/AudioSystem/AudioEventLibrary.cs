using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FMODCLONE
{
	[CreateAssetMenu(menuName = "AudioEventLibrary")]
	public class AudioEventLibrary : ScriptableObject 
	{
		public List<AudioEvent> eventList;

		public AudioEvent GetEvent(string identifier)
		{
			return eventList.Find(x => x.identifier == identifier);
		}
	}
}