using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    [SerializeField] private MusicLayerSystem musicLayerSystem;

    [SerializeField] private PlayerStats ps;

     private float waterLevelCheckTime = 7.5f;
    private float timer; 
    private void Awake()
    {
        if (ps == null)
            ps = FindObjectOfType<PlayerStats>();

    }

    private void Start()
    {
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
                musicLayerSystem.IncreaseMusicLayer();
            }
            
        }
        else
        {
            //decrease 
            musicLayerSystem.DecreaseMusicLayer();
        }



        timer = 0;
    }
    
}
}


