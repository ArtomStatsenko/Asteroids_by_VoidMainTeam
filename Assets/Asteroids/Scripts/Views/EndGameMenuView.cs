using System;
using UnityEngine;
using UnityEngine.UI;

public sealed class EndGameMenuView : MonoBehaviour
{
    public event Action OnMainMenuButtonClickEvent;
    public event Action OnRestartButtonClickEvent;
    public event Action OnExitButtonClickEvent;
    public event Action<bool> OnSetScreenParamsEvent;

    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Text _screenText;
    [SerializeField] private Text _firstPlayerScoreValue;
    [SerializeField] private Text _secondPlayerScoreValue;

    public GameObject GameObject => gameObject;

    public GameModeManager GameMode { get; set; }

    private void Start()
    {        
        _homeButton.onClick.AddListener(ToMainMenu);
        _restartButton.onClick.AddListener(RestartGame);
        _exitButton.onClick.AddListener(ExitGame);
    }

    private void ExitGame()
    {
        OnExitButtonClickEvent?.Invoke();
    }

    private void RestartGame()
    {
        OnRestartButtonClickEvent?.Invoke();
    }

    private void ToMainMenu()
    {
        OnMainMenuButtonClickEvent?.Invoke();
    }

    public void SetGameResult(string text, int P1Score, int P2Score = 0)
    {
        if (GameMode == GameModeManager.Singleplayer)
        {
            _secondPlayerScoreValue.gameObject.SetActive(false);

            _screenText.text = $"{text}";
            _firstPlayerScoreValue.text = $"SCORE: {P1Score}";
        }
        else
        {
            _secondPlayerScoreValue.gameObject.SetActive(true);

            _screenText.text = $"{text}";
            _firstPlayerScoreValue.text = $"Player 1 score: {P1Score}";
            _secondPlayerScoreValue.text = $"Player 2 score: {P2Score}";
        }
    }

    public void ShowResult(bool isVictory)
    {
        OnSetScreenParamsEvent?.Invoke(isVictory);
    }
}
