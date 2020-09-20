using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive_FixShipWheel : InteractiveObject
{
    [Header("Replacement Object")]
    public GameObject ReplacementWheel;

    private bool firstInteraction = true;
    
    public override void ExecuteInteractiveAction()
    {
        if (firstInteraction) { DialogueManager.Manager.Dialogue("Damn! It's broken, gonna need to find a replacement.", null); firstInteraction = false; Tooltip = " Replace"; return; }

        if (InventoryManager.TheInventory.CurrentInventoryIndex < InventoryManager.TheInventory.Items.Count)
        {
            if (InventoryManager.TheInventory.Items[InventoryManager.TheInventory.CurrentInventoryIndex].Name == "Ship Wheel")
            {
                AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.ItemSound, null);
                ReplacementWheel.SetActive(true);
                gameObject.SetActive(false);
                InventoryManager.TheInventory.RemoveItem("Ship Wheel");
                UIManager.TheUI.TooltipMessage("Wheel Fixed!", 1f);
            }
            else
            {
                UIManager.TheUI.TooltipMessage("Incorrect item.", 2f);
            }
        }
        else
        {
            UIManager.TheUI.TooltipMessage("You have no item equiped.", 2f);
        }
    }
}
