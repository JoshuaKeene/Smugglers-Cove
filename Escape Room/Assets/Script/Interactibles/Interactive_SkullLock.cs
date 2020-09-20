using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Interactive_SkullLock : InteractiveObject
{
    //Associated UI
    private GameObject SkullLockPannel;
    private GameObject InteractablesUIPannel;

    //Animator In Parent
    private Animator ChestOpenAnim;

    //Associated Camera
    internal GameObject ChestCam;

    //Player
    private GameObject FPSController;

    //Skulls Grouped
    internal List<GameObject> LeftSkull = new List<GameObject>();
    internal List<GameObject> RightSkull = new List<GameObject>();
    
    //Combination Gems
    private GameObject RedGem01;
    private GameObject RedGem02;

    private GameObject GreenGem01;
    private GameObject GreenGem02;

    private GameObject WhiteGem01;
    private GameObject WhiteGem02;

    private GameObject RedGem03;
    private GameObject RedGem04;

    private GameObject GreenGem03;
    private GameObject GreenGem04;

    private GameObject WhiteGem03;
    private GameObject WhiteGem04;

    private GameObject RedGem05;
    private GameObject RedGem06;

    private GameObject GreenGem05;
    private GameObject GreenGem06;

    private GameObject WhiteGem05;
    private GameObject WhiteGem06;

    //Chest Type
    public enum ChestTypeEnum { Standard, Advanced }

    //Set Chest Type - Exposed to inspector
    [Header("Chest Type")]
    public ChestTypeEnum Type;

    //Lock Combination
    public enum CombinationOne { Red, Green, White }
    public enum CombinationTwo { Red, Green, White }
    public enum CombinationThree { Red, Green, White }
    public enum CombinationFour { Red, Green, White }
    public enum CombinationFive { Red, Green, White }
    public enum CombinationSix { Red, Green, White }

    //Set Combination - Exposed to inspector
    [Header("Combination")]
    public CombinationOne One;
    public CombinationTwo Two;
    public CombinationThree Three;
    public CombinationFour Four;
    public CombinationFive Five;
    public CombinationSix Six;

    //Stored Combination
    private GameObject Combination_1 = null;
    private GameObject Combination_2 = null;
    private GameObject Combination_3 = null;
    private GameObject Combination_4 = null;
    private GameObject Combination_5 = null;
    private GameObject Combination_6 = null;

    private static bool firstInteraction = true;

    public override void ExecuteInteractiveAction()
    {
        base.ExecuteInteractiveAction();

        StartCoroutine(ActivateInXSec(2));

        //Enable Lock UI + Cursor
        InteractablesUIPannel.GetComponent<SkullLockManager>().OpenUI();
        if (firstInteraction) { UIManager.TheUI.TooltipMessage("Click the eyes to cycle colours.", 3); firstInteraction = false; }
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameObject.tag = "Untagged";

        //Define Combination
        if (Type == ChestTypeEnum.Standard)
        {
            //Combination One
            if (One == CombinationOne.Red)
            {
                Combination_1 = RedGem03;
            }
            else if (One == CombinationOne.Green)
            {
                Combination_1 = GreenGem03;
            }
            else if (One == CombinationOne.White)
            {
                Combination_1 = WhiteGem03;
            }

            //Combination Two
            if (Two == CombinationTwo.Red)
            {
                Combination_2 = RedGem04;
            }
            else if (Two == CombinationTwo.Green)
            {
                Combination_2 = GreenGem04;
            }
            else if (Two == CombinationTwo.White)
            {
                Combination_2 = WhiteGem04;
            }

        }
        else
        {
            //Combination One
            if (One == CombinationOne.Red)
            {
                Combination_1 = RedGem01;
            }
            else if (One == CombinationOne.Green)
            {
                Combination_1 = GreenGem01;
            }
            else if (One == CombinationOne.White)
            {
                Combination_1 = WhiteGem01;
            }
            //Combination Two
            if (Two == CombinationTwo.Red)
            {
                Combination_2 = RedGem02;
            }
            else if (Two == CombinationTwo.Green)
            {
                Combination_2 = GreenGem02;
            }
            else if (Two == CombinationTwo.White)
            {
                Combination_2 = WhiteGem02;
            }
            //Combination Three
            if (Three == CombinationThree.Red)
            {
                Combination_3 = RedGem03;
            }
            else if (Three == CombinationThree.Green)
            {
                Combination_3 = GreenGem03;
            }
            else if (Three == CombinationThree.White)
            {
                Combination_3 = WhiteGem03;
            }
            //Combination Four
            if (Four == CombinationFour.Red)
            {
                Combination_4 = RedGem04;
            }
            else if (Four == CombinationFour.Green)
            {
                Combination_4 = GreenGem04;
            }
            else if (Four == CombinationFour.White)
            {
                Combination_4 = WhiteGem04;
            }
            //Combination Five
            if (Five == CombinationFive.Red)
            {
                Combination_5 = RedGem05;
            }
            else if (Five == CombinationFive.Green)
            {
                Combination_5 = GreenGem05;
            }
            else if (Five == CombinationFive.White)
            {
                Combination_5 = WhiteGem05;
            }
            //Combination Six
            if (Six == CombinationSix.Red)
            {
                Combination_6 = RedGem06;
            }
            else if (Six == CombinationSix.Green)
            {
                Combination_6 = GreenGem06;
            }
            else if (Six == CombinationSix.White)
            {
                Combination_6 = WhiteGem06;
            }

        }

        if (GameObject.Find("Statue").gameObject.GetComponent<Interactive_Statue>().RiddleSolved || GameObject.Find("Statue").gameObject.GetComponent<Interactive_Statue>().IdolReturned)
        {
            UIManager.TheUI.TooltipMessage("Green, Green, Red, White, Red, Green.", 5f);
        }
    }

    private void Start()
    {
        //Fetch + Assign Private and Internal variables
        AssignVariables();
        //Create List of GameObjects making up each lock
        CreateLists();
    }

    void Update()
    {
        //Is the combination correct?
        if (Combination_1 != null)
        {
            if (Combination_1.activeInHierarchy == true && Combination_2.activeInHierarchy == true && Type == ChestTypeEnum.Standard)
            {
                //Stop further input + Open chest
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.LockSuccess, null);
                Invoke("OpenChest", 0.4f);
                //Reset stored combination
                Combination_1 = null;
                Combination_2 = null;
                Combination_3 = null;
                Combination_4 = null;
                Combination_5 = null;
                Combination_6 = null;
            }
            else if (Combination_1.activeInHierarchy == true && Combination_2.activeInHierarchy == true && Combination_3.activeInHierarchy == true && Combination_4.activeInHierarchy == true && Combination_5.activeInHierarchy == true && Combination_6.activeInHierarchy == true && Type == ChestTypeEnum.Advanced)
            {
                //Stop further input + Open chest
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.LockSuccess, null);
                Invoke("OpenChest", 0.4f);
                //Reset stored combination
                Combination_1 = null;
                Combination_2 = null;
                Combination_3 = null;
                Combination_4 = null;
                Combination_5 = null;
                Combination_6 = null;
            }
        }

        //Exit interface at any time
        if (Input.GetKeyDown(KeyCode.Q) && ChestCam.activeInHierarchy)
        {
            print("quit");
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            InteractablesUIPannel.GetComponent<SkullLockManager>().CloseUI();

            gameObject.tag = "Interactable";
        }
    }

    public void AssignVariables()
    {
        //Fetch + Assign Private and Internal variables
        FPSController = GameObject.Find("FPSController");

        SkullLockPannel = GameObject.Find("SkullLockUI");
        InteractablesUIPannel = GameObject.Find("InteractiblesUI");

        ChestCam = gameObject.transform.Find("CM ChestCam").gameObject;

        ChestOpenAnim = gameObject.transform.GetComponentInParent<Animator>();

        RedGem01 = gameObject.transform.Find("RedGem01").gameObject;
        RedGem02 = gameObject.transform.Find("RedGem02").gameObject;
        GreenGem01 = gameObject.transform.Find("GreenGem01").gameObject;
        GreenGem02 = gameObject.transform.Find("GreenGem02").gameObject;
        WhiteGem01 = gameObject.transform.Find("WhiteGem01").gameObject;
        WhiteGem02 = gameObject.transform.Find("WhiteGem02").gameObject;

        RedGem03 = gameObject.transform.Find("RedGem03").gameObject;
        RedGem04 = gameObject.transform.Find("RedGem04").gameObject;
        GreenGem03 = gameObject.transform.Find("GreenGem03").gameObject;
        GreenGem04 = gameObject.transform.Find("GreenGem04").gameObject;
        WhiteGem03 = gameObject.transform.Find("WhiteGem03").gameObject;
        WhiteGem04 = gameObject.transform.Find("WhiteGem04").gameObject;

        RedGem05 = gameObject.transform.Find("RedGem05").gameObject;
        RedGem06 = gameObject.transform.Find("RedGem06").gameObject;
        GreenGem05 = gameObject.transform.Find("GreenGem05").gameObject;
        GreenGem06 = gameObject.transform.Find("GreenGem06").gameObject;
        WhiteGem05 = gameObject.transform.Find("WhiteGem05").gameObject;
        WhiteGem06 = gameObject.transform.Find("WhiteGem06").gameObject;
    }

    public void CreateLists()
    {
        //Create List of GameObjects making up each lock
        LeftSkull.Add(gameObject.transform.Find("LeftSkull").gameObject);
        LeftSkull.Add(RedGem01);
        LeftSkull.Add(RedGem02);

        RightSkull.Add(gameObject.transform.Find("RightSkull").gameObject);
        RightSkull.Add(RedGem05);
        RightSkull.Add(RedGem06);
    }

    public void SwitchChestType()
    {
        if (Type == ChestTypeEnum.Standard)
        {
            foreach (GameObject item in LeftSkull)
            {
                item.SetActive(false);
            }
            foreach (GameObject item in RightSkull)
            {
                item.SetActive(false);
            }
        }

        if (Type == ChestTypeEnum.Advanced)
        {
            foreach (var item in LeftSkull)
            {
                item.SetActive(true);
            }
            foreach (var item in RightSkull)
            {
                item.SetActive(true);
            }
        }
    }

    void OpenChest()
    {
        //Close Lock UI
        InteractablesUIPannel.GetComponent<SkullLockManager>().CloseUI();
        //Play open animation
        ChestOpenAnim.Play(AnimationManager.SkullLockChest_Open);
        AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.ChestOpen, null);
        //Disable interactability
        gameObject.tag = "Untagged";
        Cursor.visible = false;
    }

    #region Button Functions

    public void BtnClick_01()
    {
        //Cycle Gems
        if (Type == ChestTypeEnum.Advanced)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(AudioManager.GlobalSFXManager.SkullLockEyeClick);

            if (RedGem01.activeInHierarchy == true)
            {
                RedGem01.SetActive(false);
                GreenGem01.SetActive(true);
            }
            else if (GreenGem01.activeInHierarchy == true)
            {
                GreenGem01.SetActive(false);
                WhiteGem01.SetActive(true);
            }
            else if (WhiteGem01.activeInHierarchy == true)
            {
                WhiteGem01.SetActive(false);
                RedGem01.SetActive(true);
            }
        }
    }

    public void BtnClick_02()
    {
        //Cycle Gems
        if (Type == ChestTypeEnum.Advanced)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(AudioManager.GlobalSFXManager.SkullLockEyeClick);

            if (RedGem02.activeInHierarchy == true)
            {
                RedGem02.SetActive(false);
                GreenGem02.SetActive(true);
            }
            else if (GreenGem02.activeInHierarchy == true)
            {
                GreenGem02.SetActive(false);
                WhiteGem02.SetActive(true);
            }
            else if (WhiteGem02.activeInHierarchy == true)
            {
                WhiteGem02.SetActive(false);
                RedGem02.SetActive(true);
            }
        }
    }

    public void BtnClick_03()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(AudioManager.GlobalSFXManager.SkullLockEyeClick);

        //Cycle Gems
        if (RedGem03.activeInHierarchy == true)
        {
            RedGem03.SetActive(false);
            GreenGem03.SetActive(true);
        }
        else if (GreenGem03.activeInHierarchy == true)
        {
            GreenGem03.SetActive(false);
            WhiteGem03.SetActive(true);
        }
        else if (WhiteGem03.activeInHierarchy == true)
        {
            WhiteGem03.SetActive(false);
            RedGem03.SetActive(true);
        }
    }

    public void BtnClick_04()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(AudioManager.GlobalSFXManager.SkullLockEyeClick);

        //Cycle Gems
        if (RedGem04.activeInHierarchy == true)
        {
            RedGem04.SetActive(false);
            GreenGem04.SetActive(true);
        }
        else if (GreenGem04.activeInHierarchy == true)
        {
            GreenGem04.SetActive(false);
            WhiteGem04.SetActive(true);
        }
        else if (WhiteGem04.activeInHierarchy == true)
        {
            WhiteGem04.SetActive(false);
            RedGem04.SetActive(true);
        }
    }

    public void BtnClick_05()
    {
        //Cycle Gems
        if (Type == ChestTypeEnum.Advanced)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(AudioManager.GlobalSFXManager.SkullLockEyeClick);

            if (RedGem05.activeInHierarchy == true)
            {
                RedGem05.SetActive(false);
                GreenGem05.SetActive(true);
            }
            else if (GreenGem05.activeInHierarchy == true)
            {
                GreenGem05.SetActive(false);
                WhiteGem05.SetActive(true);
            }
            else if (WhiteGem05.activeInHierarchy == true)
            {
                WhiteGem05.SetActive(false);
                RedGem05.SetActive(true);
            }
        }
    }

    public void BtnClick_06()
    {
        //Cycle Gems
        if (Type == ChestTypeEnum.Advanced)
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(AudioManager.GlobalSFXManager.SkullLockEyeClick);

            if (RedGem06.activeInHierarchy == true)
            {
                RedGem06.SetActive(false);
                GreenGem06.SetActive(true);
            }
            else if (GreenGem06.activeInHierarchy == true)
            {
                GreenGem06.SetActive(false);
                WhiteGem06.SetActive(true);
            }
            else if (WhiteGem06.activeInHierarchy == true)
            {
                WhiteGem06.SetActive(false);
                RedGem06.SetActive(true);
            }
        }
    }

    #endregion
}
