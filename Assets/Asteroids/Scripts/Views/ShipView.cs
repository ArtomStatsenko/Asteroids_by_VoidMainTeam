using System;
using UnityEngine;


public class ShipView : MonoBehaviour, IInteractiveObject, IShip
{
    public event Action<int> OnDamagedEvent;

    public Rigidbody Rigidbody => gameObject.GetComponent<Rigidbody>();
    public Transform BulletSpawnPoint => gameObject.GetComponentInChildren<BulletSpawnMarker>().transform;

    private void OnTriggerEnter(Collider other)
    { 
        if (other.TryGetComponent(out IInteractiveObject interactiveObject))
        {
            if(interactiveObject is IBullet)
            {
                var bulletView = (BulletView)interactiveObject;
                int? damage = bulletView.GetBulletDamage();
                if (damage != null)
                {
                    OnDamagedEvent?.Invoke((int)damage);
                }
                AudioController.Play(AudioClipManager.ShipHitting);
                EffectController.Init(EffectManager.ShipHitting, gameObject.transform);
            }
        }
    }

    public void Die()
    {
        AudioController.Play(AudioClipManager.ShipExplosion);
        EffectController.Init(EffectManager.ShipExplosion, gameObject.transform);
        Destroy(gameObject);
    }
}
