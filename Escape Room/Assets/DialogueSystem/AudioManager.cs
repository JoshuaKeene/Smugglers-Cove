using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager GlobalSFXManager;
    [Header("AudioEmitter")]
    public GameObject LeSFXPrefab;
    [Header("AudioClips")]
    #region AudioClips
    public AudioClip MainTheme;
    public AudioClip AmbientMusic;
    public AudioClip ChestOpen;
    public AudioClip LockSuccess;
    public AudioClip LockFail;
    public AudioClip PaperOpen;
    public AudioClip PaperClose;
    public AudioClip ItemSound;
    public AudioClip SkullLockEyeClick;
    public AudioClip FlagFlap;
    public AudioClip Fuse;
    public AudioClip Fire;
    public AudioClip WoodDoor;
    public AudioClip StoneDoor;
    public AudioClip OnPressClick;
    public AudioClip OnHoverClick;
    public AudioClip InventoryMove;
    public AudioClip ChessMove;
    public AudioClip Spring;
    public AudioClip Impact;
    public AudioClip TooltipSFX;
    public AudioClip Drink;
    public AudioClip FootStep;
    public AudioClip LiftLower;
    public AudioClip RopeCut;
    public AudioClip DigSand;
    public AudioClip WaterFootstep_01;
    public AudioClip WaterFootstep_02;
    public AudioClip SandFootstep_01;
    public AudioClip SandFootstep_02;
    public AudioClip DefaultFootstep_01;
    public AudioClip DefaultFootstep_02;
    #endregion

    void Start()
    {
        GlobalSFXManager = this;
    }


    internal AudioSource PlaySFX(AudioClip leClip, Transform audioPosition) 
    {
        GameObject GO = null;

        if (audioPosition == null) { LeSFXPrefab.GetComponent<AudioSource>().spatialBlend = 0; GO = Instantiate(LeSFXPrefab); }
        else { LeSFXPrefab.GetComponent<AudioSource>().spatialBlend = 1; GO = Instantiate(LeSFXPrefab, audioPosition); }

        GO.GetComponent<AudioSource>().clip = leClip;
        GO.GetComponent<AudioSource>().Play();
        Destroy(GO, GO.GetComponent<AudioSource>().clip.length);
        return GO.GetComponent<AudioSource>();
    }

    public IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        float startVolume = 0.2f;

        audioSource.volume = 0;
        audioSource.Play();

        while (audioSource.volume < 1.0f)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.volume = 1f;
    }

    public void CreditsMusicStart()
    {
        StartCoroutine(AudioManager.GlobalSFXManager.FadeIn(UIManager.TheUI.MainCanvas.GetComponent<AudioSource>(), 2));
    }

    public void CreditsMusicEnd()
    {
        StartCoroutine(AudioManager.GlobalSFXManager.FadeOut(UIManager.TheUI.MainCanvas.GetComponent<AudioSource>(), 4));
    }
}
