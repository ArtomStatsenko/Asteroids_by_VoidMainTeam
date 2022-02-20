using UnityEngine;

[CreateAssetMenu(fileName = "EffectData", menuName = "ScriptableObjects/EffectData", order = 1)]
public class EffectData : ScriptableObject
{
    [SerializeField] private Effect[] _effects;

    public Effect[] Effects => _effects;
}
