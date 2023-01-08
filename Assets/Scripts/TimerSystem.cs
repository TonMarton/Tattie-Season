using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    [Header("Starting time (in seconds)")]
    [SerializeField] private float startingTime = 45f;

    private bool timerIsActive = false;
    
    [Header("display time for debug")] 
    [SerializeField] private string clockTimeText;
    private void Start()
    {
        
    }

    private void Update()
    {
        if(!timerIsActive) return;

        startingTime -= Time.deltaTime;

        if (startingTime <= 0f)
        {
            clockTimeText = "0";
            return;
        }

        clockTimeText = Mathf.Floor(startingTime).ToString(); 
    }

    public void StartTimer()
    {
        timerIsActive = true;
    }
    
    public void PauseTimer()
    {
        timerIsActive = false;
    }
    
   
    public string DisplayCurrentTime()
    {
        return clockTimeText;
    }
}
