using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/AudioData", order = 1)]
public class AudioData : ScriptableObject
{
    [SerializeField] private Sound[] _sounds;

    public Sound[] Sounds => _sounds;
}
