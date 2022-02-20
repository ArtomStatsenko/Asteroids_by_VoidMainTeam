using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class MainMenuView : MonoBehaviour
{
    public event Action OnExitButtonClickEvent;

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private GameObject _gameModePanel;

    private void Start()
    {
        _startButton.onClick.AddListener(StartGame);
        _settingsButton.onClick.AddListener(OpenSettings);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        OnExitButtonClickEvent?.Invoke();
    }

    private void OpenSettings()
    {
        if (!_settingsPanel.activeSelf)
        {
            _settingsPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void StartGame()
    {
        if (!_gameModePanel.activeSelf)
        {
            _gameModePanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
