using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningCutScene : MonoBehaviour
{
    internal bool OpeningCutsceneRunning = false;
    
    public void Footstep()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(AudioManager.GlobalSFXManager.FootStep);
    }

    public void CamAnimation()
    {
        gameObject.GetComponent<Animator>().Play(AnimationManager.OpeningCS_Cam);
    }

    public void IntroCutSceneStart()
    {
        OpeningCutsceneRunning = true;
    }

    public void IntroCutSceneEnd()
    {
        OpeningCutsceneRunning = false;
    }
}
