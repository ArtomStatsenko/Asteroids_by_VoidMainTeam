using UnityEngine;

public static class Extentions
{
    public static AudioClip GetAudioClip(this Sound[] sound, AudioClipManager action)
    {
        foreach (var currentSound in sound)
        {
            if (currentSound.Action == action)
            {
                return currentSound.Audio;
            }
        }

        return default;
    }

    public static GameObject GetEffectPrefab(this Effect[] effect, EffectManager action)
    {
        foreach (var currentEffect in effect)
        {
            if (currentEffect.Action == action)
            {
                return currentEffect.Prefab;
            }
        }

        return default;
    }

    public static float GetEffectTime(this Effect[] effect, EffectManager action)
    {
        foreach (var currentEffect in effect)
        {
            if (currentEffect.Action == action)
            {
                return currentEffect.LifeTime;
            }
        }

        return default;
    }
}