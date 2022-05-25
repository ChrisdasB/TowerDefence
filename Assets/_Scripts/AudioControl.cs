using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    [SerializeField] string volumeParameter;
    [SerializeField] AudioMixer mainAudioMixer;
    [SerializeField] Slider slider;
    [SerializeField] string volumeToggle;
    [SerializeField] Toggle toggle; 
    


    private float multiplier = 40;

    private void Awake()
    {
        slider.onValueChanged.AddListener(UpdateAudioValue);
        toggle.onValueChanged.AddListener(ToggleMusic);                 
    }

    

    private void ToggleMusic(bool isPlaying)
    {
        if (isPlaying)
            mainAudioMixer.SetFloat(volumeParameter, Mathf.Log10(slider.value) * multiplier);
        else
            mainAudioMixer.SetFloat(volumeParameter, Mathf.Log10(slider.minValue) * multiplier);

        SaveBool(isPlaying);
    }   

    void UpdateAudioValue(float value)
    {
        mainAudioMixer.SetFloat(volumeParameter, Mathf.Log10(value) * multiplier);
        if (Mathf.Log10(value) * multiplier > Mathf.Log10(slider.minValue) * multiplier)
            toggle.isOn = true;
        if (Mathf.Log10(value) * multiplier <= -80f)
            toggle.isOn = false;

        print("Volume has been set to " + Mathf.Log10(value) * multiplier);

        SaveFloat(volumeParameter,value);
    }

    public void SaveFloat(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
    }

    void SaveBool(bool isActive)
    {
        int dataToSave;

        if (isActive)
            dataToSave = 1;
        else
            dataToSave = 0;
        PlayerPrefs.SetInt(volumeToggle, dataToSave);
    }

    public void LoadFloat()
    {
        slider.value = PlayerPrefs.GetFloat(volumeParameter);        
    }

    public void LoadBool()
    {
        bool myBool;
        if (PlayerPrefs.GetInt(volumeToggle) == 0)
            myBool = false;
        else
            myBool = true;

        toggle.isOn = myBool;
    }

}
