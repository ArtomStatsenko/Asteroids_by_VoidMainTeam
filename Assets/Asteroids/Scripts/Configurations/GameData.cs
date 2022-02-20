using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public sealed class GameData : ScriptableObject
{
    [SerializeField] private GameModeManager _gameMode;
    [SerializeField] private LevelData _levelData;
    [SerializeField] private AudioData _audioData;
    [SerializeField] private EffectData _effectData;

    private bool _isFullscreen;
    private int _resolutionIndex;
    private int _qualityLevel;
    private float _volume = 1f;

    public GameModeManager GameMode
    {
        get => _gameMode;
        set => _gameMode = value;
    }
    public LevelData LevelData => _levelData;
    public AudioData AudioData => _audioData;
    public EffectData EffectData => _effectData;
    public bool IsFullscreen
    {
        get => _isFullscreen;
        set
            => _isFullscreen = value;
    }
    public int ResolutionIndex
    {
        get => _resolutionIndex;
        set => _resolutionIndex = value;
    }
    public int QualityLevel
    {
        get => _qualityLevel;
        set => _qualityLevel = value;
    }
    public float Volume 
    { 
        get => _volume; 
        set => _volume = value; 
    }
}
