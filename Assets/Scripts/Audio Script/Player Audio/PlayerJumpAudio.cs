using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerJumpAudio : MonoBehaviour
{
   [SerializeField] private AudioSource audioSource;
   [SerializeField] private AudioClip jumpSfx;


   private void Awake()
   {
      if(audioSource == null)
         audioSource = GetComponent<AudioSource>();
   }

   public void PlayJumpSfx()
   {
      audioSource.volume = Random.Range(0.8f, 1.0f);
      audioSource.pitch = Random.Range(0.9f, 1.1f);
      audioSource.PlayOneShot(jumpSfx);
   }

}
