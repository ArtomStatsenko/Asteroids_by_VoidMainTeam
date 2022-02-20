using System;
using System.Collections;
using UnityEngine;

public class AsteroidView : MonoBehaviour, IInteractiveObject, IAsteroid
{
    public event Func<int?> GetAsteroidDamageEvent;
    public event Func<int?> GetAsteroidHealthEvent;
    public event Action<int> OnDamagedEvent;
    public event Action<GameObject> ReturnObjectToPoolEvent;

    public Rigidbody Rigidbody => gameObject.GetComponent<Rigidbody>();

    private void OnTriggerEnter(Collider other)
    {
        Interact(other);
    }

    public void Interact(Collider other)
    {
        var interactiveObject = other.gameObject.GetComponent<IInteractiveObject>();

        if (interactiveObject is IAsteroid)
        {
            return;
        }
        if (interactiveObject is IBullet)
        {
            var bulletView = (BulletView)interactiveObject;
            int? damage = bulletView.GetBulletDamage();
            if (damage != null)
            {
                OnDamagedEvent?.Invoke((int)damage);
            }
        }
        if (interactiveObject is IShip)
        {            
            Die();
        }
    }

    public void Die(float lifeTime)
    {
        StartCoroutine(nameof(ReturnObjectToPoolTimer), lifeTime);
    }

    private IEnumerator ReturnObjectToPoolTimer(float time)
    {
        yield return new WaitForSeconds(time);
        ReturnObjectToPool(gameObject);
        StopCoroutine(nameof(ReturnObjectToPoolTimer));
    }

    public void Die()
    {
        ReturnObjectToPool(gameObject);
    }

    public int? GetAsteroidDamage() => GetAsteroidDamageEvent?.Invoke();

    public int? GetAsteroidHealth() => GetAsteroidHealthEvent?.Invoke();
    
    public void ReturnObjectToPool(GameObject asteroid) => ReturnObjectToPoolEvent?.Invoke(asteroid);
}
