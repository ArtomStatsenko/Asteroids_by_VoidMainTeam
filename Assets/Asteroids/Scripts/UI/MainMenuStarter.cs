using UnityEngine;

public sealed class MainMenuStarter : MonoBehaviour
{
    [SerializeField] private MainMenuView _mainMenuView;
    [SerializeField] private SettingsMenuView _settingsMenuView;
    [SerializeField] private GameModeMenuView _gameModeMenuView;
    [SerializeField] private GameData _gameData;

    private MainMenuController _mainMenuController;
    private SettingsMenuController _settingsMenuController;
    private GameModeMenuController _gameModeMenuController;
    private AudioController _audioController;

    private void Start()
    {
        _audioController = new AudioController(_gameData.AudioData, _gameData.Volume);
        _mainMenuController = new MainMenuController(_mainMenuView);
        _settingsMenuController = new SettingsMenuController(_settingsMenuView, _gameData);
        _gameModeMenuController = new GameModeMenuController(_gameModeMenuView, _gameData);

        _mainMenuController.OnEnable();
        _settingsMenuController.OnEnable();
        _gameModeMenuController.OnEnable();

        _settingsMenuController.OnVolumeChandedEvent += _audioController.SetVolume;

        _audioController.Play(AudioClipManager.NewGameMusic, true);
    }
}
