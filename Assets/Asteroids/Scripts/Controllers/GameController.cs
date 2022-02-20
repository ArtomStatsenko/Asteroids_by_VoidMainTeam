using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private ShipData _firstPlayerShip;
    [SerializeField] private ShipData _secondPlayerShip;
    [SerializeField] private ShipData _enemyShip;
    [SerializeField] private BulletData _playerBullet;
    [SerializeField] private BulletData _enemyBullet;
    [SerializeField] private GameData _gameData;

    private ShootingController _firstPlayerShootingController;
    private ShootingController _secondPlayerShootingController;
    private ShootingController _enemyShootingController;
    private SpawnController _spawnController;
    private PlayerShipController _firstPlayerShipController;
    private PlayerShipController _secondPlayerShipController;
    private EnemyShipController _enemyShipController;
    private AudioController _audioController;
    private EffectController _effectController;
    private UIController _uiController;
    private EndGameMenuController _endGameMenuController;

    private BackgroundStars _bgStars;
    private float _currentTime;
    private float speed = 2f;

    private void Awake()
    {
        GameModel.SetScreenBorders();
    }

    private void Start()
    {
        Time.timeScale = 1;
        _bgStars = new BackgroundStars(50);
        _audioController = new AudioController(_gameData.AudioData, _gameData.Volume);
        _effectController = new EffectController(_gameData.EffectData);
        _spawnController = new SpawnController(_gameData, _audioController, _effectController);
        _uiController = new UIController(_audioController, _gameData.GameMode);

        _enemyShipController = _spawnController.SpawnEnemyShip(_enemyShip);

        _firstPlayerShipController = _spawnController.SpawnPlayerShip(
                _firstPlayerShip, 
                _uiController.GetPlayerHUD(PlayerManager.First));

        _firstPlayerShootingController = new ShootingController(
            _firstPlayerShipController.BulletStartPoint, 
            _playerBullet, 
            _firstPlayerShip.ShootingLayer, 
            _audioController,
            _uiController.GetPlayerHUD(PlayerManager.First));

        _enemyShootingController = new ShootingController(
            _enemyShipController.BulletStartPoint, 
            _enemyBullet, 
            _enemyShip.ShootingLayer, 
            _audioController);

        if (_gameData.GameMode == GameModeManager.Multiplayer)
        {
            _secondPlayerShipController = _spawnController.SpawnPlayerShip(
                _secondPlayerShip,
                _uiController.GetPlayerHUD(PlayerManager.Second), 
                PlayerManager.Second);

            _secondPlayerShootingController = new ShootingController(
                _secondPlayerShipController.BulletStartPoint, 
                _playerBullet, 
                _firstPlayerShip.ShootingLayer, 
                _audioController,
                _uiController.GetPlayerHUD(PlayerManager.Second));
        }

        _audioController.Play(AudioClipManager.BackgroundMusic, true);
        _endGameMenuController = new EndGameMenuController(_audioController, _gameData.GameMode);
        _endGameMenuController.OnEnable();
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;

        _firstPlayerShipController?.Execute();
        _enemyShipController?.Execute();
        _secondPlayerShipController?.Execute();

        _spawnController.SpawnAsteroid(_currentTime);
    }

    private void FixedUpdate()
    {
        _bgStars.MoveStars(speed);
        _firstPlayerShipController?.FixedExecute();
        _enemyShipController?.FixedExecute();
        _secondPlayerShipController?.FixedExecute();

        _firstPlayerShootingController?.Shoot();
        _enemyShootingController?.Shoot();
        _secondPlayerShootingController?.Shoot();
    }
}
