using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public sealed class LevelData : ScriptableObject
{
    [SerializeField] private List<AsteroidData> _asteroidDataList;

    public List<AsteroidData> AsteroidDataList => _asteroidDataList;
}