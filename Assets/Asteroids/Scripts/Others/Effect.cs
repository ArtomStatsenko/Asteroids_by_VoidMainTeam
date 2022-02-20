using UnityEngine;
using System;

[Serializable]
public sealed class Effect
{
    public GameObject Prefab;
    public EffectManager Action;
    public float LifeTime;
}