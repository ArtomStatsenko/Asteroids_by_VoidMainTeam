using UnityEngine;


[CreateAssetMenu(fileName = "AsteroidData", menuName = "ScriptableObjects/AsteroidData", order = 1)]
public sealed class AsteroidData : ScriptableObject
{
    [SerializeField] private float _minMoveSpeed = 4f;
    [SerializeField] private float _maxMoveSpeed = 8f;
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _rotationSpeed = 3f;
    [SerializeField] private float _lifeTime = 1;

    [SerializeField] private int _hp = 1;
    [SerializeField] private int _damage = 1;
    [SerializeField] private int _minSize = 1;
    [SerializeField] private int _maxSize = 3;
    [SerializeField] private int _size = 1;

    [SerializeField] private GameObject _asteroidPrefab;

    public GameObject AsteroidPrefab => _asteroidPrefab;
    public float MinMoveSpeed => _minMoveSpeed;
    public float MaxMoveSpeed => _maxMoveSpeed;
    public float MoveSpeed => _moveSpeed;
    public float RotationSpeed => _rotationSpeed;
    public float LifeTime => _lifeTime;
    public int HP => _hp;
    public int Damage => _damage;
    public int MinSize => _minSize;
    public int MaxSize => _maxSize;
    public int Size => _size;
}