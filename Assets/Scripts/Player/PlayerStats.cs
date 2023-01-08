using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float startingHealth = 3f;
    [SerializeField] private float startingWaterLevel = 1f;
    [SerializeField] private float waterDepletionPerSecond = 0.1f;

    [Header("Events")] 
    public UnityEvent OnHurt; 
    public UnityEvent OnDeath;
    
    public float health { get; private set; }
    private float waterLevel;
    private LevelManager levelManager;

    private void Awake()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>(); ;

        health = startingHealth;
        waterLevel = startingWaterLevel;
    }

    private void Update()
    {
        UpdateWaterLevel();
    }

    private void UpdateWaterLevel()
    {
        waterLevel = waterLevel - waterDepletionPerSecond * Time.deltaTime;
        if (waterLevel <= 0f)
        {
            Die();
        }
    }

    public void IncreaseWaterLevel(float amount)
    {
        Debug.Log("Water level: " + waterLevel + ", Amount: " + amount);
        waterLevel += amount;
        //TODO: Update Water Level on the UI
        Debug.Log("Water level: " + waterLevel);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.collider.gameObject;
        if (other.CompareTag("Enemy"))
        {
            // TODO: eveluate properly if collision should damage the player - do this on enemy
            TakeDamage(other);
        }
        else if (other.CompareTag("Thorns"))
        {
            JumpOnAttack();
            TakeDamage(other);
        }
    }
    
    private void TakeDamage(GameObject damagingObject)
    {
        float touchDamage = damagingObject.GetComponent<EnemyStats>().touchDamage;

        health = Mathf.Max(health - touchDamage, 0);
        Debug.Log("Health: " + health);
        OnHurt?.Invoke();
        if (health == 0f)
        {
            Die();
        }
    }

    private void JumpOnAttack()
    {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 10f);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5f);
    }

    private void Die()
    {
        OnDeath?.Invoke();
        Destroy(this.gameObject);
        Debug.Log("Player Dead");
    }
}
