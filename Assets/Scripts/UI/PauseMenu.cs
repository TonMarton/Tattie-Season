using System.Collections.Generic;
using UnityEngine.UIElements;

public class PauseMenu
{
    public delegate void OnTimerToggleChanged(bool value);
    public event OnTimerToggleChanged onTimerToggleChanged;
    public delegate void OnSfxSliderChanged(float value);
    public event OnSfxSliderChanged onSfxSliderChanged;
    public delegate void OnMusicSliderChanged(float value);
    public event OnMusicSliderChanged onMusicSliderChanged;

    private enum PauseMenuPage
    {
        MainPage,
        SettingsPage,
    }

    private VisualElement rootElement;
    private VisualElement mainPageContainer;
    private Button settingsButton;
    private Button quitButton;
    private Button resumeButton;
    private VisualElement settingsPageContainer;
    private Toggle timerToggle;
    private Slider sfxSlider;
    private Slider musicSlider;

    private Dictionary<PauseMenuPage, VisualElement> pages;
    private PauseMenuPage activePage = PauseMenuPage.MainPage;

    private LevelManager.ResumeGameDelegate resumeGameCallback;
    private LevelManager.GoToMainMenuDelegate goToMainMenuCallback;

    public PauseMenu(
        VisualElement rootElement,
        LevelManager.ResumeGameDelegate resumeGameCallback,
        LevelManager.GoToMainMenuDelegate goToMainMenuCallback,
        bool shouldShowTimer
        )
    {
        InitUI(rootElement, shouldShowTimer);
        InitMenuPages();
        SubscribeToEvents(resumeGameCallback, goToMainMenuCallback);
    }

    private void InitUI(VisualElement rootElement, bool shouldShowTimer)
    {
        this.rootElement = rootElement;
        this.rootElement.style.display = DisplayStyle.None;
        mainPageContainer = rootElement.Q<VisualElement>("pause-main-container");
        settingsButton = rootElement.Q<Button>("pause-settings-button");
        quitButton = rootElement.Q<Button>("pause-quit-button");
        resumeButton = rootElement.Q<Button>("pause-resume-button");

        settingsPageContainer = rootElement.Q<VisualElement>("pause-settings-container");
        timerToggle = rootElement.Q<Toggle>("timer-toggle");
        timerToggle.value = shouldShowTimer;
        sfxSlider = rootElement.Q<Slider>("sfx-slider");
        musicSlider = rootElement.Q<Slider>("music-slider");
    }

    private void InitMenuPages()
    {
        pages = new Dictionary<PauseMenuPage, VisualElement>() {
            { PauseMenuPage.MainPage, mainPageContainer},
            { PauseMenuPage.SettingsPage, settingsPageContainer},
        };
        activePage = PauseMenuPage.MainPage;
    }
    private void SubscribeToEvents(
        LevelManager.ResumeGameDelegate resumeGameCallback,
        LevelManager.GoToMainMenuDelegate goToMainMenuCallback
        )
    {
        this.resumeGameCallback = resumeGameCallback;
        this.goToMainMenuCallback = goToMainMenuCallback;
        resumeButton.clickable.clicked += this.resumeGameCallback.Invoke;
        settingsButton.clickable.clicked += () => ChangePage(PauseMenuPage.SettingsPage);
        quitButton.clickable.clicked += () => this.goToMainMenuCallback.Invoke();
        timerToggle.RegisterValueChangedCallback(OnTimerToggleValueChanged);
        sfxSlider.RegisterValueChangedCallback(OnSfxSliderValueChanged);
        musicSlider.RegisterValueChangedCallback(OnMusicSliderValueChanged);
    }

    public bool IsMainPageActive()
    {
        return activePage == PauseMenuPage.MainPage;
    }

    public void GoToMainPage()
    {
        ChangePage(PauseMenuPage.MainPage);
    }

    public void TogglePauseMenu()
    {
        rootElement.style.display =
            rootElement.style.display.value == DisplayStyle.None
            ? DisplayStyle.Flex : DisplayStyle.None;
    }

    private void OnTimerToggleValueChanged(ChangeEvent<bool> change)
    {
        onTimerToggleChanged?.Invoke(change.newValue);
    }

    private void OnSfxSliderValueChanged(ChangeEvent<float> change)
    {
        onSfxSliderChanged?.Invoke(change.newValue);
    }

    private void OnMusicSliderValueChanged(ChangeEvent<float> change)
    {
        onMusicSliderChanged?.Invoke(change.newValue);
    }

    private void ChangePage(PauseMenuPage page)
    {
        pages[activePage].style.display = DisplayStyle.None;
        pages[page].style.display = DisplayStyle.Flex;
        activePage = page;
    }

    public void UnsubscribeFromEvents()
    {
        resumeButton.clickable.clicked -= resumeGameCallback.Invoke;
        settingsButton.clickable.clicked -= () => ChangePage(PauseMenuPage.SettingsPage);
        quitButton.clickable.clicked -= () => goToMainMenuCallback.Invoke();
        timerToggle.UnregisterValueChangedCallback(OnTimerToggleValueChanged);
        sfxSlider.UnregisterValueChangedCallback(OnSfxSliderValueChanged);
        musicSlider.UnregisterValueChangedCallback(OnMusicSliderValueChanged);
    }
}