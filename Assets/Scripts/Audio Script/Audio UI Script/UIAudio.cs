using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// The main brain for the UI audio sounds 
/// </summary>
public class UIAudio : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip playButtonSfxClip; 
    [SerializeField] private AudioClip hoverButtonSfxClip; 
    [SerializeField] private AudioClip feedbackSliderSfxClip;

    [SerializeField] private AudioMixerSnapshot defaultSnapshot;
    [SerializeField] private AudioMixerSnapshot lowPassSnapshot;
    [SerializeField] private float transitionTime = 1.5f;

    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer sfxMixer;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = feedbackSliderSfxClip;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
    }


    //will potentially be the resume button as well 
    public void PlayButtonSfx()
    {
        audioSource.PlayOneShot(playButtonSfxClip);
    }
    
    
    public void PlayHoverButtonSfx()
    {
        audioSource.PlayOneShot(hoverButtonSfxClip);
    }
    
    public void PlayFeedbackSliderSfx()
    {
        audioSource.Play();
    }
    
    public void StopFeedbackSliderSfx()
    {
        audioSource.Stop();
    }

    public void GotoDefaultSnapshot()
    {
        defaultSnapshot.TransitionTo(transitionTime);
    }
    
    public void GotoLowPassSnapshot()
    {
        lowPassSnapshot.TransitionTo(transitionTime);
    }


    public void SetSfxLvl(float sfxLvl)
    {
        sfxMixer.SetFloat("SfxVol", sfxLvl);
    }
    public void SetMusicLvl(float musicLvl)
    {
        musicMixer.SetFloat("MusicVol", musicLvl);
    }
}
