using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTriggerSystem : MonoBehaviour
{

    [SerializeField] private MusicLayerSystem musicLayerSystem;

    private void Awake()
    {
        Debug.Log("yo");
      
        
    }

    private void Start()
    {
        musicLayerSystem.StartMusicSystem();
    }

    
    public void OnPlayerEnter()
    {
      
       musicLayerSystem.IncreaseMusicLayer();
        
    }
}


