using UnityEngine;

public abstract class MovementController
{
    private Rigidbody _rigidbody;
    private ShipModel _shipModel;
    private Transform _transform;
    private float _offsetX;
    private float _offsetZ;
    private float _leftMovementLimit;
    private float _rightMovementLimit;
    private float _topMovementLimit;
    private float _bottomMovementLimit;
    private Vector3 _movement;

    public void Init(Rigidbody rigidbody, ShipModel shipModel, CapsuleCollider collider)
    {
        _rigidbody = rigidbody;
        _shipModel = shipModel;
        _transform = _rigidbody.transform;
        _offsetX = collider.radius;
        _offsetZ = collider.height / 2;
        _leftMovementLimit = GameModel.ScreenBorder[Border.Left] + _offsetX;
        _rightMovementLimit = GameModel.ScreenBorder[Border.Right] - _offsetX;
        _topMovementLimit = GameModel.ScreenBorder[Border.Top] - _offsetZ;
        _bottomMovementLimit = GameModel.ScreenBorder[Border.Bottom] + _offsetZ;
    }

    public void Execute()
    {
        _movement = GetMovementDirection();
        LimitFlightArea();
    }

    public abstract Vector3 GetMovementDirection();

    public void FixedExecute()
    {
        Move();
    }

    private void Move()
    {
        if (_rigidbody)
        {
            _rigidbody.velocity = _movement * _shipModel.MoveSpeed * Time.fixedDeltaTime * 60;
            _rigidbody.rotation = 
                Quaternion.Euler(0, 0, -_rigidbody.velocity.x * _shipModel.TurnSpeed * Time.fixedDeltaTime * 60);
        }
    }

    private void LimitFlightArea()
    {
        if (_transform)
        {
            float x = Mathf.Clamp(_transform.position.x, _leftMovementLimit, _rightMovementLimit);
            float z = Mathf.Clamp(_transform.position.z, _bottomMovementLimit, _topMovementLimit);
            _transform.position = new Vector3(x, 0, z);
        }
    }
}
