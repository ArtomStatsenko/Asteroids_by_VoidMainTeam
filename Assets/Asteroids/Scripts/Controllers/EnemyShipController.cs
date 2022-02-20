using System;
using UnityEngine;
using Object = UnityEngine.Object;

public sealed class EnemyShipController : ShipController
{
    public event Action OnDiedEvent;
    public event Action<int> OnHpChangedEvent;
    public event Action<int> OnArmorChangedEvent;

    private EnemyShipModel _model;
    private EnemyShipView _view;
    private EnemyShipMovement _movementController;
    private AudioController _audioController;
    private EffectController _effectController;

    public EnemyShipController(ShipData data, AudioController audioController, EffectController effectController) : base(data)
    {
        _model = new EnemyShipModel(data);
        _audioController = audioController;
        _effectController = effectController;
    }

    public override void Init(Vector3 spawnPosition)
    {
        GameObject shipGameObject = Object.Instantiate(Data.ShipPrefab, spawnPosition, Quaternion.identity);
        _view = shipGameObject.GetComponent<EnemyShipView>();

        BulletStartPoint = _view.BulletSpawnPoint;

        _view.SetMaxHealth(_model.MaxHP);
        _view.SetMaxArmor(_model.MaxArmor);

        if (shipGameObject.TryGetComponent(out CapsuleCollider collider))
        {
            _movementController = new EnemyShipMovement();
            _movementController.Init(_view.Rigidbody, _model, collider);
        }

        OnEnable();
    }

    public override void OnEnable()
    {
        _view.OnDamagedEvent += RecieveDamage;
        _view.OnDamagedEvent += CreateHittingEffects;
        OnHpChangedEvent += _view.SetHealth;
        OnArmorChangedEvent += _view.SetArmor;
        OnDiedEvent += _view.Die;
        OnDiedEvent += CreateExplosionEffects;
        OnDiedEvent += OnDisable;
    }

    public override void OnDisable()
    {
        _view.OnDamagedEvent -= RecieveDamage;
        _view.OnDamagedEvent -= CreateHittingEffects;
        OnHpChangedEvent -= _view.SetHealth;
        OnArmorChangedEvent -= _view.SetArmor;
        OnDiedEvent -= _view.Die;
        OnDiedEvent -= CreateExplosionEffects;
        OnDiedEvent -= OnDisable;
    }

    public override void Execute()
    {
        _movementController.Execute();
    }

    public override void FixedExecute()
    {
        _movementController.FixedExecute();
    }

    private void RecieveDamage(int damage)
    {
        if (_model.CurrentArmor > 0)
        {
            _model.CurrentArmor -= damage;
            OnArmorChangedEvent?.Invoke(_model.CurrentArmor);
        }
        else
        {
            _model.CurrentHP -= damage;
            OnHpChangedEvent?.Invoke(_model.CurrentHP);
        }

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
        _audioController.Play(AudioClipManager.ShipHitting);
        _effectController.CreateWorld(EffectManager.ShipHitting, _view.transform);
    }
    private void CreateExplosionEffects()
    {
        _audioController.Play(AudioClipManager.ShipExplosion);
        _effectController.CreateWorld(EffectManager.ShipExplosion, _view.transform);
    }
}