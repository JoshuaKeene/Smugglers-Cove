using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive_Statue : InteractiveObject //Green, Green, Red, White, Red, Green
{
    public GameObject StatueUI;
    public GameObject StatueCam;
    public GameObject Statue;

    [HideInInspector]
    public int Phase = 1;
    [HideInInspector]
    public bool Interacting = false;

    internal bool AllContinueBtnOne = false;
    internal bool AllContinueBtnTwo = false;
    internal bool AllContinueBtnThree = false;
    internal bool AllContinueBtnFour = false;

    Dictionary<int, string> DialogueCache = new Dictionary<int, string>();

    private bool CanExit = true;

    internal bool LastPhase = false;
    internal bool LastPhaseEnd = false;

    internal bool hasIdol = false;
    internal bool hasGivenIdol = false;
    internal bool IdolReturned = false;

    internal bool RiddleSolved = false;
    private bool LookingForIdol = false;
    
    public override void ExecuteInteractiveAction()
    {
        base.ExecuteInteractiveAction();

        if (InventoryManager.TheInventory.HasItem("Golden Idol")) { HasIdol(true); }
        else { HasIdol(false); }

        LastPhase = false;

        OpenUI();

        if (LookingForIdol) { Statue.GetComponent<DialogueScript>().StartingDialogueBranch = 41; Phase = 10; }

        UpdatePhases();

        if (RiddleSolved || IdolReturned) { Statue.GetComponent<DialogueScript>().StartingDialogueBranch = 42; StatueUI.GetComponent<StatueUIManager>().BtnDisableOverride(true); }

        Statue.GetComponent<DialogueScript>().DialogueInit();

        print("CLICK");

        StartCoroutine(ActivateInXSec(2));
    }

    public void Start()
    {
        //Phase 1
        DialogueCache.Add(1, "You... can talk?"); //Isn't that obvious?
        DialogueCache.Add(2, "How is this possible?"); //This world is more mystical that you give it credit. <Continue>
        DialogueCache.Add(3, "Woah"); //Understandable.
        DialogueCache.Add(4, null); //ERROR

        //Phase 2
        DialogueCache.Add(5, "That's not an answer..."); //Well it's the one you're getting.
        DialogueCache.Add(6, "Can you help me escape?"); //What kind of help do you need? <Continue>
        DialogueCache.Add(7, null); //ERROR
        DialogueCache.Add(8, null); //ERROR

        //Phase 3
        DialogueCache.Add(9, "Can you fix my ship?"); //Do I look like I know how to fix a ship?
        DialogueCache.Add(10, "Can you... teleport me? Or something?"); //I'm going to pretend you didn't ask that.
        DialogueCache.Add(11, "I need the combination to that chest."); //Now that sounds like something I could do. <Continue>
        DialogueCache.Add(12, null); //ERROR

        //Phase 4
        DialogueCache.Add(13, "'Could do' ?"); //Well I'd like you to do something first. <Continue>
        DialogueCache.Add(14, null); //ERROR
        DialogueCache.Add(15, null); //ERROR
        DialogueCache.Add(16, null); //ERROR

        //Phase 5
        DialogueCache.Add(17, "Are you serious?"); //Adamantly.
        DialogueCache.Add(18, "What do you want?"); //I want you to answer a riddle. <Continue>
        DialogueCache.Add(19, null); //ERROR
        DialogueCache.Add(20, null); //ERROR

        //Phase 6
        DialogueCache.Add(21, "Fine."); //Wonderful. Here it is... Why couldn't the pirate play cards? <Continue>
        DialogueCache.Add(22, "No! Surely there is something else?"); //Oh come now, give it a go. If you get it wrong then I'll think of something else.  <Continue>
        DialogueCache.Add(23, null); //ERROR
        DialogueCache.Add(24, null); //ERROR

        //Phase 7
        DialogueCache.Add(25, "Because he wanted to Go Fish?"); //Incorrect! <Continue>
        DialogueCache.Add(26, "Because he was standing on the deck?"); //Correct! Well done. I'll keep up my end of the deal. The combination is: Red, Red, Red, Red, Red, Red. <Continue> <End>
        DialogueCache.Add(27, "Because the King would hang him?"); //Wrong! <Continue>
        DialogueCache.Add(28, "His parrot kept shouting his cards out?"); //Nope! <Continue>

        //Phase 8
        DialogueCache.Add(29, "I knew this was pointless."); //Nonsense, it was just some fun.
        DialogueCache.Add(30, "What about the 'something else'?"); //Well.. come to think of it.. If you could get my Idol back that the pirates here locked away then I'd help you. <Continue>
        DialogueCache.Add(31, null); //ERROR
        DialogueCache.Add(32, null); //ERROR

        //Phase 9
        DialogueCache.Add(33, "Idol?"); //Yes. A small statuette mad from gold.
        DialogueCache.Add(34, "Why'd they lock it away?"); //I think they thought it would contain me. Or they saw a shiny thing and took it, both equally possible.
        DialogueCache.Add(35, "Right I'll go find it then."); //I thank you for your service. <Continue> <End>
        DialogueCache.Add(36, null); //Well done. I'll keep up my end of the deal. The combination is: Red, Red, Red, Red, Red, Red. <Continue> <End>

        //Phase 10
        DialogueCache.Add(37, "No."); //What're you doing here then?
        DialogueCache.Add(38, null); //Well done. I'll keep up my end of the deal. The combination is: Red, Red, Red, Red, Red, Red. <Continue> <End>
        DialogueCache.Add(39, null); //ERROR
        DialogueCache.Add(40, null); //ERROR
    }

    public void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Q) && StatueUI.activeInHierarchy && CanExit) || (!Statue.GetComponent<DialogueScript>().IsTalking && LastPhaseEnd))
        {
            LastPhaseEnd = false;
            CloseUI();
        }

        if (Statue.GetComponent<DialogueScript>().UpdateCheck == true && StatueUI.GetComponent<StatueUIManager>().Continue == true)
        {
            StatueUI.GetComponent<StatueUIManager>().Continue = false;

            if (Phase == 9) { LookingForIdol = true; }

            if (hasGivenIdol) { LastPhaseEnd = true; IdolReturned = true; return; }
            if (LastPhase) { LastPhaseEnd = true; return; }
            if (AllContinueBtnTwo) { LastPhaseEnd = true; RiddleSolved = true; return; }

            Phase++;
            StatueUI.GetComponent<Animator>().Play(AnimationManager.Statue_DialogueChoice_FadeOut, -1, 0f);
            StartCoroutine("WaitForFadeAnim");
        }

        if (Statue.GetComponent<DialogueScript>().IsTalking) { CanExit = false; }
        else { CanExit = true; }
    }

    public void ButtonPress(int ButtonID)
    {
        if (Phase == 1) { ButtonID = ButtonID; }
        else if (Phase == 2) { ButtonID = ButtonID + 4; }
        else if (Phase == 3) { ButtonID = ButtonID + 8; }
        else if (Phase == 4) { ButtonID = ButtonID + 12; }
        else if (Phase == 5) { ButtonID = ButtonID + 16; }
        else if (Phase == 6) { ButtonID = ButtonID + 20; }
        else if (Phase == 7) { ButtonID = ButtonID + 24; }
        else if (Phase == 8) { ButtonID = ButtonID + 28; }
        else if (Phase == 9) { ButtonID = ButtonID + 32; }
        else if (Phase == 10) { ButtonID = ButtonID + 36; }

        Statue.GetComponent<DialogueScript>().StartingDialogueBranch = ButtonID;

        Statue.GetComponent<DialogueScript>().DialogueInit();
    }

    public void UpdatePhases()
    {
        if (Phase == 1)
        {
            StatueUI.GetComponent<StatueUIManager>().SetDialogueOptions(DialogueCache[1], DialogueCache[2], DialogueCache[3], DialogueCache[4], 2, "Statue:");
            //Last Phase = true;
        }
        else if (Phase == 2)
        {
            StatueUI.GetComponent<StatueUIManager>().SetDialogueOptions(DialogueCache[5], DialogueCache[6], DialogueCache[7], DialogueCache[8], 2, "Statue:");
            LastPhase = false;
        }
        else if (Phase == 3)
        {
            StatueUI.GetComponent<StatueUIManager>().SetDialogueOptions(DialogueCache[9], DialogueCache[10], DialogueCache[11], DialogueCache[12], 3, "Statue:");
            LastPhase = false;
        }
        else if (Phase == 4)
        {
            StatueUI.GetComponent<StatueUIManager>().SetDialogueOptions(DialogueCache[13], DialogueCache[14], DialogueCache[15], DialogueCache[16], 1, "Statue:");
            LastPhase = false;
        }
        else if (Phase == 5)
        {
            StatueUI.GetComponent<StatueUIManager>().SetDialogueOptions(DialogueCache[17], DialogueCache[18], DialogueCache[19], DialogueCache[20], 2, "Statue:");
            LastPhase = false;
        }
        else if (Phase == 6)
        {
            StatueUI.GetComponent<StatueUIManager>().SetDialogueOptions(DialogueCache[21], DialogueCache[22], DialogueCache[23], DialogueCache[24], 1, "Statue:");
            LastPhase = false;
        }
        else if (Phase == 7)
        {
            StatueUI.GetComponent<StatueUIManager>().SetDialogueOptions(DialogueCache[25], DialogueCache[26], DialogueCache[27], DialogueCache[28], 5, "Statue:"); //All (Hidden) (END 2)
            LastPhase = false;
        }
        else if (Phase == 8)
        {
            StatueUI.GetComponent<StatueUIManager>().SetDialogueOptions(DialogueCache[29], DialogueCache[30], DialogueCache[31], DialogueCache[32], 2, "Statue:");
            LastPhase = false;
        }
        else if (Phase == 9)
        {
            if (InventoryManager.TheInventory.HasItem("Golden Idol")) { StatueUI.GetComponent<StatueUIManager>().SetDialogueOptions(DialogueCache[33], DialogueCache[34], DialogueCache[35], DialogueCache[36], 4, "Statue:"); } //(END 4)
            else { StatueUI.GetComponent<StatueUIManager>().SetDialogueOptions(DialogueCache[33], DialogueCache[34], DialogueCache[35], DialogueCache[36], 3, "Statue:"); } //(END 3)
            LastPhase = true;
            if (InventoryManager.TheInventory.HasItem("Golden Idol")) { hasIdol = true; }
        }
        else if (Phase == 10)
        {
            //if (IdolReturned) { StatueUI.GetComponent<StatueUIManager>().SetDialogueOptions(null, null, null, null, 2, "Statue:"); }
            //else { StatueUI.GetComponent<StatueUIManager>().SetDialogueOptions(DialogueCache[37], DialogueCache[38], DialogueCache[39], DialogueCache[40], 2, "Statue:"); }//(END 1)
            StatueUI.GetComponent<StatueUIManager>().SetDialogueOptions(DialogueCache[37], DialogueCache[38], DialogueCache[39], DialogueCache[40], 2, "Statue:");
            LastPhase = true;
            if (InventoryManager.TheInventory.HasItem("Golden Idol")) { hasIdol = true; }
        }
    }

    public void HasIdol(bool HasIdol)
    {
        if (HasIdol || IdolReturned) 
        { 
            DialogueCache[36] = "You mean this Idol?";
            DialogueCache[38] = "Here is your Idol.";
        }
        else 
        { 
            DialogueCache[36] = null;
            DialogueCache[38] = null;
        }
    }

    public void OpenUI()
    {
        if (UIManager.TheUI.AreAnyUIsActive()) return;

        Interacting = true;

        UIManager.TheUI.PlayerHUD.SetActive(false);
        UIManager.TheUI.CurItemFrame.SetActive(false);
        UIManager.TheUI.ExitHint("Q", true);
        UIManager.TheUI.InputLockState(false);
        UIManager.TheUI.FPSCamera.enabled = false;

        StatueCam.SetActive(true);
        StatueUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Statue.GetComponent<DialogueScript>().ContinuousMode = false;

        StatueUI.GetComponent<Animator>().Play(AnimationManager.Statue_DialogueBox_FadeIn, -1, 0f);
    }

    public void CloseUI()
    {
        StatueUI.GetComponent<Animator>().Play(AnimationManager.Statue_DialogueBox_FadeOut, -1, 0f);
        UIManager.TheUI.ExitHint("Q", false);
        StartCoroutine("WaitForAnimClose");
    }

    public IEnumerator WaitForAnimClose()
    {
        yield return new WaitForSeconds(0.3f);

        Statue.GetComponent<DialogueScript>().ContinuousMode = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        StatueUI.SetActive(false);
        StatueCam.SetActive(false);

        UIManager.TheUI.FPSCamera.enabled = true;
        UIManager.TheUI.InputLockState(true);
        UIManager.TheUI.CurItemFrame.SetActive(true);
        UIManager.TheUI.PlayerHUD.SetActive(true);

        Interacting = false;
    }

    public IEnumerator WaitForFadeAnim()
    {
        yield return new WaitForSeconds(0.3f);

        UpdatePhases();
        StatueUI.GetComponent<Animator>().Play(AnimationManager.Statue_DialogueChoice_FadeIn, -1, 0f);
    }
}
