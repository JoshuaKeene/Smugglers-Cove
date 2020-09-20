using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentManager : MonoBehaviour
{
    public GameObject DocPannel;

    public GameObject OverplayTextPannel;
    public Image DocOnScreen;
    public Text DocOnScreenText;
    public Text DocTextNoOverlay;
    private string CachedDocText;

    public static DocumentManager TheManager;

    public enum TextState
    {
        Appering,
        Hiding,
        Opaque,
        Transparent
    }
    public TextState CurrentTextState;
    private float AlphaColor = 0;
    public float AlphaColorRate;
    public float TextBackroundAlphaOffest;

    // Start is called before the first frame update
    void Start()
    {
        TheManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentTextState)
        {
            case TextState.Appering:
                AlphaColor += AlphaColorRate;

                if(AlphaColor>=1)
                {
                    AlphaColor = 1;
                    CurrentTextState = TextState.Opaque;
                }

                DocOnScreenText.color = new Color(1, 1, 1, AlphaColor);
                OverplayTextPannel.GetComponent<Image>().color = new Color(0, 0, 0, AlphaColor - TextBackroundAlphaOffest);
                break;
            case TextState.Hiding:
                AlphaColor -= AlphaColorRate;

                if (AlphaColor <= 0)
                {
                    AlphaColor = 0;
                    CurrentTextState = TextState.Transparent;
                }

                DocOnScreenText.color = new Color(1, 1, 1, AlphaColor);
                OverplayTextPannel.GetComponent<Image>().color = new Color(0, 0, 0, AlphaColor - TextBackroundAlphaOffest);
                break;
            case TextState.Opaque:
                break;
            case TextState.Transparent:
                break;
            default:
                break;
        }

        //if(DocPannel.activeInHierarchy)
        //{
        //    if(Input.GetKeyDown(KeyCode.E))
        //    {
        //        if(CurrentTextState == TextState.Opaque)
        //        {
        //            HideText();
        //        }
        //        else if (CurrentTextState == TextState.Transparent)
        //        {
        //            ShowText();
        //        }
        //    }
        //}
        
        if(Input.GetKeyDown(KeyCode.Q) && DocPannel.activeInHierarchy)
        {
            CloseDocumentPannel();
        }
    }

    #region Document Utilites

    private void CloseDocumentPannel()
    {
        DocPannel.GetComponent<Animator>().Play(AnimationManager.Document_Close, -1, 0f);
        AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.PaperClose, null);
        UIManager.TheUI.ExitHint("Q", false);
        StartCoroutine("WaitForNoteAnim");
    }

    public void ShowText()
    {
        CurrentTextState = TextState.Appering;
    }

    private void HideText(bool DisableImmediately = false)
    {
        if(DisableImmediately)
        {
            CurrentTextState = TextState.Transparent;
            AlphaColor = 0;
            DocOnScreenText.color = new Color(1, 1, 1, 0);
            OverplayTextPannel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
        else
        {
            CurrentTextState = TextState.Hiding;
        }
    }

    internal void OpenDocumentPannel(Sprite Img, string DocName, string DocText)
    {
        DocPannel.SetActive(true);
        DocOnScreen.sprite = Img;
        CachedDocText = "<size=50>" + DocName + "</size>\n" + DocText;
        DocOnScreenText.text = CachedDocText;
        DocTextNoOverlay.text = CachedDocText;

        //Lock The Inputs
        UIManager.TheUI.InputLockState(false);
        UIManager.TheUI.PlayerHUD.SetActive(false);
        UIManager.TheUI.ExitHint("Q", true);

        DocOnScreenText.color = new Color(1, 1, 1, 0);
        OverplayTextPannel.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        CurrentTextState = TextState.Transparent;
    }

    internal void OpenDocumentPannel(DocumentScript DS)
    {
        OpenDocumentPannel(DS.DocSprite, DS.DocName, DS.DocText);
        DocPannel.GetComponent<Animator>().Play(AnimationManager.Document_Open, -1, 0f);
        AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.PaperOpen, null);
    }

    #endregion

    public IEnumerator WaitForNoteAnim()
    {
        yield return new WaitForSeconds(0.5f);
        HideText(true);
        DocPannel.SetActive(false);
        //Unlock the inputs
        UIManager.TheUI.InputLockState(true);
        UIManager.TheUI.PlayerHUD.SetActive(true);
    }
}
