using UnityEngine;
using System.Collections.Generic;

public sealed class AudioController
{
    private const string SOUND = "Sound";
    private AudioData _audioData;
    private GameObject _root;
    private List<AudioSource> _soundSource;
    private float _volume;

    public AudioController(AudioData data, float volume)
    {
        _audioData = data;
        _root = new GameObject($"[{SOUND}]");
        _soundSource = new List<AudioSource>();
        _volume = volume;
    }

    public void Play(AudioClipManager clipType, bool isLoop = false)
    {
        AudioClip clip = _audioData.Sounds.GetAudioClip(clipType);
        if (clip == null)
        {
            Debug.Log($"{clipType} is missing in {nameof(AudioData)}.");
            return;
        }

        Play(clip, isLoop);
    }

    private void Play(AudioClip clip, bool isLoop)
    {
        GameObject soundGameObject = new GameObject(SOUND);
        soundGameObject.transform.SetParent(_root.transform);
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        _soundSource.Add(audioSource);

        audioSource.clip = clip;
        audioSource.loop = isLoop;
        audioSource.volume = _volume;
        audioSource.Play();

        if (!isLoop)
        {
            Object.Destroy(audioSource.gameObject, clip.length);
            _soundSource.Remove(audioSource);
        }
    }

    public void Clear()
    {        
        foreach (var source in _soundSource)
        {
            if (source != null)
            {
                Object.Destroy(source?.gameObject);
            }
        }
    }

    public void SetVolume(float volume)
    {
        foreach (var source in _soundSource)
        {
            source.volume = volume;
        }
    }
}