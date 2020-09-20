using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactive_ChessBoard : InteractiveObject //NxF7 BxA3
{
    [Tooltip("Use board coordinates")]
    [Header("Solution")]
    public string KnightPosition;
    public string BishopPosition;

    [Header("Puzzle Variables")]
    public GameObject ChessboardCam;
    public GameObject Buttons;
    public GameObject RayBlocker;
    public GameObject PuzzleUI;
    public GameObject KnightSelection;
    public GameObject BishopSelection;
    public GameObject ChessKeyItem;

    private GameObject KnightBorder;
    private GameObject BishopBorder;

    private GameObject KnightImage;
    private GameObject BishopImage;

    private GameObject KnightText;
    private GameObject BishopText;

    private GameObject ActiveKnight;
    private GameObject ActiveBishop;

    private bool KnightPlaced = false;
    private bool BishopPlaced = false;

    private bool Solved = false;

    private int interactions = 0;

    private static bool firstInteraction = true;

    public override void ExecuteInteractiveAction()
    {
        base.ExecuteInteractiveAction();

        OpenUI();

        if (firstInteraction) { UIManager.TheUI.TooltipMessage("'W' and 'S' to select between pieces.\nClick on spaces to place selected piece.", 6); firstInteraction = false; }

        if (InventoryManager.TheInventory.HasItem("Knight")) { KnightImage.SetActive(true); KnightText.SetActive(false); }
        else if (!KnightPlaced) { KnightImage.SetActive(false); KnightText.SetActive(true); }
        if (InventoryManager.TheInventory.HasItem("Bishop")) { BishopImage.SetActive(true); BishopText.SetActive(false); }
        else if (!BishopPlaced) { BishopImage.SetActive(false); BishopText.SetActive(true); }

        print("CLICK");

        StartCoroutine(ActivateInXSec(2));    
    }

    public void Start()
    {
        KnightBorder = KnightSelection.transform.Find("Selector").gameObject;
        BishopBorder = BishopSelection.transform.Find("Selector").gameObject;

        KnightImage = KnightSelection.transform.Find("Image").gameObject;
        BishopImage = BishopSelection.transform.Find("Image").gameObject;

        KnightText = KnightSelection.transform.Find("Text").gameObject;
        BishopText = BishopSelection.transform.Find("Text").gameObject;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && ChessboardCam.activeInHierarchy)
        {
            CloseUI();
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && ChessboardCam.activeInHierarchy)
        {
            AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.InventoryMove, null);
            KnightBorder.SetActive(true);
            BishopBorder.SetActive(false);
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && ChessboardCam.activeInHierarchy)
        {
            AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.InventoryMove, null);
            BishopBorder.SetActive(true);
            KnightBorder.SetActive(false);
        }

        if (Buttons.transform.Find(KnightPosition).gameObject.transform.Find("Knight").gameObject.activeInHierarchy && Buttons.transform.Find(BishopPosition).gameObject.transform.Find("Bishop").gameObject.activeInHierarchy && !Solved)
        {
            Solved = true;
            StartCoroutine("Success");
        }
    }

    public void OnBtnClick(string ID)
    {
        interactions++;
        Hints();

        var buttonPressed = Buttons.transform.Find(ID).gameObject;

        if (buttonPressed.transform.Find("Bishop").gameObject.activeInHierarchy || buttonPressed.transform.Find("Knight").gameObject.activeInHierarchy)
        {
            UIManager.TheUI.TooltipMessage("Space occupied.", 2f);
            return;
        }

        if (ActiveKnight == null && KnightBorder.activeInHierarchy && KnightImage.activeInHierarchy)
        {
            AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.ChessMove, null);

            buttonPressed.transform.Find("Knight").gameObject.SetActive(true);
            ActiveKnight = buttonPressed.transform.Find("Knight").gameObject;

            InventoryManager.TheInventory.RemoveItem("Knight");
            KnightPlaced = true;
        }
        else if (ActiveBishop == null && BishopBorder.activeInHierarchy && BishopImage.activeInHierarchy)
        {
            AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.ChessMove, null);

            buttonPressed.transform.Find("Bishop").gameObject.SetActive(true);
            ActiveBishop = buttonPressed.transform.Find("Bishop").gameObject;

            InventoryManager.TheInventory.RemoveItem("Bishop");
            BishopPlaced = true;
        }
        else if (ActiveKnight != null && KnightBorder.activeInHierarchy && KnightImage.activeInHierarchy)
        {
            AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.ChessMove, null);

            buttonPressed.transform.Find("Knight").gameObject.SetActive(true);
            ActiveKnight.SetActive(false);
            ActiveKnight = buttonPressed.transform.Find("Knight").gameObject;
        }
        else if (ActiveBishop != null && BishopBorder.activeInHierarchy && BishopImage.activeInHierarchy)
        {
            AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.ChessMove, null);

            buttonPressed.transform.Find("Bishop").gameObject.SetActive(true);
            ActiveBishop.SetActive(false);
            ActiveBishop = buttonPressed.transform.Find("Bishop").gameObject;
        }
        else
        {
            UIManager.TheUI.TooltipMessage("Selection unavailable.", 2f);
        }
    }

    private void Hints()
    {
        if ((interactions == 5) && (!KnightImage.activeInHierarchy || !BishopImage.activeInHierarchy))
        {
            DialogueManager.Manager.Dialogue("I think I need to find all the missing pieces.", null);
            return;
        }
        
        if (interactions == 10)
        {
            DialogueManager.Manager.Dialogue("What did that note say? 'put those pieces back EXACTLY where they were'.", null);
        }
        else if (interactions == 40)
        {
            DialogueManager.Manager.Dialogue("The Castle on F4 stops the King from moving to F7 and F8.", null);
        }
        else if (interactions == 50)
        {
            DialogueManager.Manager.Dialogue("I just need to make sure the King can't stay where he is or move to D7 or D8.", null);
        }
        else if (interactions == 65)
        {
            DialogueManager.Manager.Dialogue("I think the Bishops goes on C6.", null);
        }
        else if (interactions == 75)
        {
            DialogueManager.Manager.Dialogue("I think the Knight goes on B7.", null);
        }
    }

    public void OpenUI()
    {
        if (UIManager.TheUI.AreAnyUIsActive()) return;

        gameObject.tag = "Untagged";

        UIManager.TheUI.PlayerHUD.SetActive(false);
        UIManager.TheUI.CurItemFrame.SetActive(false);
        UIManager.TheUI.ExitHint("Q", true);
        UIManager.TheUI.InputLockState(false);
        UIManager.TheUI.FPSCamera.enabled = false;

        PostProcessManager.TheManager.SetFocalDistanceChessPuzzle();

        if(!Solved)
        {
            foreach (Transform child in Buttons.transform)
            {
                child.GetComponent<Button>().interactable = true;
            }
        }

        ChessboardCam.SetActive(true);
        PuzzleUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseUI()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        PuzzleUI.SetActive(false);
        ChessboardCam.SetActive(false);

        foreach (Transform child in Buttons.transform)
        {
            child.GetComponent<Button>().interactable = false;
        }

        PostProcessManager.TheManager.SetFocalDistanceDefault();

        UIManager.TheUI.FPSCamera.enabled = true;
        UIManager.TheUI.InputLockState(true);
        UIManager.TheUI.PlayerHUD.SetActive(true);
        UIManager.TheUI.CurItemFrame.SetActive(true);
        UIManager.TheUI.ExitHint("Q", false);

        gameObject.tag = "Interactable";
    }

    public IEnumerator Success()
    {
        RayBlocker.GetComponent<Image>().raycastTarget = true;
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Animator>().Play(AnimationManager.Chessboard_KeyReveal);
        yield return new WaitForSeconds(1.5f);
        CloseUI();
        ChessKeyItem.tag = ("Pickupable");
    }

    public void SpringSFX()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(AudioManager.GlobalSFXManager.Spring);
    }

    public void ImpactSFX()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(AudioManager.GlobalSFXManager.Impact);
    }
}
