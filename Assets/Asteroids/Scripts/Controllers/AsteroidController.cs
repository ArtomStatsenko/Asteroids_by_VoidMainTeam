using System;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class AsteroidController
{
    public event Action OnDiedEvent;

    private AsteroidModel _model;
    private AsteroidView _view;
    private AsteroidData _data;
    private Rigidbody _rigidBody;
    private GameObjectPool _asteroidPool;
    private float _borderSpawnOffset;
    private float _ySpawnPosition;
    private AudioController _audioController;
    private EffectController _effectController;

    public AsteroidController(
        AsteroidData data, AudioController audioController, 
        EffectController effectController, GameObjectPool asteroidPool)
    {
        _data = data;
        _asteroidPool = asteroidPool;
        _borderSpawnOffset = 0.5f;
        _ySpawnPosition = 8f;
        _audioController = audioController;
        _effectController = effectController;
    }

    public void Move()
    {
        if (_rigidBody)
        {
            _rigidBody.angularVelocity = Random.insideUnitSphere * _model.RotationSpeed;
            _rigidBody.velocity = Vector3.back * _model.MoveSpeed;
        }
    }

    public void Init()
    {
        _model = new AsteroidModel(_data);

        GameObject asteroidGameObject = _asteroidPool.GetGameObject();

        var xSpawnPosition = Random.Range(GameModel.ScreenBorder[Border.Left] + _borderSpawnOffset, GameModel.ScreenBorder[Border.Right] - _borderSpawnOffset);
        asteroidGameObject.transform.position = new Vector3(xSpawnPosition, 0, _ySpawnPosition);
        asteroidGameObject.transform.localScale = Vector3.one * _model.Size;

        _view = asteroidGameObject.GetComponent<AsteroidView>();
        _rigidBody = _view.Rigidbody;
        _view.Die(_model.LifeTime);
    }

    public void AddToQueue(GameObject asteroid)
    {
        _asteroidPool.AddToQueue(asteroid);
    }


    public void OnEnable()
    {
        _view.GetAsteroidHealthEvent += GetHealth;
        _view.GetAsteroidDamageEvent += GetDamage;
        _view.OnDamagedEvent += RecieveDamage;
        _view.OnDamagedEvent += CreateHittingEffects;
        _view.ReturnObjectToPoolEvent += AddToQueue;
        OnDiedEvent += _view.Die;
        OnDiedEvent += CreateExplosionEffects;
        OnDiedEvent += OnDisable;
    }

    public void OnDisable()
    {
        _view.GetAsteroidHealthEvent -= GetHealth;
        _view.GetAsteroidDamageEvent -= GetDamage;
        _view.OnDamagedEvent -= RecieveDamage;
        _view.OnDamagedEvent -= CreateHittingEffects;
        _view.ReturnObjectToPoolEvent -= AddToQueue;
        OnDiedEvent -= _view.Die;
        OnDiedEvent -= CreateExplosionEffects;
        OnDiedEvent -= OnDisable;
    }

    private int? GetHealth() => _model.CurrentHP;

    private int? GetDamage() => _model.Damage;

    private void RecieveDamage(int damage)
    {
        _model.CurrentHP -= damage;

        if (_model.CurrentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDiedEvent?.Invoke();
    }

    private void CreateHittingEffects(int _)
    {
        _audioController.Play(AudioClipManager.AsteroidHitting);
        _effectController.CreateWorld(EffectManager.AsteroidHitting, _view.transform);
    }

    private void CreateExplosionEffects()
    {
        _audioController.Play(AudioClipManager.AsteroidExplosion);
        _effectController.CreateWorld(EffectManager.AsteroidExplosion, _view.transform);
    }
}
