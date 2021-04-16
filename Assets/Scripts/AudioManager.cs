using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
	public Sound[] sounds;
	private bool isPlaying = false;
	void Awake()
	{
		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			s.source.playOnAwake = s.playOnAwake;
		}
	}
	void Start()
	{
		//Play("Theme1");

		if (SceneManager.GetActiveScene().name == "GamePlay_1")
		{
			FindObjectOfType<AudioManager>().Play("Theme1");
		}
		if (SceneManager.GetActiveScene().name == "GamePlay_2")
		{
			FindObjectOfType<AudioManager>().Play("Theme3");
		}

		if (SceneManager.GetActiveScene().name == "GamePlay_3")
		{
			FindObjectOfType<AudioManager>().Play("Theme5");
		}
	}

	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
		isPlaying = true;
		s.source.Play();
	}

	public void Stop(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
		isPlaying = false;
		s.source.Stop();
	}

	public void PlayOnce(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		if (s.source.isPlaying == true)
		{
			s.source.Stop();
			isPlaying = false;
		}
		if (s.source.isPlaying == false)
		{
			s.source.Play();
			//isPlaying = true;
		}

	}

	public void PlayOneShot(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		if (isPlaying == false)
        {
			s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
			s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
			s.source.PlayOneShot(s.source.clip, s.source.volume);
			isPlaying = true;
		}
		else
        {
			s.source.Stop();
			isPlaying = false;
		}
	}
}