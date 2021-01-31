using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class soundVolume : MonoBehaviour
{

    public AudioSource mixer;
    public Slider slider;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("AudioSource", 0.75f);
    }

    public void SetLevel(float sliderValue)
    {
        
      //  mixer.SetFloat("<AudioSource>", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("<AudioSource>", sliderValue);
        mixer.volume = sliderValue;
    }


}