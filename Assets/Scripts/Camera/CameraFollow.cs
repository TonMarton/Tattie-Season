using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
  
    public Transform target;
    public int offset = 10;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y + offset, -10);
    }
}
