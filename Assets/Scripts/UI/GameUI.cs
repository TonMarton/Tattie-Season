using UnityEngine.UIElements;

public class GameUI
{
	private VisualElement rootElement;

	public HUD hud {
		get; private set;
	}

	public PauseMenu pauseMenu {
		get; private set;
	}

	public GameUI(UIDocument uiDocument, Settings settings, LevelManager.ResumeGameDelegate resumeGameCallback, LevelManager.GoToMainMenuDelegate goToMainMenuCallback)
	{
		rootElement = uiDocument.rootVisualElement;
		pauseMenu = new PauseMenu(
			rootElement.Q<VisualElement>("PauseMenu"),
			resumeGameCallback,
			goToMainMenuCallback,
			settings.showTimer
		);

		hud = new HUD(rootElement.Q<VisualElement>("HUD"), pauseMenu);
		hud.ToggleTimer(settings.showTimer);
	}

	public void UnsubscribeFromEvents()
	{
		pauseMenu.UnsubscribeFromEvents();
		hud.UnsubscribeFromEvents();
	}
}
