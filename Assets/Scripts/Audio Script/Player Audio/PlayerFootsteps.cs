using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{

	private AudioSource audioSource;
	public List<AudioClip> iceFootsteps;
	public List<AudioClip> dirtFootsteps;
	[Header("Random Params")]
	[SerializeField] [Range(0.01f, 0.99f)] private float minVolume = 0.8f;
	[SerializeField] [Range(0.02f, 1f)] private float maxVolume = 1f;

	[SerializeField] [Range(0.25f, 0.75f)] private float stepCooldown = 0.5f;

	[SerializeField] private PlayerMovement pm;

	private float timer = 0;
	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		if (pm == null)
			pm = FindObjectOfType<PlayerMovement>();
	}

	private void Update()
	{
		timer += Time.deltaTime;

		if (timer < stepCooldown) return;
		if (!pm.IsMoving()) return;
		if (!pm.IsGrounded()) return;
		PlayFootstepAudio();
		timer = 0;
	}

	private void PlayFootstepAudio()
	{


		var clip = dirtFootsteps[Random.Range(0, iceFootsteps.Count)];


		audioSource.volume = Random.Range(minVolume, maxVolume);
		audioSource.pitch = Random.Range(0.9f, 1.1f);
		audioSource.PlayOneShot(clip);
	}
}
