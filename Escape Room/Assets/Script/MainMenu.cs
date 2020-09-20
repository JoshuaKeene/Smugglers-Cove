using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject ActivateGameStartDirector;
    public GameObject ActivateToOptionsDirector;
    public GameObject ActivateFromOptionsDirector;

    public AudioSource CanvasAudioSource;

    public bool InMenu;

    private void Start()
    {
        InMenu = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) { return; }
    }

    public void StartBtnClick()
    {
        StartCoroutine(AudioManager.GlobalSFXManager.FadeOut(CanvasAudioSource, 3));
        //AudioManager.GlobalSFXManager.gameObject.GetComponent<AudioSource>().enabled = true;
        Debug.Log("START");
        ActivateGameStartDirector.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        InMenu = false;
    }

    public void OptionsBtnClick()
    {
        Debug.Log("OPTIONS");
        ActivateToOptionsDirector.SetActive(true);
        Invoke("DeactivateBackDirector", 1);
    }

    public void BackBtnClick()
    {
        if (!UIManager.TheUI.MainCanvas.GetComponent<MainMenu>().InMenu) return;
        
        Debug.Log("BACK");
        ActivateFromOptionsDirector.SetActive(true);
    }

    public void QuitBtnClick()
    {
        Application.Quit();
        Debug.Log("QUIT");
    }

    public void TestBtnClick()
    {
        Debug.Log("BUTTON_PRESSED");
    }

    public void DeactivateBackDirector()
    {
        ActivateFromOptionsDirector.SetActive(false);
    }
}

