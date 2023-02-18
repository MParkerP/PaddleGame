using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixerGroup audioMixerGroup;

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume * 1.25f);
    }

    public void SetEffectsVolume(float volume)
    {
        audioMixer.SetFloat("effectsVolume", volume * 1.25f);
    }

}
