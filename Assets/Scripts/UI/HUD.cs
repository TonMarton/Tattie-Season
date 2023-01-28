using System;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD
{
	private PauseMenu pauseMenu;

	private VisualElement waterBar;
	private VisualElement healthBar;
	private VisualElement timerContainer;
	private Label timeLabel;

	public HUD(VisualElement rootElement, PauseMenu pauseMenu)
	{
		this.pauseMenu = pauseMenu;
		pauseMenu.onTimerToggleChanged += ToggleTimer;

		waterBar = rootElement.Q<VisualElement>("waterbar");
		healthBar = rootElement.Q<VisualElement>("healthbar");
		timerContainer = rootElement.Q<VisualElement>("timer-container");
		timeLabel = rootElement.Q<Label>("time");
	}

	public void ChangeWaterBarValue(float value)
	{
		ChangeBarLength(waterBar, value);
	}

	public void ChangeHealthBarValue(float value)
	{
		ChangeBarLength(healthBar, value);
	}

	private void ChangeBarLength(VisualElement bar, float xScale)
	{
		bar.style.scale = new StyleScale(new Scale(new Vector2(xScale, 1f)));
	}

	public void ChangeTimeText(float time)
	{
		TimeSpan timeSpan = TimeSpan.FromSeconds(time);
		string timeText = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
		timeLabel.text = timeText;
	}

	public void ToggleTimer(bool shouldShow)
	{
		timerContainer.style.display = shouldShow ? DisplayStyle.Flex : DisplayStyle.None;
	}

	public void UnsubscribeFromEvents()
	{
		pauseMenu.onTimerToggleChanged -= ToggleTimer;
	}
}
