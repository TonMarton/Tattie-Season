using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UnityEvent playButtonHoverSound;
    [SerializeField] private UnityEvent playButtonClickSound;

    private UIDocument uIDocument;

    private VisualElement rootElement;
    private Button playButton;
    private Button quitButton;

    private void Awake()
    {
        uIDocument = gameObject.GetComponent<UIDocument>();

        rootElement = uIDocument.rootVisualElement;
        playButton = rootElement.Q<Button>("play-button");
        quitButton = rootElement.Q<Button>("quit-button");

        playButton.clickable.clicked += OnPlayButtonClicked;
        quitButton.clickable.clicked += OnQuitButtonClicked;

        playButton.RegisterCallback<MouseOverEvent>((type) => OnButtonHover());
        quitButton.RegisterCallback<MouseOverEvent>((type) => OnButtonHover());

    }

    private void OnButtonHover() {
        playButtonHoverSound.Invoke();
    }

    private void OnButtonClicked() {
        playButtonClickSound.Invoke();
    }

    private void OnPlayButtonClicked()
    {
        OnButtonClicked();
        SceneManager.LoadScene(1);
    }

    private void OnQuitButtonClicked()
    {
        OnButtonClicked();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }

    private void OnDestroy()
    {
        playButton.clickable.clicked -= OnPlayButtonClicked;
        quitButton.clickable.clicked -= OnQuitButtonClicked;

        playButton.UnregisterCallback<MouseOverEvent>((type) => OnButtonHover());
        quitButton.UnregisterCallback<MouseOverEvent>((type) => OnButtonHover());
    }
}
