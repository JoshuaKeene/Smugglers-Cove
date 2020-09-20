using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactive_FirePit : InteractiveObject
{
    [Header("Fire Pit Variables")]
    public float TimeLimit;

    public Sprite Torch;
    public Sprite LitTorch;

    private static bool firstInteraction = true;

    public override void ExecuteInteractiveAction()
    {
        if (InventoryManager.TheInventory.CurrentInventoryIndex < InventoryManager.TheInventory.Items.Count)
        {
            if (InventoryManager.TheInventory.Items[InventoryManager.TheInventory.CurrentInventoryIndex].Name == "Torch")
            {
                UIManager.TheUI.TooltipMessage("Torch lit!", 2f);
                InventoryManager.TheInventory.Items[InventoryManager.TheInventory.GetIndexOfItem("Torch")].Image = LitTorch;
                InventoryManager.TheInventory.Items[InventoryManager.TheInventory.GetIndexOfItem("Torch")].Name = "Lit Torch";
                gameObject.GetComponent<MeshCollider>().enabled = false;
                StartCoroutine("LitDuration");
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

    public IEnumerator LitDuration()
    {
        yield return new WaitForSeconds(TimeLimit);
        InventoryManager.TheInventory.Items[InventoryManager.TheInventory.GetIndexOfItem("Lit Torch")].Image = Torch;
        InventoryManager.TheInventory.Items[InventoryManager.TheInventory.GetIndexOfItem("Lit Torch")].Name = "Torch";
        if (firstInteraction) { UIManager.TheUI.TooltipMessage("Torch extinguished!\nYou need to be faster!", 2f); firstInteraction = false; }
        else { UIManager.TheUI.TooltipMessage("Torch extinguished!", 2f); }
        gameObject.GetComponent<MeshCollider>().enabled = true;
    }

}
