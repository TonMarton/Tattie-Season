using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float startingHealth = 3f;
    [SerializeField] private float maxHealth = 3f;

    private float health;

    private void Awake()
    {
        health = startingHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.collider.gameObject;
        Debug.Log("Collision");
        Debug.Log(other.tag);
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy Collision");
            // TODO: eveluate properly if collision should damage the player - do this on enemy
            Debug.Log(other.GetComponent<EnemyStats>());
            TakeDamage(other);
        }
    }

    private void TakeDamage(GameObject damagingObject) {
        float touchDamage = damagingObject.GetComponent<EnemyStats>().touchDamage;

        health = Mathf.Max(health - touchDamage, 0);
        Debug.Log("Health: " + health);

        if (health == 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Dead");
    }
}
