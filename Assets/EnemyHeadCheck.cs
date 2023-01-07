using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadCheck : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.GetComponent<PlayerCheck>()){
            rb.velocity = new Vector2(rb.velocity.x, 10f);
            rb.AddForce(Vector2.up * 30f);
        }
    }
}
