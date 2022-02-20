using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class EndGameMenuController
{
    private UIView _uIView;
    private PlayerHUDView _firstPlayerHUD;
    private PlayerHUDView _secondPlayerHUD;
    private EnemyHUDView _enemyHUDView;
    private EndGameMenuModel _model;
    private EndGameMenuView _view;
    private AudioClipManager _audioClipType;
    private AudioController _audioController;
    private GameModeManager _gameModeManager;

    public EndGameMenuController(AudioController audioController, GameModeManager gameModeManager)
    {
        _model = new EndGameMenuModel();
        _audioController = audioController;
        _gameModeManager = gameModeManager;

        _view = Object.FindObjectOfType<EndGameMenuView>();
        _view.GameMode = gameModeManager;

        _uIView = Object.FindObjectOfType<UIView>();
        _firstPlayerHUD = _uIView.FirstPlayer;
        _secondPlayerHUD = _uIView.SecondPlayer;
        _enemyHUDView = Object.FindObjectOfType<EnemyHUDView>();

        SetScreenActive(false);
    }

    public void SetScreenActive(bool flag)
    {
        _firstPlayerHUD.SetScreenActive(!flag);

        if (_gameModeManager == GameModeManager.Multiplayer)
        {
            _secondPlayerHUD.SetScreenActive(!flag);
        }

        _view.GameObject.SetActive(flag);

        Time.timeScale = flag ? 0 : 1;
    }

    public void SetGameEndParams(bool isVictory)
    {
        string endGameText;

        if (isVictory)
        {
            endGameText = EndGameMenuModel.WIN_GAME;
            _audioClipType = AudioClipManager.VictoryMusic;
        }
        else
        {
            endGameText = EndGameMenuModel.LOSE_GAME;
            _audioClipType = AudioClipManager.GameOverMusic;
        }

        _model.FirstPlayerScore = _firstPlayerHUD.GetScore();
        _model.SecondPlayerScore = _secondPlayerHUD.GetScore();

        if (_gameModeManager == GameModeManager.Singleplayer)
        {
            _view.SetGameResult(endGameText, _model.FirstPlayerScore);
        }
        else
        {
            _view.SetGameResult(endGameText, _model.FirstPlayerScore, _model.SecondPlayerScore);
        }
    }

    private void ToMainMenu()
    {
        _audioController.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    private void RestartGame()
    {
        _audioController.Clear();
        SetScreenActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnEnable()
    {
        _view.OnMainMenuButtonClickEvent += ToMainMenu;
        _view.OnExitButtonClickEvent += ExitGame;
        _view.OnRestartButtonClickEvent += RestartGame;
        _view.OnSetScreenParamsEvent += ShowEndGameMenu;
    }

    public void OnDisable()
    {
        _view.OnMainMenuButtonClickEvent -= ToMainMenu;
        _view.OnExitButtonClickEvent -= ExitGame;
        _view.OnRestartButtonClickEvent -= RestartGame;
        _view.OnSetScreenParamsEvent -= ShowEndGameMenu;
    }

    private void ShowEndGameMenu(bool isVictory)
    {
        if ((_gameModeManager == GameModeManager.Singleplayer) ||
            (_gameModeManager == GameModeManager.Multiplayer && _firstPlayerHUD.IsDead && _secondPlayerHUD.IsDead || _enemyHUDView.IsDead))
        {
            SetGameEndParams(isVictory);

            _audioController.Clear();
            _audioController.Play(_audioClipType, true);

            SetScreenActive(true);
        } 
    }
}
