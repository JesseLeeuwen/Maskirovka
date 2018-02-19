using UnityEngine;
using UnityEngine.Audio;

namespace FMODCLONE
{
	[System.Serializable]
	public class AudioEvent 
	{
		public string identifier;

		public AudioClip clip;
		public AudioMixerGroup mixer;
		public float volume;
		public bool loop;
		public AudioSource source;

		public void Pause()
		{
			source.Pause();
		}

		public void Resume()
		{
			source.Pause();
		}

		public void Stop()
		{
			source.Stop();
		}

		public void Play(Vector3 position = default(Vector3))
		{
			AudioManager.PlayClip(this, position);
		}

		public AudioEvent Copy()
		{
			return (AudioEvent) this.MemberwiseClone();
		} 
	}
}