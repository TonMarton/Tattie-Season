using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    [SerializeField] private MusicLayerSystem musicLayerSystem;

    [SerializeField] private PlayerStats ps;
    [SerializeField] private MusicStates musicStates;

     private float waterLevelCheckTime = 7.5f;
    private float timer; 
    private void Awake()
    {
        if (ps == null)
            ps = FindObjectOfType<PlayerStats>();

    }

    private void Start()
    {
        musicStates = MusicStates.Lvl1; 
    }
/// <summary>
/// Game starts with no music. As you continue being at 50% or more water level, the music will increase intensity. else it will decrease 
/// </summary>
    private void Update()
{
    timer += Time.deltaTime;

    if (timer > waterLevelCheckTime)
    {
        if (ps.GetCurrentWaterLevel() / ps.GetStartingWaterLevel() > 0.5f)
        {
            //increase
            if(!musicLayerSystem.Active) musicLayerSystem.StartMusicSystem();
            else
            {
                Debug.Log("increasing!");
                musicLayerSystem.IncreaseMusicLayer();
            }
            
        }
        else
        {
            //decrease 
            Debug.Log("decreasing!");
            musicLayerSystem.DecreaseMusicLayer();
        }



        timer = 0;
    }
    
}
}

public enum MusicStates
{
  Lvl1, //nothing is playing 
  Lvl2, // layer 1 is playing
  Lvl3, //layer 2 is playing
  Lvl4, //layer 3 is playing, full level
          
}

