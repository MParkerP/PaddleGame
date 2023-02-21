using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    private Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Text resolutionText;
    public Toggle fullscreenToggle;

    private void Start()
    {

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            resolutions = Screen.resolutions;

            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();

            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }

            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;

            resolutionText.text = resolutions[currentResolutionIndex].width + " x " + resolutions[currentResolutionIndex].height; 
        }

        fullscreenToggle.isOn = Screen.fullScreen;
    }
    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume * 1.25f);
    }

    public void SetEffectsVolume(float volume)
    {
        audioMixer.SetFloat("effectsVolume", volume * 1.25f);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        resolutionText.text = resolutions[resolutionIndex].width + " x " + resolutions[resolutionIndex].height;

    }



}
