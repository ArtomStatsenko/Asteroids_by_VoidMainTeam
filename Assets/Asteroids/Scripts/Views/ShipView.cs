using System;
using UnityEngine;

public abstract class ShipView : MonoBehaviour, IInteractiveObject, IShip
{
    public event Action<int> OnDamagedEvent;

    public Rigidbody Rigidbody => gameObject.GetComponent<Rigidbody>();
    public Transform BulletSpawnPoint => gameObject.GetComponentInChildren<BulletSpawnMarker>().transform;

    private void OnTriggerEnter(Collider other)
    {
        Interact(other);
    }

    public abstract void Interact(Collider other);

    public abstract void Die();

    public void GetDamage(int damage)
    {
        OnDamagedEvent?.Invoke(damage);
    }
}
