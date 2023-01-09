using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private Sprite deathSprite;
    // Start is called before the first frame update
    void Start()
    {
        if (sr == null)
            sr = GetComponentInParent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.GetComponent<EnemyHeadCheck>()){
            // collision.GetComponent<EnemyStats>().TakeDamage(1);
            StartCoroutine(WaitThenDie());
        }
    }

    private IEnumerator WaitThenDie()
    {
        sr.sprite = deathSprite; 
        yield return new WaitForSeconds(0.75f);
        Destroy(transform.parent.gameObject);
    }
}
