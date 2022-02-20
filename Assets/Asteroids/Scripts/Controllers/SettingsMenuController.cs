using UnityEngine;
using System.Collections.Generic;
using OptionData = TMPro.TMP_Dropdown.OptionData;
using System;

public sealed class SettingsMenuController
{
    public event Action<float> OnVolumeChandedEvent;

    private Resolution[] _resolutions;
    private SettingsMenuView _view;
    private GameData _gameData;

    public SettingsMenuController(SettingsMenuView view, GameData gameData)
    {
        _view = view;
        _gameData = gameData;
        InitVolumeSlider();
        InitResolutionsToggle();
        _view.UpdateFullscreenToggle(_gameData.IsFullscreen);
        _view.UpdateQualityDropdown(_gameData.QualityLevel);
    }

    private void InitVolumeSlider()
    {
        var volume = _gameData.Volume;
        _view.SetVolumeSliderValue(volume);
    }

    public void OnEnable()
    {
        _view.OnVolumeSliderValueChangedEvent += SetVolume;
        _view.OnGraphicsDropdownValueChangedEvent += SetQuality;
        _view.OnFullscreenToggleValueChangedEvent += SetFullscreen;
        _view.OnResolutionDropdownValueChangedEvent += SetResolution;
    }

    public void OnDisable()
    {
        _view.OnVolumeSliderValueChangedEvent -= SetVolume;
        _view.OnGraphicsDropdownValueChangedEvent -= SetQuality;
        _view.OnFullscreenToggleValueChangedEvent -= SetFullscreen;
        _view.OnResolutionDropdownValueChangedEvent -= SetResolution;
    }

    private void SetVolume(float volume)
    {
        _gameData.Volume = volume;
        OnVolumeChandedEvent?.Invoke(volume);
    }

    private void SetQuality(int qualityLevelIndex)
    {
        _gameData.QualityLevel = qualityLevelIndex;
        QualitySettings.SetQualityLevel(qualityLevelIndex);
    }

    private void SetFullscreen(bool isFullscreen)
    {
        _gameData.IsFullscreen = isFullscreen;
#if UNITY_EDITOR
        var editorWindow = UnityEditor.EditorWindow.focusedWindow;
        editorWindow.maximized = isFullscreen;
#else
        Screen.fullScreen = isFullscreen;
#endif
    }

    private void SetResolution(int resolutionIndex)
    {
        _gameData.ResolutionIndex = resolutionIndex;
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    private void InitResolutionsToggle()
    {
        _resolutions = Screen.resolutions;

        List<OptionData> resolutions = new List<OptionData>();
        for (int i = 0; i < _resolutions.Length; i++)
        {
            Resolution resolution = _resolutions[i];
            string resolutionText = $"{resolution.width} x {resolution.height}";
            OptionData optionData = new OptionData(resolutionText);
            resolutions.Add(optionData);
        }

        _view.AddResolutionsToDropdown(resolutions, _gameData.ResolutionIndex);
    }
}
