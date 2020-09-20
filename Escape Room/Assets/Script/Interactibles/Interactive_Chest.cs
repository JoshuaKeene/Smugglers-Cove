using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive_Chest : InteractiveObject
{
    public Animator ChestOpenAnim;
    public string KeyItemName;

    private static bool firstInteraction = true;

    public override void ExecuteInteractiveAction()
    {
        base.ExecuteInteractiveAction();

        print("CLICK");

        StartCoroutine(ActivateInXSec(2));

        if (InventoryManager.TheInventory.CurrentInventoryIndex < InventoryManager.TheInventory.Items.Count)
        {
            if (InventoryManager.TheInventory.Items[InventoryManager.TheInventory.CurrentInventoryIndex].Name == KeyItemName)
            {
                InventoryManager.TheInventory.RemoveItem(KeyItemName);
                AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.LockSuccess, null);
                gameObject.tag = "Untagged";

                StartCoroutine("Wait");
            }
            else
            {
                AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.LockFail, null);
                UIManager.TheUI.TooltipMessage("Incorrect item.", 2f);
                if (firstInteraction) { DialogueManager.Manager.Dialogue("This needs a key...", null); firstInteraction = false; return; }
            }
        }
        else
        {
            AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.LockFail, null);
            UIManager.TheUI.TooltipMessage("You have no item equiped.", 2f);
            if (firstInteraction) { DialogueManager.Manager.Dialogue("This needs a key...", null); firstInteraction = false; return; }
        }         
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.4f);

        AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.ChestOpen, null);
        ChestOpenAnim.Play(AnimationManager.Chest_Open);
    }
}
