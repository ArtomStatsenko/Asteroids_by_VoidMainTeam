using UnityEngine;

public abstract class ShipController
{
    private Transform _bulletStartPoint;
    private ShipData _data;

    public ShipData Data
    {
        get => _data;
        private set => _data = value;
    }

    public Transform BulletStartPoint
    {
        get => _bulletStartPoint;
        set => _bulletStartPoint = value;
    }

    public ShipController(ShipData data)
    {
        _data = data;
    }

    public abstract void Init(Vector3 spawnPosition);

    public abstract void Execute();

    public abstract void FixedExecute();

    public abstract void OnEnable();

    public abstract void OnDisable();
}