using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPainAudio : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClip hurtSfx;
	[SerializeField] private AudioClip deathSfx;

	private void Awake()
	{
		if (audioSource == null)
			audioSource = GetComponent<AudioSource>();
	}

	public void PlayDeathSfx()
	{
		RandomizeAudio();
		audioSource.PlayOneShot(deathSfx);
	}
	public void PlayHurtSfx()
	{
		Debug.Log("playing hurt");
		RandomizeAudio();
		audioSource.PlayOneShot(hurtSfx);
	}

	private void RandomizeAudio()
	{
		audioSource.volume = Random.Range(0.8f, 1.0f);
		audioSource.pitch = Random.Range(0.8f, 1.2f);
	}
}
