using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class OpenInventory : MonoBehaviour
{
    private bool InInventory = false;
    private Animator InventoryAnimator;
    public GameObject PlayerInventory;
    
    // Start is called before the first frame update
    void Start()
    {
        InventoryAnimator = PlayerInventory.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (gameObject.GetComponent<FirstPersonController>().enabled == true && InInventory == false)
            {
                //Debug.Log("Pressed I");
                PlayerInventory.SetActive(true);
                InventoryAnimator.Play("InventoryOpenAnim", -1, 0f);
                Cursor.lockState = CursorLockMode.None;
                gameObject.GetComponent<FirstPersonController>().enabled = false;
                Invoke("WaitForAnimOpen", 1f);
            }
            else if (gameObject.GetComponent<FirstPersonController>().enabled == false && InInventory == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                gameObject.GetComponent<FirstPersonController>().enabled = true;
                InventoryAnimator.Play("InventoryCloseAnim", -1, 0f);
                Invoke("WaitForAnimClose", 1f);
            }
        }

    }

    public void WaitForAnimClose()
    {
        PlayerInventory.SetActive(false);
        InInventory = false;
    }

    public void WaitForAnimOpen()
    {
        InInventory = true;
    }
}
