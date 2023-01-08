using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollectWaterDropAudio : MonoBehaviour
{
    //The audiosource for this is on the level manaager
    [SerializeField] private AudioSource audioSource;
    public AudioClip[] collectCoinSfx;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayCollectCoinSfx()
    {
        audioSource.volume = Random.Range(0.8f, 1.0f);
        audioSource.PlayOneShot(collectCoinSfx[Random.Range(0, collectCoinSfx.Length)]);
    }
}
