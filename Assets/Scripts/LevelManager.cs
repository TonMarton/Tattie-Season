using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int dropsToCollectForWin = 2;
    public int dropsCollected { get; private set; } = 0;

    public UnityEvent OnCollectWaterDrop;
    public UnityEvent OnGamePaused;

    private TimerSystem timer;

    private void Awake()
    {
        timer = GetComponent<TimerSystem>();
        if (timer == null)
        {
            Debug.LogWarning("Please add a TimerSystem Script to this gameobject");
        }
    }

    private void Start()
    {
        timer.StartTimer();
    }

    public void CollectDrop()
    {
        OnCollectWaterDrop?.Invoke();
        dropsCollected += 1;
        if (dropsCollected == dropsToCollectForWin)
        {
            // TODO: open win gate
        }
        Debug.Log("DropsCollected: " + dropsCollected);
    }


    public void PauseGame()
    {
        OnGamePaused?.Invoke();
        timer.PauseTimer();
        Time.timeScale = 0;
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1;
        timer.StartTimer();
    }

    public void Win()
    {
        Debug.Log("Won");
    }
    
}
