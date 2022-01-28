﻿using UnityEngine;

public sealed class EnemyShipView : ShipView
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

                EffectController.Create(EffectManager.ShipHitting, gameObject.transform);
            }
        }
    }

    public override void Die()
    {
        if (_loseMenu)
        {
            _loseMenu.SetGameEndParams(true);
            _loseMenu.SetScreenActive(true);
        }

        EffectController.Create(EffectManager.ShipExplosion, gameObject.transform);
        Destroy(gameObject);
    }

    private void Awake()
    {
        _hudView = FindObjectOfType<EnemyHUDView>();
        _loseMenu = FindObjectOfType<EndGameMenuView>();
    }

    private void Start()
    {
        if (_loseMenu)
        {
            _loseMenu.gameObject.SetActive(false);
        }
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
