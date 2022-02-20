using UnityEngine;

public sealed class EnemyShipView : ShipView, IEnemy
{
    private EnemyHUDView _hudView;
    private EndGameMenuView _loseMenu;

    public override void Interact(Collider other)
    {
        if (other.TryGetComponent(out IInteractiveObject interactiveObject))
        {
            if (interactiveObject is IBullet)
            {
                var bulletView = (BulletView)interactiveObject;
                int? damage = bulletView.GetBulletDamage();
                if (damage != null)
                {
                    GetDamage((int)damage);
                }
            }
        }
    }

    public override void Die()
    {
        _hudView.IsDead = true;

        if (_loseMenu)
        {
            _loseMenu.ShowResult(true);
        }

        Destroy(gameObject);
    }

    private void Awake()
    {
        _hudView = FindObjectOfType<EnemyHUDView>();
        _loseMenu = FindObjectOfType<EndGameMenuView>();
    }

    public void SetMaxHealth(int health)
    {
        _hudView.SetMaxHealth(health);
    }

    public void SetHealth(int health)
    {
        _hudView.SetHealth(health);
    }

    public void SetMaxArmor(int armor)
    {
        _hudView.SetMaxArmor(armor);
    }

    public void SetArmor(int armor)
    {
        _hudView.SetArmor(armor);
    }
}
