using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupTrigger : MonoBehaviour
{
    //private GameObject InventorySlot;
    //private Sprite ItemPickupIcon;
    public Material SelectedMat;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var interactableRenderer = gameObject.GetComponent<Renderer>();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E Pressed");
            if (interactableRenderer.sharedMaterial == SelectedMat)
            {
                Debug.Log("Is Selected");
                //InventorySlot = GameObject.Find("ItemImage01");
                //ItemPickupIcon = gameObject.GetComponent<ItemBottle>().ItemIcon;
                gameObject.SetActive(false);
                //ItemPickupIcon = InventorySlot.GetComponent<Image>().;
                

            }
        }


    }
}
