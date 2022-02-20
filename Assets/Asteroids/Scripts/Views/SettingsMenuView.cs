using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System;
using OptionData = TMPro.TMP_Dropdown.OptionData;

public sealed class SettingsMenuView : MonoBehaviour
{
    public event Action<float> OnVolumeSliderValueChangedEvent;
    public event Action<int> OnGraphicsDropdownValueChangedEvent;
    public event Action<bool> OnFullscreenToggleValueChangedEvent;
    public event Action<int> OnResolutionDropdownValueChangedEvent;

    [SerializeField] private Button _backButton;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private Slider _volumeSlider;  
    [SerializeField] private TMP_Dropdown _graphicsDropdown;
    [SerializeField] private Toggle _fullscreenToggle;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    private void Start()
    {
        _backButton.onClick.AddListener(OpenMainMenu);
        _volumeSlider.onValueChanged.AddListener(SetVolume);
        _graphicsDropdown.onValueChanged.AddListener(SetQuality);
        _fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        _resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    private void OpenMainMenu()
    {
        if (!_mainMenuPanel.activeSelf)
        {
            _mainMenuPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void SetVolume(float volume)
    {
        OnVolumeSliderValueChangedEvent?.Invoke(volume);
    }

    private void SetQuality(int qualityLevelIndex)
    {
        OnGraphicsDropdownValueChangedEvent?.Invoke(qualityLevelIndex);
    }

    private void SetFullscreen(bool isFullscreen)
    {
        OnFullscreenToggleValueChangedEvent?.Invoke(isFullscreen);
    }

    private void SetResolution(int resolutionIndex)
    {
        OnResolutionDropdownValueChangedEvent?.Invoke(resolutionIndex);
    }

    public void AddResolutionsToDropdown(List<OptionData> resolutions, int defaultResolutionIndex)
    {
        _resolutionDropdown.ClearOptions();
        _resolutionDropdown.AddOptions(resolutions);
        _resolutionDropdown.value = defaultResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    public void SetVolumeSliderValue(float value)
    {
        _volumeSlider.value = value;
    }

    public void UpdateFullscreenToggle(bool isFullscreen)
    {
        _fullscreenToggle.isOn = isFullscreen;
    }

    public void UpdateQualityDropdown(int qualityLevel)
    {
        _graphicsDropdown.value = qualityLevel;
    }
}
