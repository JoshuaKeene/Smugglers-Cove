using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTutorial : MonoBehaviour
{
    public List<GameObject> Items = new List<GameObject>();

    private bool ItemPickedUp = false;

    // Update is called once per frame
    void Update()
    {
        if (!ItemPickedUp)
        {
            foreach (var item in Items)
            {
                if (!item.activeInHierarchy) 
                { 
                    ItemPickedUp = true;
                    UIManager.TheUI.TooltipMessage("'TAB' to open Inventory.", 2);
                }
            }
        }
    }
}
