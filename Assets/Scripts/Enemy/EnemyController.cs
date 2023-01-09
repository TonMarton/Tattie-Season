using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    private Transform playerPos;
    private Vector2 currentPos;
    private Vector2 initialPosition;
    private int direction;
    public float movingSpeed;
    public float maxDist;
    public float minDist;
    public float speedEnemy;
    public float distance;
    public float currentPosX;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        playerPos = player.GetComponent<Transform>();
        currentPos = GetComponent<Transform>().position;
        initialPosition = transform.localPosition;
        direction = -1;
        maxDist += transform.localPosition.x;
        minDist = maxDist - 5;
        movingSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerPos == null) return;
        
        currentPosX = transform.localPosition.x;
        if(Vector2.Distance(currentPos, playerPos.position) < distance)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, playerPos.position, speedEnemy * 0.1f);
            minDist = transform.localPosition.x + 3 *-1;
            maxDist = transform.localPosition.x + 3;
        } else {
           switch (direction)
                {
                    case -1:
                        // Moving Left
                        if( transform.localPosition.x > minDist)
                        {
                            GetComponent <Rigidbody2D>().velocity = new Vector2(-movingSpeed,GetComponent<Rigidbody2D>().velocity.y);
                        }
                        else
                        {
                            direction = 1;
                        }
                        break;
                    case 1:
                        //Moving Right
                        if(transform.localPosition.x < maxDist)
                        {
                            GetComponent <Rigidbody2D>().velocity = new Vector2(movingSpeed,GetComponent<Rigidbody2D>().velocity.y);
                        }
                        else
                        {
                            direction = -1;
                        }
                        break;
                }
        }
    }

   
}
