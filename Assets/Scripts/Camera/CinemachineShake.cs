using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVC;
    [SerializeField] private float shakeTime;
    [SerializeField] private float intensity;
    private float currShakeTime  = 0;

    public bool EnableScreenShake { get; set; } = true;

    private void Awake()
    {
        cinemachineVC = GetComponent<CinemachineVirtualCamera>();
    }

    
    //this is beign called with the onhurt event in the playerStats script
    public void ShakeCamera()
    {
        if (!EnableScreenShake) return;
       var cbmp = cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
       cbmp.m_AmplitudeGain = intensity;
       currShakeTime = shakeTime;
    }

    private void Update()
    {
        if (currShakeTime > 0)
        {
            currShakeTime -= Time.deltaTime;
            if (currShakeTime <= 0f)
            {
                var cbmp = cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cbmp.m_AmplitudeGain = 0f;
            }
        }
        
    }
}
