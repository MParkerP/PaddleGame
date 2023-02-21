using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetSlider : MonoBehaviour
{
    public AudioMixer mainMixer;
    private Slider slider;

    private float effectsVolume;
    private float musicVolume;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        mainMixer.GetFloat("effectsVolume", out effectsVolume);
        mainMixer.GetFloat("musicVolume", out musicVolume);

        if (this.CompareTag("Music"))
        {
            slider.value = musicVolume;
        }
        else if (this.CompareTag("Effects"))
        {
            slider.value = effectsVolume;
        }
        
    }

}
