using System;
using UnityEngine;
using Object = UnityEngine.Object;

public sealed class PlayerShipController : ShipController
{
    public event Action OnDiedEvent;
    public event Action<int> OnHpChangedEvent;
    public event Action<int> OnArmorChangedEvent;

    private PlayerShipModel _model;
    private PlayerShipView _view;
    private PlayerManager _player;
    private PlayerShipMovement _movementController;
    private AudioController _audioController;
    private EffectController _effectController;
    private PlayerHUDView _hudView;

    public PlayerShipController(ShipData data, PlayerManager player, AudioController auidoController, 
        EffectController effectController, PlayerHUDView hudView) : base(data)
    {
        _model = new PlayerShipModel(data);
        _player = player;
        _audioController = auidoController;
        _effectController = effectController;
        _hudView = hudView;
    }

    public override void Init(Vector3 spawnPosition)
    {
        GameObject shipGameObject = Object.Instantiate(Data.ShipPrefab, spawnPosition, Quaternion.identity);
        _view = shipGameObject.GetComponent<PlayerShipView>();

        BulletStartPoint = _view.BulletSpawnPoint;

        _view.SetHUDView(_hudView);
        _view.SetMaxHealth(_model.MaxHP);
        _view.SetMaxArmor(_model.MaxArmor);

        if (shipGameObject.TryGetComponent(out CapsuleCollider collider))
        {
            _movementController = new PlayerShipMovement(_player);
            _movementController.Init(_view.Rigidbody, _model, collider);
        }

        OnEnable();
    }

    public override void OnEnable()
    {
        _view.OnDamagedEvent += RecieveDamage;
        _view.OnDamagedEvent += CreateHittingEffects;
        _view.OnBurnEvent += CreateBurnEffect;
        _view.OnDecceleratedEvent += Deccelerate;
        _view.OnAcceleratedEvent += Accelerate;
        _view.OnDiedEvent += Die;
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
        _view.OnBurnEvent -= CreateBurnEffect;
        _view.OnDecceleratedEvent -= Deccelerate;
        _view.OnAcceleratedEvent -= Accelerate;
        _view.OnDiedEvent -= Die;
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

    private void CreateBurnEffect()
    {
        _effectController.CreateLocal(EffectManager.Burning, _view.transform);
    }

    private void Deccelerate(float speedParam)
    {
        _model.MoveSpeed /= speedParam;
        _effectController.CreateLocal(EffectManager.Deceleration, _view.transform);
    }

    private void Accelerate(float speedParam)
    {
        _model.MoveSpeed *= speedParam;
    }
}