using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioFadeInAndOut : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    
            [SerializeField][Range(1, 9)] private float fadeTime = 5;
        
            [SerializeField][Range(1, 9)] private float minTimeOff = 5;
            [SerializeField][Range(11, 19)] private float maxTimeOff = 15;
            private float timeOff;
            
            [SerializeField][Range(1, 9)] private float minTimeOn = 5;
            [SerializeField][Range(11, 19)] private float maxTimeOn = 15;
            private float timeOn;
    
            private readonly float minVol = 0.125f;
            private readonly float maxVol = 0.625f;
            private float audioVolume; 
            
            [Header("Debug Display")]
            [SerializeField] private float timer;
            [SerializeField] private float timeTillNextEvent;
    
            private bool audioIsPlaying =false ;
    
            private void Start()
            {
                RandomizeValues();
                timeTillNextEvent = timeOff;
                audioSource.volume = 0f;
                audioSource.Play();
            }
    
            private void Update()
            {
                timer += Time.deltaTime;
                if ((timer > timeTillNextEvent))
                {
    
    
    
                    if (audioIsPlaying)
                    {
                        StartCoroutine(FadeAudio(audioSource.volume, 0, fadeTime));
                        audioIsPlaying = false;
                        RandomizeValues();
                        timeTillNextEvent += timeOff + fadeTime;
                    }
                    else
                    {
    
                        StartCoroutine(FadeAudio(0, audioVolume, fadeTime));
                        audioIsPlaying = true;
                        audioSource.panStereo = Random.Range(-1f, 1f);
                        RandomizeValues();
                        timeTillNextEvent += timeOn + fadeTime;
                    }
                }
            }
    
            private void RandomizeValues()
            {
                timeOff = Random.Range(minTimeOff, maxTimeOff);
                timeOn = Random.Range(minTimeOn, maxTimeOn);
                audioVolume = Random.Range(minVol, maxVol);
              
            }
    
            private IEnumerator FadeAudio(float startValue, float endValue, float duration)
            {
                float currentTime = 0;
                
                while (currentTime <= duration)
                {
                    audioSource.volume = Mathf.Lerp(startValue, endValue, (currentTime / duration));
                    currentTime += Time.deltaTime;
                    yield return null;
                }
            }
    
        }


