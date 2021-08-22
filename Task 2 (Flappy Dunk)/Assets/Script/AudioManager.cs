using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//control audio
public class AudioManager : MonoBehaviour
{
	public Sound[] soundClip;

	void Awake()
	{
		foreach (Sound s in soundClip)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;

			s.source.volume = s.volume;
			s.source.pitch = s.pitch;

			s.source.spatialBlend = s.spatial_blend;

			
			s.source.loop = s.loop;

			s.source.playOnAwake = s.PlayOnAwake;
			if (s.PlayOnAwake) s.source.Play();

		}
	}

	public void Play(string name)
	{
		Sound s = Array.Find(soundClip, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogWarning("Sound " + name + " Not Found");
			return;
		}

		s.source.Play();
	}

	public void Stop(string name)
	{
		Sound s = Array.Find(soundClip, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogWarning("Sound " + name + " Not Found");
			return;
		}

		s.source.Stop();
	}
}
