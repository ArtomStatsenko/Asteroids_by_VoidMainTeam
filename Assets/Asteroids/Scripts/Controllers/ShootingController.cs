using UnityEngine;

public class ShootingController
{
    private BulletController _bulletController;
    private float _nextShotTime;
    private float _shootDelay;
    private float _shootingDistance = 10f;
    private Transform _startPoint;
    private LayerMask _layer;
    private AudioController _audioController;

    public ShootingController(Transform startPoint, BulletData bullet, 
        LayerMask layer, AudioController audioController, PlayerHUDView playerHUDView = null)
    {
        _startPoint = startPoint;
        _bulletController = new BulletController(bullet, _startPoint, playerHUDView);
        _shootDelay = bullet.ShootDelay;
        _layer = layer;
        _audioController = audioController;
    }

    public void Shoot()
    {
        bool canShoot = Time.time > _nextShotTime;
        bool isEnemyDetected = false;

        if (_startPoint != null)
        {
            isEnemyDetected = Physics.Raycast(_startPoint.position, _startPoint.forward, _shootingDistance, _layer);
        }

        if (canShoot && isEnemyDetected)
        {
            _bulletController.Init();
            _bulletController.OnDisable();
            _bulletController.OnEnable();
            _bulletController.Move();
            _audioController.Play(AudioClipManager.Shot);
            _nextShotTime = Time.time + _shootDelay;
        }
    }
}
