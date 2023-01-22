using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAudio : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;

	public List<AudioClip> enemyIdlesSfx;
	public List<AudioClip> enemyAttackSfx;
	// Start is called before the first frame update
	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.GetComponent<PlayerMovement>()) {
			PlayEnemyIdle();
		}
	}

	private void PlayEnemyIdle()
	{
		RandomizeAudio();
		audioSource.PlayOneShot(enemyIdlesSfx[Random.Range(0, enemyIdlesSfx.Count)]);
	}


	private void RandomizeAudio()
	{
		audioSource.volume = Random.Range(0.8f, 1.0f);
		audioSource.pitch = Random.Range(0.8f, 1.2f);
	}
}
