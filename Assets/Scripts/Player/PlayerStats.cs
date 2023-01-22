using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float startingHealth = 3f;
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float startingWaterLevel = 1f;
    [SerializeField] private float maxWaterLevel = 10f;
    [SerializeField] private float waterDepletionPerSecond = 0.1f;

    [Header("Events")]
    public UnityEvent OnHurt;
    public UnityEvent OnDeath;

    public float health { get; private set; }
    [Header("Debug")]
    [SerializeField] private float waterLevel;
    private LevelManager levelManager;

    private HUD hud;

    private void Awake()
    {
        health = startingHealth;
        waterLevel = startingWaterLevel;
    }

    private void Start()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        InitHUD();
    }

    private void InitHUD()
    {
        hud = levelManager.gameUi.hud;
        hud.ChangeWaterBarValue(waterLevel / maxWaterLevel);
        hud.ChangeHealthBarValue(health / maxHealth);
    }

    private void Update()
    {
        UpdateWaterLevel();
    }

    private void UpdateWaterLevel()
    {
        waterLevel -= waterDepletionPerSecond * Time.deltaTime;
        hud.ChangeWaterBarValue(waterLevel / maxWaterLevel);
        if (waterLevel <= 0f)
        {
            Die();
        }
    }

    public void IncreaseWaterLevel(float amount)
    {
        // Debug.Log("Water level: " + waterLevel + ", Amount: " + amount);
        waterLevel += amount;
        hud.ChangeWaterBarValue(waterLevel / maxWaterLevel);
        Debug.Log("Water level: " + waterLevel);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.collider.gameObject;
        if (other.CompareTag("Enemy"))
        {
            // TODO: eveluate properly if collision should damage the player - do this on enemy
            // TakeDamage(other);
        }
        else if (other.CompareTag("Thorns"))
        {
            JumpOnAttack();
            TakeDamage(1);
        }
    }

    public void TakeDamage(float dmgAmt)
    {

        health = Mathf.Max(health - dmgAmt, 0);
        hud.ChangeHealthBarValue(health / maxHealth);

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
        //Destroy(this.gameObject);
        StartCoroutine(WaitThenDie());
        Debug.Log("Player Dead");
    }

    private IEnumerator WaitThenDie()
    {
        yield return new WaitForSeconds(2.5f);
        this.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //restart game?
    }
    public float GetStartingWaterLevel()
    {
        return startingWaterLevel;
    }

    public float GetCurrentWaterLevel()
    {
        return waterLevel;
    }
}
