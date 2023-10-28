using Services;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button settingsButton;

    private void Awake()
    {
        startButton.onClick.AddListener(StartGame);
        settingsButton.onClick.AddListener(SettingsManager.Instance.OpenSettings);
    }

    private void StartGame()
    {
        SceneLoader.Instance.LoadScene(SceneLoader.SceneNames.Game);
    }
}
