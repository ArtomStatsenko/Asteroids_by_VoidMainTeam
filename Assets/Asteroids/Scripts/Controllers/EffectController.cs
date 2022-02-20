using UnityEngine;

public sealed class EffectController
{
    private const string EFFECT = "Effect";
    private EffectData _effectData;
    private GameObject _root;
    private float _yPosition = 2f;

    public EffectController(EffectData effectData)
    {
        _effectData = effectData;
        _root = new GameObject($"[{EFFECT}]");
    }

    public void CreateWorld(EffectManager effect, Transform root)
    {
        var position = new Vector3(root.position.x, _yPosition, root.position.z);
        var parent = _root.transform;
        Create(effect, root, position, parent);
    }

    public void CreateLocal(EffectManager effect, Transform root)
    {
        var position = root.position;
        var parent = root;
        Create(effect, root, position, parent);
    }

    private void Create(EffectManager effect, Transform root, Vector3 position, Transform parent)
    {
        GameObject prefab = _effectData.Effects.GetEffectPrefab(effect);
        if (prefab == null)
        {
            Debug.Log($"{effect} is missing in {nameof(EffectData)}.");
            return;
        }

        GameObject effectGameObject = Object.Instantiate(prefab);
        effectGameObject.transform.position = position;
        effectGameObject.transform.rotation = root.rotation;
        effectGameObject.transform.SetParent(parent);
        float lifeTime = _effectData.Effects.GetEffectTime(effect);
        Object.Destroy(effectGameObject, lifeTime);
    }
}