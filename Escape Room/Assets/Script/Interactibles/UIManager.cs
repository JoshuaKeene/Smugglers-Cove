using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Manager")]
    public static UIManager TheUI;
    public FirstPersonController TheCharInputs;
    public GameObject FPSController;
    public Camera FPSCamera;
    public GameObject MainCanvas;
    public GameObject MainCamera;
    public GameObject OpeningCutscene;
    public GameObject ExitHintText;
    [Header("Associated UI")]
    public GameObject SkullLock;
    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject StatueUI;
    public GameObject ChessUI;
    public GameObject PauseMenu;
    public GameObject PlayerHUD;
    public GameObject CurItemFrame;
    public GameObject Background;
    public GameObject SubText;
    [Space]
    public GameObject Tooltip;

    private bool InProgress = false;


    // Start is called before the first frame update
    void Start()
    {
        TheUI = this;
    }

    public bool AreAnyUIsActive()
    {
        //print("Are any UIs active?");
        if (DocumentManager.TheManager.DocPannel.activeInHierarchy) return true;
        if (InventoryManager.TheInventory.InventoryPannel.activeInHierarchy) return true;
        if (OpeningCutscene.activeInHierarchy) return true;
        if (SkullLock.activeInHierarchy) return true;
        if (MainMenu.activeInHierarchy) return true;
        if (OptionsMenu.activeInHierarchy) return true;
        if (StatueUI.activeInHierarchy) return true;
        if (ChessUI.activeInHierarchy) return true;
        if (PauseMenu.activeInHierarchy) return true;

        return false;
    }

    internal void InputLockState(bool LockState) //true = unlocked
    {
        TheCharInputs.enabled = LockState;
        print(TheCharInputs.enabled);
    }

    //UI Tooltip
    internal void TooltipMessage(string message, float displayTime)
    {
        if (!InProgress)
        {
            InProgress = true;

            Tooltip.GetComponentInChildren<Text>().text = message;
            var storedDuration = displayTime;

            Tooltip.SetActive(true);
            Tooltip.GetComponent<Animator>().Play(AnimationManager.Tooltip_FadeIn, -1, 0f);
            AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.TooltipSFX, null);

            StartCoroutine("DisplayDuration", storedDuration);
        }
    }

    public IEnumerator DisplayDuration(float Duration)
    {
        yield return new WaitForSeconds(Duration);
        Tooltip.GetComponent<Animator>().Play(AnimationManager.Tooltip_FadeOut, -1, 0f);
        yield return new WaitForSeconds(0.5f);
        Tooltip.SetActive(false);

        InProgress = false;
    }

    public void ExitHint(string btnToExit, bool active)
    {
        ExitHintText.GetComponent<Text>().text = "'" + btnToExit + "' to exit.";
        ExitHintText.SetActive(active);
    }
 
}
