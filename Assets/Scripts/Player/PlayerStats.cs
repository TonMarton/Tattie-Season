using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float startingHealth = 3f;
    [SerializeField] private float maxHealth = 3f;


    [Header("Events")] 
    public UnityEvent OnHurt; 
    public UnityEvent OnDeath;
    
    public float health { get; private set; }
    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>(); ;

        health = startingHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.collider.gameObject;
        if (other.CompareTag("Enemy"))
        {
            // TODO: eveluate properly if collision should damage the player - do this on enemy
            TakeDamage(other);
        }
        else if (other.CompareTag("WaterDrop"))
        {
            CollectWaterDrop(other);
        } else if (other.CompareTag("Thorns")){
            JumpOnAttack();
            TakeDamage(other);
        }
    }

    
    private void TakeDamage(GameObject damagingObject) {
        float touchDamage = damagingObject.GetComponent<EnemyStats>().touchDamage;

        health = Mathf.Max(health - touchDamage, 0);
        Debug.Log("Health: " + health);
        OnHurt?.Invoke();
        if (health == 0f)
        {
            Die();
        }
    }

    private void JumpOnAttack(){
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 10f);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5f);
    }
    private void Die()
    {
        OnDeath?.Invoke();
        Destroy(this.gameObject);
        Debug.Log("Player Dead");
    }

    private void CollectWaterDrop(GameObject waterDrop)
    {
        waterDrop.SetActive(false);
        levelManager.CollectDrop();
    }
}
