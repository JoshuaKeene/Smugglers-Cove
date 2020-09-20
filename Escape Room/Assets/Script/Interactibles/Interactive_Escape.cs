using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactive_Escape : InteractiveObject
{
    public GameObject CreditsDirector;

    public override void ExecuteInteractiveAction()
    {
        gameObject.tag = "Untagged";
        CreditsDirector.SetActive(true);
        StartCoroutine(AudioManager.GlobalSFXManager.FadeOut(AudioManager.GlobalSFXManager.gameObject.GetComponent<AudioSource>(), 3));
    }
}