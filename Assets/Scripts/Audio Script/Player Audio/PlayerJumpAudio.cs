using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerJumpAudio : MonoBehaviour
{
   [SerializeField] private AudioSource audioSource;
   [SerializeField] private AudioClip jumpSfx;
   public float minVal = 0.45f;
   public float maxVal = 0.55f;

   private void Awake()
   {
      if(audioSource == null)
         audioSource = GetComponent<AudioSource>();
   }

   public void PlayJumpSfx()
   {
      audioSource.volume = Random.Range(0.8f, 1.0f);
      audioSource.pitch = Random.Range(minVal, maxVal);
      audioSource.PlayOneShot(jumpSfx);
   }

}
