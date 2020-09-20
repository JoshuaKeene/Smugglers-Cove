using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatueUIManager : MonoBehaviour
{
    public GameObject Statue;
    public GameObject RaycastBlocker;

    public GameObject OptionOne;
    public GameObject OptionTwo;
    public GameObject OptionThree;
    public GameObject OptionFour;
    private GameObject ContinueButton;
    private int ContinueID;

    private bool AllContinue = false;

    public Text SpeakerName;

    public Color ContinueTextColor;

    [HideInInspector]
    public bool Continue = false;

    public void Update()
    {
        if (Statue.GetComponent<DialogueScript>().IsTalking) { RaycastBlocker.SetActive(true); }
        else { RaycastBlocker.SetActive(false); }
    }

    public void SetDialogueOptions(string One, string Two, string Three, string Four, int Continue_Option, string Speaker)
    {
        if (One != null && OptionOne.GetComponent<Button>().interactable == false) { OptionOne.GetComponent<Button>().interactable = true; }
        OptionOne.GetComponentInChildren<Text>().text = One;
        if (One == null) { OptionOne.GetComponent<Button>().interactable = false; }

        if (Two != null && OptionTwo.GetComponent<Button>().interactable == false) { OptionTwo.GetComponent<Button>().interactable = true; }
        OptionTwo.GetComponentInChildren<Text>().text = Two;
        if (Two == null) { OptionTwo.GetComponent<Button>().interactable = false; }

        if (Three != null && OptionThree.GetComponent<Button>().interactable == false) { OptionThree.GetComponent<Button>().interactable = true; }
        OptionThree.GetComponentInChildren<Text>().text = Three;
        if (Three == null) { OptionThree.GetComponent<Button>().interactable = false; }

        if (Four != null && OptionFour.GetComponent<Button>().interactable == false) { OptionFour.GetComponent<Button>().interactable = true; }
        OptionFour.GetComponentInChildren<Text>().text = Four;
        if (Four == null) { OptionFour.GetComponent<Button>().interactable = false; }


        SpeakerName.text = Speaker;

        ContinueID = Continue_Option;

        SetContinueOption(ContinueID);
    }

    public void SetContinueOption(int ContinueOption)
    {
        if (ContinueOption == 1)
        {
            ContinueButton = OptionOne;
        }
        else if (ContinueOption == 2)
        {
            ContinueButton = OptionTwo;
        }
        else if (ContinueOption == 3)
        {
            ContinueButton = OptionThree;
        }
        else if (ContinueOption == 4)
        {
            ContinueButton = OptionFour;
        }
        else if (ContinueOption == 5)
        {
            AllContinue = true;
            ContinueButton = null;
        }
        else 
        {
            ContinueButton = null;
        }

        OptionOne.GetComponentInChildren<Text>().color = Color.white;
        OptionTwo.GetComponentInChildren<Text>().color = Color.white;
        OptionThree.GetComponentInChildren<Text>().color = Color.white;
        OptionFour.GetComponentInChildren<Text>().color = Color.white;

        if (ContinueButton != null) { ContinueButton.GetComponentInChildren<Text>().color = ContinueTextColor; }
    }

    public void BtnDisableOverride(bool Disable)
    {
        if (!Disable)
        {
            OptionOne.GetComponent<Button>().interactable = true;
            OptionTwo.GetComponent<Button>().interactable = true;
            OptionThree.GetComponent<Button>().interactable = true;
            OptionFour.GetComponent<Button>().interactable = true;
        }
        else
        {
            OptionOne.GetComponent<Button>().interactable = false;
            OptionTwo.GetComponent<Button>().interactable = false;
            OptionThree.GetComponent<Button>().interactable = false;
            OptionFour.GetComponent<Button>().interactable = false;
        }
    }

    #region ButtonFunctions

    public void OnBtnClick_OpOne()
    {
        Statue.GetComponent<Interactive_Statue>().ButtonPress(1);
        if (ContinueID == 1 || AllContinue) { Continue = true; }
        if (AllContinue) { AllContinue = false; Statue.GetComponent<Interactive_Statue>().AllContinueBtnOne = true; }
    }

    public void OnBtnClick_OpTwo()
    {
        Statue.GetComponent<Interactive_Statue>().ButtonPress(2);
        if (ContinueID == 2 || AllContinue) { Continue = true; }
        if (AllContinue) { AllContinue = false; Statue.GetComponent<Interactive_Statue>().AllContinueBtnTwo = true; }
        if (Statue.GetComponent<Interactive_Statue>().hasIdol) 
        { 
            Statue.GetComponent<Interactive_Statue>().hasGivenIdol = true;
            Statue.transform.Find("Gold Idol").gameObject.SetActive(true);
            InventoryManager.TheInventory.RemoveItem("Golden Idol");
        }
    }

    public void OnBtnClick_OpThree()
    {
        Statue.GetComponent<Interactive_Statue>().ButtonPress(3);
        if (ContinueID == 3 || AllContinue) { Continue = true; }
        if (AllContinue) { AllContinue = false; Statue.GetComponent<Interactive_Statue>().AllContinueBtnThree = true; }
    }

    public void OnBtnClick_OpFour()
    {
        Statue.GetComponent<Interactive_Statue>().ButtonPress(4);
        if (ContinueID == 4 || AllContinue) { Continue = true; }
        if (AllContinue) { AllContinue = false; Statue.GetComponent<Interactive_Statue>().AllContinueBtnFour = true; }
        if (Statue.GetComponent<Interactive_Statue>().hasIdol)
        {
            Statue.GetComponent<Interactive_Statue>().hasGivenIdol = true;
            Statue.transform.Find("Gold Idol").gameObject.SetActive(true);
            InventoryManager.TheInventory.RemoveItem("Golden Idol");
        }
    }

    #endregion
}
