using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [Space]
    public bool softShadows = true;
    public bool postProcessing = true;
    public bool waterReflections = true;
    [Space]
    public GameObject water;
    [Space]
    public Light[] pointLights;

    private void Start()
    {
        QualitySettings.shadows = ShadowQuality.All;
    }

    public void SetMasterVolume(float amount)
    {
        audioMixer.SetFloat("MasterVolume", amount);
    }

    public void SetMusicVolume(float amount)
    {
        audioMixer.SetFloat("MusicVolume", amount);
    }

    public void SetSFXVolume(float amount)
    {
        audioMixer.SetFloat("SFXVolume", amount);
    }

    public void ToggleShadows()
    {
        if (softShadows)
        {
            //Turn them off
            //QualitySettings.shadows = ShadowQuality.Disable;
            foreach (Light light in pointLights)
            {
                light.shadows = LightShadows.None;
            }
            softShadows = false;
        }
        else if (!softShadows)
        {
            //Turn them on
            //QualitySettings.shadows = ShadowQuality.All;
            foreach (Light light in pointLights)
            {
                light.shadows = LightShadows.Soft;
            }
            softShadows = true;
        }
    }

    public void TogglePostProcessing()
    {
        if (postProcessing)
        {
            //Turn it off
            PostProcessManager.TheManager.gameObject.GetComponent<PostProcessVolume>().enabled = false;
            postProcessing = false;
        }
        else if (!postProcessing)
        {
            //Turn it on
            PostProcessManager.TheManager.gameObject.GetComponent<PostProcessVolume>().enabled = true;
            postProcessing = true;
        }
    }

    public void ToggleWaterReflections()
    {
        if (waterReflections)
        {
            //Turn it off
            //waterWithReflections.SetActive(false);
            //waterWithoutReflections.SetActive(true);

            water.GetComponent<Renderer>().material.SetFloat("_RealtimeReflections", 0);

            waterReflections = false;
        }
        else if (!waterReflections)
        {
            //Turn it on
            //waterWithoutReflections.SetActive(false);
            //waterWithReflections.SetActive(true);

            water.GetComponent<Renderer>().material.SetFloat("_RealtimeReflections", 1);

            waterReflections = true;
        }
    }
}
