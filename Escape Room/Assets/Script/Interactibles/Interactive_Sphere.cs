using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive_Sphere : InteractiveObject
{
    public override void ExecuteInteractiveAction()
    {
        base.ExecuteInteractiveAction();

        if (InventoryManager.TheInventory.Items[InventoryManager.TheInventory.CurrentInventoryIndex].Name == "Sword")
        {
            print("Bottle was used");
            InventoryManager.TheInventory.RemoveItem("Sword");
            gameObject.SetActive(false);
        }

        print("CLICK");

        StartCoroutine(ActivateInXSec(2));
                    
    }

}
