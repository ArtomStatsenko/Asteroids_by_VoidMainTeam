using UnityEngine.SceneManagement;

internal class GameModeMenuController
{
    private GameData _gameData;
    private GameModeMenuView _view;

    public GameModeMenuController(GameModeMenuView view, GameData gameData)
    {
        _view = view;
        _gameData = gameData;
    }

    public void OnEnable()
    {
        _view.OnSingleplayerButtonClickEvent += StartSingleplayerGame;
        _view.OnMultiplayeButtonClickEvent += StartMultiplayerGame;
    }
       
    public void OnDisable()
    {
        _view.OnSingleplayerButtonClickEvent -= StartSingleplayerGame;
        _view.OnMultiplayeButtonClickEvent -= StartMultiplayerGame;
    }

    private void StartSingleplayerGame()
    {
        _gameData.GameMode = GameModeManager.Singleplayer;
        LoadLevel();
    }

    private void StartMultiplayerGame()
    {
        _gameData.GameMode = GameModeManager.Multiplayer;
        LoadLevel();
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}