using UnityEngine;

public class BulletController
{
    private BulletModel _model;
    private BulletView _view;
    private BulletData _data;
    private Rigidbody _rigidbody;
    private GameObject _prefab;
    private Transform _spawnPosition;
    private GameObjectPool _bulletPool;
    private PlayerHUDView _playerHUDView;

    public BulletController(BulletData data, Transform spawnPosition, PlayerHUDView playerHUDView = null)
    {
        _data = data;
        _prefab = data.Prefab;
        _spawnPosition = spawnPosition;
        _bulletPool = new GameObjectPool(_prefab);
        _playerHUDView = playerHUDView;
    }

    public void Init()
    {
        _model = new BulletModel(_data);
        GameObject bulletGameObject = _bulletPool.GetGameObject();
        bulletGameObject.transform.position = _spawnPosition.position;
        _view = bulletGameObject.GetComponent<BulletView>();
        _rigidbody = _view.Rigidbody;
        _view.Die(_model.LifeTime);
        _view.PlayerHUD = _playerHUDView;
    }

    public void Move()
    {
        if (_rigidbody)
        {
            _rigidbody.velocity = _spawnPosition.forward * _model.BulletSpeed;
        }
    }

    public void AddToQueue(GameObject bullet)
    {
        _bulletPool.AddToQueue(bullet);
    }

    public void OnEnable()
    {
        _view.ReturnBulletToPoolEvent += AddToQueue;
        _view.GetBulletDamageEvent += GetDamage;
    }

    public void OnDisable()
    {
        _view.ReturnBulletToPoolEvent -= AddToQueue;
        _view.GetBulletDamageEvent -= GetDamage;
    }

    private int? GetDamage()
    {        
        return _model.Damage;
    }
}
