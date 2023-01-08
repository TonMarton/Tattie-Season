using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// All water drop objects have this script, when interact with the player, they send themselves to the level manager 
/// </summary>
public class WaterDropCollectable : MonoBehaviour
{
    private LevelManager lvlManager;

    private void Awake()
    {
        lvlManager = FindObjectOfType<LevelManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            lvlManager.CollectDrop();

            this.gameObject.SetActive(false);
        }
    }
}
