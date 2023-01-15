using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public int dropsCollected { get; private set; } = 0;

    public UnityEvent OnCollectWaterDrop;
    public UnityEvent OnGamePaused;

    public GameUI gameUi { get; private set; }
    private Timer timer;

    private void Awake()
    {
        gameUi = gameObject.GetComponentInChildren<GameUI>();
        timer = new Timer(gameUi.hud);
    }

    private void Start()
    {
        timer.isActive = true;
    }

    private void Update()
    {
        timer.UpdateTime();
    }

    public void CollectDrop()
    {
        OnCollectWaterDrop?.Invoke();
        dropsCollected += 1;
        Debug.Log("DropsCollected: " + dropsCollected);
    }


    public void PauseGame()
    {
        OnGamePaused?.Invoke();
        timer.isActive = false;
        Time.timeScale = 0;
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1;
        timer.isActive = true;
    }

    public void Win()
    {
        Debug.Log("Won");
    }
    
}
