using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive_TreasureDig : InteractiveObject
{
    [Header("Treasure Variables")]
    public GameObject ShipWheel;

    private static bool firstInteraction = true;

    public override void ExecuteInteractiveAction()
    {
        if (InventoryManager.TheInventory.CurrentInventoryIndex < InventoryManager.TheInventory.Items.Count)
        {
            if (InventoryManager.TheInventory.Items[InventoryManager.TheInventory.CurrentInventoryIndex].Name == "Shovel")
            {
                AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.DigSand, null);
                ShipWheel.GetComponent<Animator>().Play(AnimationManager.Dig_ShipWheel);
                gameObject.GetComponent<SphereCollider>().enabled = false;
                StartCoroutine("WaitForDig");
            }
            else
            {
                UIManager.TheUI.TooltipMessage("You can't use that to dig.", 2f);
                if (firstInteraction) { DialogueManager.Manager.Dialogue("I need a shovel...", null); firstInteraction = false; }
            }
        }
        else
        {
            UIManager.TheUI.TooltipMessage("You have no item equiped.", 2f);
            if (firstInteraction) { DialogueManager.Manager.Dialogue("I need a shovel...", null); firstInteraction = false; }
        }
    }

    public IEnumerator WaitForDig()
    {
        yield return new WaitForSeconds(3f);
        ShipWheel.tag = "Pickupable";
    }
}
