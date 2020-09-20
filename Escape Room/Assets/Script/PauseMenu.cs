using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (UIManager.TheUI.PauseMenu.activeInHierarchy || UIManager.TheUI.OptionsMenu.activeInHierarchy)
            {
                if (UIManager.TheUI.PauseMenu.activeInHierarchy)
                {
                    ClosePauseMenu();
                    UIManager.TheUI.Background.GetComponent<Animator>().Play(AnimationManager.Background_FadeOut);
                    StartCoroutine("WaitForBackground");
                }
                else if (UIManager.TheUI.OptionsMenu.activeInHierarchy)
                {
                    OnClickBack();
                }
            }
            else if (!UIManager.TheUI.PauseMenu.activeInHierarchy)
            {
                //Checks with UI manager if any UIs active
                if (UIManager.TheUI.AreAnyUIsActive()) return;

                OpenPauseMenu();
                UIManager.TheUI.Background.GetComponent<Animator>().Play(AnimationManager.Background_FadeIn);
            }
        }
    }

    public void OnClickResume()
    {
        ClosePauseMenu();
        UIManager.TheUI.Background.GetComponent<Animator>().Play(AnimationManager.Background_FadeOut);
        StartCoroutine("WaitForBackground");
    }

    public void OnClickOptions()
    {
        UIManager.TheUI.OptionsMenu.SetActive(true);

        UIManager.TheUI.OptionsMenu.GetComponent<Animator>().Play(AnimationManager.OptionsMenu_Open);
        UIManager.TheUI.PauseMenu.GetComponent<Animator>().Play(AnimationManager.PauseMenu_Close);
        StartCoroutine("WaitForPauseCloseForOptions");
    }

    public void OnClickBack()
    {
        if (UIManager.TheUI.MainCanvas.GetComponent<MainMenu>().InMenu) return;

        UIManager.TheUI.OptionsMenu.GetComponent<Animator>().Play(AnimationManager.OptionsMenu_Close);
        StartCoroutine("WaitForOptionsCloseAnim");
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    private void OpenPauseMenu()
    {
        UIManager.TheUI.PauseMenu.SetActive(true);

        UIManager.TheUI.PlayerHUD.SetActive(false);
        UIManager.TheUI.InputLockState(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        UIManager.TheUI.PauseMenu.GetComponent<Animator>().Play(AnimationManager.PauseMenu_Open);
    }

    private void ClosePauseMenu()
    {
        UIManager.TheUI.PauseMenu.GetComponent<Animator>().Play(AnimationManager.PauseMenu_Close);
        StartCoroutine("WaitForPauseCloseAnim");
    }

    private IEnumerator WaitForPauseCloseAnim()
    {
        yield return new WaitForSeconds(0.2f);
        UIManager.TheUI.PauseMenu.GetComponent<Animator>().Play(AnimationManager.Nothing);

        UIManager.TheUI.PauseMenu.SetActive(false);

        UIManager.TheUI.PlayerHUD.SetActive(true);
        UIManager.TheUI.InputLockState(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private IEnumerator WaitForOptionsCloseAnim()
    {
        yield return new WaitForSeconds(0.4f);
        UIManager.TheUI.OptionsMenu.GetComponent<Animator>().Play(AnimationManager.Nothing);

        UIManager.TheUI.OptionsMenu.SetActive(false);
        UIManager.TheUI.PauseMenu.SetActive(true);
        UIManager.TheUI.OptionsMenu.GetComponent<Image>().enabled = false;

        UIManager.TheUI.PauseMenu.GetComponent<Animator>().Play(AnimationManager.PauseMenu_Open);
    }

    private IEnumerator WaitForPauseCloseForOptions()
    {
        yield return new WaitForSeconds(0.3f);
        UIManager.TheUI.PauseMenu.GetComponent<Animator>().Play(AnimationManager.Nothing);
        UIManager.TheUI.PauseMenu.SetActive(false);
    }

    private IEnumerator WaitForBackground()
    {
        yield return new WaitForSeconds(0.3f);
        UIManager.TheUI.Background.GetComponent<Animator>().Play(AnimationManager.Nothing);
    }
}

