using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeLiftAudio : MonoBehaviour
{
    public void FadeAudio()
    {
        var LiftAudio = gameObject.transform.Find("Lift").gameObject.GetComponent<AudioSource>();

        StartCoroutine(AudioManager.GlobalSFXManager.FadeOut(LiftAudio, 1));
    }
}
