using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class InventoryManager : MonoBehaviour
{
    public List<InventoryItem> Items;
    public int InventoryLimit;
    public int CurrentInventoryIndex = 0;

    public List<GameObject> ItemSlots = new List<GameObject>();
    public List<GameObject> GlowSlots = new List<GameObject>();

    public GameObject IconsParent;
    public GameObject GlowParent;

    public GameObject InventoryPannel;

    public Sprite Empty;

    public Text NameTxt;
    public Text DescTxt;

    public GameObject FPSController;

    public GameObject CurItem_Frame;
    public Image CurItem_Image;

    public Animator InventoryAnimator;
    
    private bool InventoryOpen = false;
    private bool Opening = false;
    private bool Closing = false;

    public static InventoryManager TheInventory;

    private void Start()
    {
        //Assign Static
        TheInventory = this;

        //Create lists
        for (int i = 0; i < IconsParent.transform.childCount; i++)
        {
            ItemSlots.Add(IconsParent.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < GlowParent.transform.childCount; i++)
        {
            GlowSlots.Add(GlowParent.transform.GetChild(i).gameObject);
        }

        //Assign empty image
        foreach (var item in ItemSlots)
        {
            item.GetComponent<Image>().sprite = Empty;
        }

        //Deactivate inventory on start
        InventoryPannel.SetActive(false);

        //Disable equiped item on start
        CurItem_Frame.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (InventoryOpen == false && Opening == false)
            {
                //Checks with UI manager if any UIs active
                if (UIManager.TheUI.AreAnyUIsActive()) return;

                Opening = true;
                //Lock Inputs
                UIManager.TheUI.InputLockState(false);
                //Set Focal Distance
                PostProcessManager.TheManager.SetFocalDistanceMenu();
                //Enable Inventory UI
                InventoryPannel.SetActive(true);
                NameTxt.gameObject.SetActive(true);
                //Update Inventory
                UpdateInventory();
                //Animate UI into frame
                InventoryAnimator.Play(AnimationManager.Inventory_Open, -1, 0f);
                //Wait for Inventory to open
                StartCoroutine("WaitForInventoryOpen");
            }
            else if (InventoryOpen == true && Closing == false)
            {
                Closing = true;
                //Lock Inputs
                UIManager.TheUI.InputLockState(true);
                UIManager.TheUI.ExitHint("TAB", false);
                //Set Focal Distance
                PostProcessManager.TheManager.SetFocalDistanceDefault();
                //Animate UI out of frame
                InventoryAnimator.Play(AnimationManager.Inventory_Close, -1, 0f);
                //Wait for Inventory to close
                StartCoroutine("WaitForInventoryClose");
            }
        }

        //Realtime Equiped Item Displayed
        if (CurrentInventoryIndex < Items.Count)
        {
            CurItem_Frame.SetActive(true);
            CurItem_Image.sprite = Items[CurrentInventoryIndex].Image;
        }
        else
        {
            CurItem_Frame.SetActive(false);
        }

        //Inventory Navigation
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && InventoryPannel.activeInHierarchy)
        {
            if (CurrentInventoryIndex > 0)
            {
                InventoryPannel.GetComponent<AudioSource>().PlayOneShot(AudioManager.GlobalSFXManager.InventoryMove);

                CurrentInventoryIndex--;
                UpdateInventory();
            }
        }
        if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && InventoryPannel.activeInHierarchy)
        {
            if (CurrentInventoryIndex < InventoryLimit - 1)
            {
                InventoryPannel.GetComponent<AudioSource>().PlayOneShot(AudioManager.GlobalSFXManager.InventoryMove);

                CurrentInventoryIndex++;
                UpdateInventory();
            }
        }
        if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) && InventoryPannel.activeInHierarchy)
        {
            if (CurrentInventoryIndex < InventoryLimit - 4)
            {
                InventoryPannel.GetComponent<AudioSource>().PlayOneShot(AudioManager.GlobalSFXManager.InventoryMove);

                CurrentInventoryIndex = CurrentInventoryIndex + 4;
                UpdateInventory();
            }
        }
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && InventoryPannel.activeInHierarchy)
        {
            if (CurrentInventoryIndex > 3)
            {
                InventoryPannel.GetComponent<AudioSource>().PlayOneShot(AudioManager.GlobalSFXManager.InventoryMove);

                CurrentInventoryIndex = CurrentInventoryIndex - 4; ;
                UpdateInventory();
            }
        }
    }

    public IEnumerator WaitForInventoryOpen()
    {
        //Wait
        yield return new WaitForSeconds(0.5f);
        //Set InventoryOpen to true
        InventoryOpen = true;
        Opening = false;
        UIManager.TheUI.ExitHint("TAB", true);
    }
    public IEnumerator WaitForInventoryClose()
    {
        //Wait
        yield return new WaitForSeconds(0.5f);
        //Disable Inventory
        InventoryPannel.SetActive(false);
        //Set InventoryOpen to false
        InventoryOpen = false;
        Closing = false;
    }
    
    private void UpdateInventory()
    {
        //Step 1: Update Item Slots
        for (int i = 0; i < InventoryLimit; i++)
        {
            ItemSlots[i].GetComponent<Image>().enabled = false;
        }
        for (int i = 0; i < Items.Count; i++)
        {
            ItemSlots[i].GetComponent<Image>().enabled = true;
            ItemSlots[i].GetComponent<Image>().sprite = Items[i].Image;
        }

        //Step 2: Update Glow Behind Slots
        for (int i = 0; i < InventoryLimit; i++)
        {
            GlowSlots[i].SetActive(false);
        }
        GlowSlots[CurrentInventoryIndex].SetActive(true);


        //Step 3: Update Name/Description Tooltips
        if (CurrentInventoryIndex < Items.Count)
        {
            NameTxt.text = Items[CurrentInventoryIndex].Name;
            DescTxt.text = Items[CurrentInventoryIndex].Description;
        }
        else
        {
            NameTxt.text = "Empty";
            DescTxt.text = "No Item Selected";
        }


    }

    #region InventoryUtilities

    public void AddItem(InventoryItem X)
    {
        if(Items.Count == InventoryLimit)
        {
            print("Inventory Full");
            return;
        }

        Items.Add(X);
    }

    public bool HasItem(string Name)
    {
        bool hasItem = false;

        foreach (var item in Items)
        {
            if(item.Name == Name)
            {
                hasItem = true;
            }
        }

        return hasItem;
    }

    public void RemoveItem(string Name)
    {
        if (HasItem(Name))
        {
            int INDX = GetIndexOfItem(Name);
            Items.RemoveAt(INDX);
        }
        UpdateInventory();
    }

    public void RemoveItem(int Index)
    {
        Items.RemoveAt(Index);
    }

    public int GetIndexOfItem(string Name)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Name == Name) return i;
        }
        return -1;
    }

    public Sprite GetSpriteOfItem(string name)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Name == name) return Items[i].Image;
        }

        return null;
    }

    #endregion
}


[System.Serializable]
public class InventoryItem
{
    public string Name;
    public string Description;
    public Sprite Image;
}
