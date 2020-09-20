using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessManager : MonoBehaviour
{
    public static PostProcessManager TheManager;
    
    private PostProcessVolume PostProcessing;
    private DepthOfField DepthOfFieldLayer;
    private ChromaticAberration ChromaticAberrationLayer;

    private float Default_DofFocalDis = 10f;
    private float Default_DofAperture = 0.15f;
    private float Default_DofFocalLength = 15f;

    private float Low_DofFocalDis = 0.9f;
    private float Low_DofAperture = 0.4f;
    private float Low_DofFocalLength = 20f;

    private float Menu_DofFocalDis = 0.1f;
    private float Menu_DofAperture = 2.3f;
    private float Menu_DofFocalLength = 6f;

    // Update is called once per frame
    private void Start()
    {
        TheManager = this;
        
        PostProcessing = gameObject.GetComponent<PostProcessVolume>();
        DepthOfFieldLayer = PostProcessing.profile.GetSetting<DepthOfField>();
        ChromaticAberrationLayer = PostProcessing.profile.GetSetting<ChromaticAberration>();
        ChromaticAberrationLayer.enabled.value = false;
    }

    public void ChromaticAberration_Drunk(bool Drunk)
    {
        if (Drunk) { ChromaticAberrationLayer.enabled.value = true; }
        else { ChromaticAberrationLayer.enabled.value = false; }
    }

    #region Depth of Field Utilities

    public void DisableDoF()
    {
        DepthOfFieldLayer.enabled.value = false;
    }

    public void EnableDoF()
    {
        DepthOfFieldLayer.enabled.value = true;
    }

    public void SetFocalDistanceDefault()
    {
        DepthOfFieldLayer.focusDistance.value = Default_DofFocalDis;
        DepthOfFieldLayer.aperture.value = Default_DofAperture;
        DepthOfFieldLayer.focalLength.value = Default_DofFocalLength;
    }

    public void SetFocalDistanceMenu()
    {
        DepthOfFieldLayer.focusDistance.value = Menu_DofFocalDis;
        DepthOfFieldLayer.aperture.value = Menu_DofAperture;
        DepthOfFieldLayer.focalLength.value = Menu_DofFocalLength;
    }

    public void SetFocalDistanceChessPuzzle()
    {
        DepthOfFieldLayer.focusDistance.value = 1f;
        DepthOfFieldLayer.aperture.value = 0.6f;
        DepthOfFieldLayer.focalLength.value = 15f;
    }

    public void SetFocalDistanceLow()
    {
        DepthOfFieldLayer.focusDistance.value = Low_DofFocalDis;
        DepthOfFieldLayer.aperture.value = Low_DofAperture;
        DepthOfFieldLayer.focalLength.value = Low_DofFocalLength;
    }

    public void SetFocalDistanceMedium()
    {
        DepthOfFieldLayer.focusDistance.value = 1f;
        DepthOfFieldLayer.aperture.value = 1f;
        DepthOfFieldLayer.focalLength.value = 1f;
    }

    public void SetFocalDistanceHigh()
    {
        DepthOfFieldLayer.focusDistance.value = 10f;
        DepthOfFieldLayer.aperture.value = 0.15f;
        DepthOfFieldLayer.focalLength.value = 1f;
    }

    #endregion
}
