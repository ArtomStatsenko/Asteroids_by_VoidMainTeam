using System.Collections.Generic;
using UnityEngine;

public static class ResourcesManager
{
    private static GameObject _shipPrefab;

    public static GameObject ShipPrefab
    {
        get => _shipPrefab;
        set => _shipPrefab = value;
    }

    private static GameObject _bulletPrefab;

    public static GameObject BulletPrefab
    {
        get => _bulletPrefab;
        set => _bulletPrefab = value;
    }

    private static GameObject _asteroidPrefab;

    public static GameObject AsteroidPrefab
    {
        get => _asteroidPrefab;
        set => _asteroidPrefab = value;
    }

    public static void LoadPrefab(string prefabName)
    {
        var loadedPrefab = Resources.Load<GameObject>(prefabName);
        var interactiveObject = loadedPrefab.GetComponent<IInteractiveObject>();

        if (interactiveObject is IShip)
        {
            ShipPrefab = loadedPrefab;
        }
        else if (interactiveObject is IBullet)
        {
            BulletPrefab = loadedPrefab;
        }
        else if (interactiveObject is IAsteroid)
        {
            AsteroidPrefab = loadedPrefab;
        }
        else
        {
            return;
        }
    }

    public static void LoadPrefabsByNameList(List<string> prefabNamesList)
    {
        foreach (string _loadedPrefabName in prefabNamesList)
        {
            LoadPrefab(_loadedPrefabName);
        }
    }
}
