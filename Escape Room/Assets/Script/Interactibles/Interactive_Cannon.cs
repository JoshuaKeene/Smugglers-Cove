using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive_Cannon : InteractiveObject
{
    private bool GunpowderLoaded = false;
    private bool CannonBallLoaded = false;
    private bool Fired = false;

    private static bool firstInteraction = true;

    [Header("Door to Destroy")]
    public GameObject Door;

    public override void ExecuteInteractiveAction()
    {
        base.ExecuteInteractiveAction();

        if (InventoryManager.TheInventory.CurrentInventoryIndex < InventoryManager.TheInventory.Items.Count)
        {
            if (InventoryManager.TheInventory.Items[InventoryManager.TheInventory.CurrentInventoryIndex].Name == "Gunpowder Keg" && GunpowderLoaded == false)
            {
                print("Gunpowder Loaded");

                GunpowderLoaded = true;

                UIManager.TheUI.TooltipMessage("Gunpowder loaded!", 1f);
                AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.ItemSound, null);

                InventoryManager.TheInventory.RemoveItem("Gunpowder Keg");
                Tooltip = " Load Cannon Ball";
            }
            else if (InventoryManager.TheInventory.Items[InventoryManager.TheInventory.CurrentInventoryIndex].Name == "Cannon Ball" && GunpowderLoaded == true)
            {
                print("Cannon Ball Loaded");

                CannonBallLoaded = true;

                UIManager.TheUI.TooltipMessage("Cannon ball loaded!", 1f);
                AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.ItemSound, null);

                InventoryManager.TheInventory.RemoveItem("Cannon Ball");
                Tooltip = " Fire";
            }
            else if (InventoryManager.TheInventory.Items[InventoryManager.TheInventory.CurrentInventoryIndex].Name == "Lit Torch" && CannonBallLoaded == true)
            {
                print("FIRE!");
                AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.Fuse, null);
                StartCoroutine("WaitForFuse");
            }
            else
            {
                if (InventoryManager.TheInventory.Items[InventoryManager.TheInventory.CurrentInventoryIndex].Name == "Torch" && CannonBallLoaded && firstInteraction)
                {
                    DialogueManager.Manager.Dialogue("I need to light this somehow...", null); 
                    firstInteraction = false;
                }

                UIManager.TheUI.TooltipMessage("You do not have the requied item", 2f);
            }
        }
        else
        {
            UIManager.TheUI.TooltipMessage("You do not have the requied item", 2f);
        }


        print("CLICK");

        StartCoroutine(ActivateInXSec(2));
                    
    }

    private IEnumerator WaitForFuse()
    {
        yield return new WaitForSeconds(1.5f);

        gameObject.transform.Find("Cannon Blast").gameObject.SetActive(true);
        Fired = true;
        gameObject.tag = "Untagged";
        StartCoroutine("WaitForExplosion");
    }

    private IEnumerator WaitForExplosion()
    {
        yield return new WaitForSeconds(0.5f);
        Door.SetActive(false);
    }

}
