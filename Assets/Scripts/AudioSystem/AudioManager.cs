using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace FMODCLONE
{
	public class AudioManager : MonoBehaviour 
	{
		[SerializeField]
		private AudioEventLibrary library;
		[SerializeField]
		private int bufferSize;
		[SerializeField]
		private List<AudioEvent> ongoingEvents;
		[SerializeField]
		private AudioMixer mixer;

		private Queue<AudioSource> sources;
		private static AudioManager instance;

		void Start () 
		{
			instance = this;
			AudioSource newSource;
			GameObject gObject;

			sources = new Queue<AudioSource>(bufferSize);
			ongoingEvents = new List<AudioEvent>(bufferSize);

			for(int i = 0; i < bufferSize; ++i)
			{
				gObject = new GameObject("source: " + i);
				newSource = gObject.AddComponent<AudioSource>();
				gObject.transform.parent = transform;
				sources.Enqueue( newSource );
			}
		}

		void Update()
		{
			for(int i = ongoingEvents.Count-1; i >= 0; --i)
			{	
				if( ongoingEvents[i].source.time >= ongoingEvents[i].clip.length - 0.05f && ongoingEvents[i].loop == false)
				{
					ongoingEvents.RemoveAt(i);
				}
			}
		}

		public static void PlayClip(string eventIdentifier, ref AudioEvent audioEvent)
		{
			PlayClip(eventIdentifier, ref audioEvent, Vector3.zero);
		}

		public static void PlayClip(AudioEvent audioEvent, Vector3 position)
		{
			AudioSource source = instance.sources.Dequeue();
			source.clip = audioEvent.clip;
			source.outputAudioMixerGroup = audioEvent.mixer;
			source.volume = audioEvent.volume;
			source.transform.position = position;
			source.loop = audioEvent.loop;
			source.Play();
			audioEvent.source = source;			

			instance.ongoingEvents.Add ( audioEvent );
			instance.sources.Enqueue(source);
		}

		public static void PlayClip(string eventIdentifier, ref AudioEvent audioEvent, Vector3 position)
		{
			audioEvent = instance.library.GetEvent( eventIdentifier ).Copy();
			PlayClip( audioEvent, position);
		}

		public static void PlayClip(string eventIdentifier)
		{
			PlayClip(eventIdentifier, Vector3.zero);
		}

		public static void PlayClip(string eventIdentifier, Vector3 position)
		{
			AudioEvent e = instance.library.GetEvent( eventIdentifier ).Copy();
			PlayClip( e, position);
		}

		public static void StopAllEvents()
		{
			for(int i = instance.ongoingEvents.Count -1; i >= 0; --i)
			{	
				instance.ongoingEvents[i].source.Stop();
				instance.ongoingEvents.RemoveAt(i);
			}
		}

		public static void StopAllEvents(string eventIdentifier)
		{
			List<AudioEvent> events = instance.ongoingEvents.FindAll(x=> x.identifier == eventIdentifier);
			print(events.Count);
			for(int i = events.Count-1; i >= 0; --i)
			{	
				events[i].Stop();
				instance.ongoingEvents.RemoveAt(i);
			}
		}

		public static void PauseAllEvents()
		{
			for(int i = instance.ongoingEvents.Count -1; i >= 0; --i)
			{	
				instance.ongoingEvents[i].Pause();				
			}
		}

		public static void PauseAllEvents(string eventIdentifier)
		{
			List<AudioEvent> events = instance.ongoingEvents.FindAll(x=> x.identifier == eventIdentifier);

			for(int i = events.Count -1; i > 0; --i)
			{	
				events[i].Pause();				
			}
		}

		public static void ResumeAllEvents()
		{
			for(int i = instance.ongoingEvents.Count -1; i >= 0; --i)
			{	
				instance.ongoingEvents[i].Resume();				
			}
		}

		public static void ResumeAllEvents(string eventIdentifier)
		{
			List<AudioEvent> events = instance.ongoingEvents.FindAll(x=> x.identifier == eventIdentifier);

			for(int i = events.Count -1; i >= 0; --i)
			{	
				events[i].Resume();				
			}
		}
		public static AudioMixerGroup[] GetMixergroups(string path)
		{
			return instance.mixer.FindMatchingGroups( path );
		}
	}
}