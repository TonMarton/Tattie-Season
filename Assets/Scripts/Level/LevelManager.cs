using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
	public delegate void ResumeGameDelegate();
	public delegate void GoToMainMenuDelegate();

	public int DropsCollected { get; private set; } = 0;

	public UnityEvent OnCollectWaterDrop;
	public UnityEvent OnGamePaused;

	public Settings settings;
	public GameUI gameUi {
		get; private set;
	}
	private Timer timer;

	private InputAction pauseAction;
	private bool isGamePaused = false;

	private void Awake()
	{
		settings = new Settings();
		gameUi = new GameUI(
			gameObject.GetComponentInChildren<UIDocument>(),
			settings,
			ResumeGame,
			GoToMainMenu
		);
		timer = new Timer(gameUi.hud);
	}

	private void Start()
	{
		pauseAction = GameObject.Find("Player").GetComponent<PlayerInput>().actions.FindAction("Pause");
		pauseAction.performed += GamePausedResumed;
		timer.isActive = true;
		UIAudio uiAudio = GameObject.Find("Sound").GetComponentInChildren<UIAudio>();
		gameUi.pauseMenu.onMusicSliderChanged += uiAudio.SetMusicLvl;
		gameUi.pauseMenu.onSfxSliderChanged += uiAudio.SetSfxLvl;
	}

	private void Update()
	{
		timer.UpdateTime();
	}

	public void CollectDrop()
	{
		OnCollectWaterDrop?.Invoke();
		DropsCollected += 1;
		Debug.Log("DropsCollected: " + DropsCollected);
	}

	private void GamePausedResumed(InputAction.CallbackContext context)
	{
		if (isGamePaused) {
			if (gameUi.pauseMenu.IsMainPageActive()) {
				ResumeGame();
			} else {
				gameUi.pauseMenu.GoToMainPage();
			}
		} else {
			PauseGame();
		}
	}

	public void PauseGame()
	{
		isGamePaused = true;
		OnGamePaused?.Invoke();
		gameUi.pauseMenu.TogglePauseMenu();
		timer.isActive = false;
		Time.timeScale = 0;
	}

	public void ResumeGame()
	{
		isGamePaused = false;
		gameUi.pauseMenu.TogglePauseMenu();
		Time.timeScale = 1;
		timer.isActive = true;
	}

	public void Win()
	{
		Debug.Log("Won");
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene(0);
	}

	private void OnDestroy()
	{
		gameUi.UnsubscribeFromEvents();
		pauseAction.performed -= GamePausedResumed;
	}
}
