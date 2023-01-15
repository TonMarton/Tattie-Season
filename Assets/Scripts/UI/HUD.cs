using System;
using UnityEngine;
using UnityEngine.UIElements;

public class HUD: MonoBehaviour
{
    private UIDocument uIDocument;

    private VisualElement rootElement;
    private VisualElement waterBar;
    private VisualElement healthBar;
    private Label timeLabel;

    private void Awake()
    {
        uIDocument = gameObject.GetComponent<UIDocument>();

        rootElement = uIDocument.rootVisualElement;
        waterBar = rootElement.Q<VisualElement>("waterbar");
        healthBar = rootElement.Q<VisualElement>("healthbar");
        timeLabel = rootElement.Q<Label>("time");
    }

    public void ChangeWaterBarValue(float value)
    {
        ChangeBarLength(waterBar, value);
    }

    public void ChangeHealthBarValue(float value) {
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
}
