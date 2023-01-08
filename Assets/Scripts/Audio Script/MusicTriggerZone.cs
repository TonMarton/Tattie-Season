using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTriggerZone : MonoBehaviour
{
    [SerializeField] private MusicTriggerSystem musicTriggerSystem;
    private bool playerEntered = false;

    private void Awake()
    {
        playerEntered = false;
        if (musicTriggerSystem == null)
            musicTriggerSystem = FindObjectOfType<MusicTriggerSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>())
        {
            if (playerEntered) return;
            musicTriggerSystem.OnPlayerEnter();
            playerEntered = true;
        }
    }
}
