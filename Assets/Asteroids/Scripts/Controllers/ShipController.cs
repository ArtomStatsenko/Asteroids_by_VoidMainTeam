using UnityEngine;

public sealed class ShipController
{
    private ShipModel _model;
    private ShipView _view;
    private ShipData _data;
    private Rigidbody _rigidBody;
    private GameObject _gameObject;
    private GameObject _prefab;
    private Vector3 _startPosition;
    private Vector3 _movement;
    private GameModel _gameModel;
    private float _nextChangeDirectionTime;
    private Transform _bulletStartPoint;

    public Transform BulletStartPoint => _bulletStartPoint;

    public ShipController(ShipData data, GameModel gameModel)
    {
        _data = data;
        _startPosition = data.StartPosition;
        _prefab = data.ShipPrefab;
        _gameModel = gameModel;
    }

    public void Init()
    {
        _model = new ShipModel(_data);
        GameObject shipGameObject = Object.Instantiate(_prefab, _startPosition, Quaternion.identity);
        _view = shipGameObject.GetComponent<ShipView>();
        _rigidBody = _view.Rigidbody;
        _gameObject = _view.gameObject;
        _bulletStartPoint = _view.BulletSpawnPoint;
    }

    public void Execute(ShipType type)
    {
        if (type == ShipType.Player)
        {
            _movement = GetMovementDirection();
        }
        else if (type == ShipType.Enemy && Time.time > _nextChangeDirectionTime)
        {
            _movement = GetRandomHorizontalMovementDirection();
            _nextChangeDirectionTime += Random.Range(1f, 5f);
        }        

        LimitFlightArea(_gameModel.LeftScreenBorder, _gameModel.RightScreenBorder,
            _gameModel.TopScreenBorder, _gameModel.BottomScreenBorder);
    }

    public void FixedExecute()
    {
        Move(_movement);
    }

    private Vector3 GetMovementDirection()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        return new Vector3(horizontal, 0, vertical);
    }

    private Vector3 GetRandomHorizontalMovementDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), 0f, 0f);
    }

    private void Move(Vector3 movement)
    {
        if (_rigidBody)
        {        
            _rigidBody.velocity = movement * _model.MoveSpeed;
            _rigidBody.rotation = Quaternion.Euler(0, 0, -_rigidBody.velocity.x * _model.TurnSpeed);
        }
    }

    public void LimitFlightArea(float leftLimit, float rightLimit, float topLimit, float bottomLimit)
    {
        if (_gameObject)
        {
            float x = Mathf.Clamp(_gameObject.transform.position.x, leftLimit, rightLimit);
            float z = Mathf.Clamp(_gameObject.transform.position.z, bottomLimit, topLimit);

            _gameObject.transform.position = new Vector3(x, 0, z);
        }
    }
}

public enum ShipType
{
    Player = 1,
    Enemy = 2
}