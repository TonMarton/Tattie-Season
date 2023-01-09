using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTriggerZone : MonoBehaviour
{
    [SerializeField] private MusicController musicController;
    private bool playerEntered = false;

    private void Awake()
    {
        playerEntered = false;
        if (musicController == null)
            musicController = FindObjectOfType<MusicController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>())
        {
/*            if (playerEntered) return;
            musicTriggerSystem.OnPlayerEnter();
            playerEntered = true;
            */
        }
    }
}
