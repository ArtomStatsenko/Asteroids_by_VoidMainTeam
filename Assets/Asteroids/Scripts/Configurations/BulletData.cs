using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObjects/BulletData", order = 1)]
public sealed class BulletData : ScriptableObject
{
    [SerializeField] private float _bulletSpeed = 15f;
    [SerializeField] private float _lifeTime = 1f;
    [SerializeField] private float _shootDelay = 0.2f;
    [SerializeField] private int _damage = 1;
    [SerializeField] GameObject _bulletPrefab;

    public float BulletSpeed=> _bulletSpeed;        
    public float LifeTime => _lifeTime;
    public float ShootDelay => _shootDelay;
    public int Damage => _damage;
    public GameObject Prefab => _bulletPrefab;
}