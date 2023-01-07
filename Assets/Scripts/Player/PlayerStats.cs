using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float startingHealth = 3f;
    [SerializeField] private float maxHealth = 3f;

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
            // TakeDamage(other);
        }
        else if (other.CompareTag("WaterDrop"))
        {
            CollectWaterDrop(other);
        }
    }

    /*
    private void TakeDamage(GameObject damagingObject) {
        float touchDamage = damagingObject.GetComponent<EnemyStats>().touchDamage;

        health = Mathf.Max(health - touchDamage, 0);
        Debug.Log("Health: " + health);

        if (health == 0f)
        {
            Die();
        }
    }
    */

    private void Die()
    {
        Debug.Log("Player Dead");
    }

    private void CollectWaterDrop(GameObject waterDrop)
    {
        waterDrop.SetActive(false);
        levelManager.CollectDrop();
    }
}
