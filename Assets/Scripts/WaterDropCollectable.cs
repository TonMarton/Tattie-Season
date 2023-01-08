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
    [SerializeField] float waterIncreaseAmount = 0.25f;
    private LevelManager lvlManager;

    bool isTriggeredAlready = false;
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
        if (col.gameObject.CompareTag("Player") && !isTriggeredAlready)
        {
            isTriggeredAlready = true;
            lvlManager.CollectDrop();

            this.gameObject.SetActive(false);
        }
    }
}
