using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class GameModeMenuView : MonoBehaviour
{
    public event Action OnSingleplayerButtonClickEvent;
    public event Action OnMultiplayeButtonClickEvent;

    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _singleplayerButton;
    [SerializeField] private Button _multiplayerButton;

    private void Start()
    {
        _backButton.onClick.AddListener(OpenMainMenu);
        _singleplayerButton.onClick.AddListener(StartSingleplayerGame);
        _multiplayerButton.onClick.AddListener(StartMultiplayerGame);
    }
    
    private void OpenMainMenu()
    {
        if (!_mainMenuPanel.activeSelf)
        {
            _mainMenuPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void StartSingleplayerGame()
    {
        OnSingleplayerButtonClickEvent?.Invoke();
    }

    private void StartMultiplayerGame()
    {
        OnMultiplayeButtonClickEvent?.Invoke();
    }
}