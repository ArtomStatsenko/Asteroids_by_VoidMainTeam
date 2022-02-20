using UnityEngine;

public class AsteroidModel
{
    private float _moveSpeed;
    private float _rotationSpeed;
    private float _lifeTime;
    private float _size;
    private int _currentHP;
    private int _maxHP;
    private int _damage;
    private int _paramValue;
    private int _speedMultiplier = 3;
    private int _healthMultiplier = 10;
    private float _sizeMultiplier = 0.5f;

    public float MoveSpeed => _moveSpeed;
    public float RotationSpeed => _rotationSpeed;
    public float LifeTime => _lifeTime;
    public float Size => _size;
    public int Damage => _damage;
    public int ParamValue => _paramValue;
    public int CurrentHP
    {
        get => _currentHP;
        set => _currentHP = value;
    }

    public AsteroidModel(AsteroidData data)
    {
        _paramValue = Random.Range(data.MinSize, data.MaxSize + 1);
        _damage = ParamValue;
        _size = ParamValue * _sizeMultiplier;
        _maxHP = ParamValue * _healthMultiplier;
        _currentHP = _maxHP;
        _moveSpeed = Mathf.Abs(ParamValue - (data.MaxSize + 1)) * _speedMultiplier;
        _rotationSpeed = data.RotationSpeed;
        _lifeTime = data.LifeTime;
    }
}
