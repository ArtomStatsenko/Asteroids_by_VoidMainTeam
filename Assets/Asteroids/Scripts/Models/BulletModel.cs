public class BulletModel
{
    private float _bulletSpeed;
    private float _lifeTime;
    private int _damage;

    public float BulletSpeed => _bulletSpeed;
    public float LifeTime => _lifeTime;
    public int Damage => _damage;

    public BulletModel(BulletData data)
    {
        _bulletSpeed = data.BulletSpeed;
        _lifeTime = data.LifeTime;
        _damage = data.Damage;
    }
}
