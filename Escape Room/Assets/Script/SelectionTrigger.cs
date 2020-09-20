using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SelectionTrigger : MonoBehaviour
{
    public GameObject InteractInterfacePlayer;
    public GameObject InteractInterfaceObject;
    public Material SelectedMat;
    public Material SelectedMatProps;
    private bool InMenu = false;

    // Update is called once per frame
    void Update()
    {
        var interactableRenderer = gameObject.GetComponent<Renderer>();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (InMenu == false && interactableRenderer.sharedMaterial == SelectedMat || interactableRenderer.sharedMaterial == SelectedMatProps)
            {
                //Debug.Log("YYYYEEEE");
                InMenu = true;
                Cursor.lockState = CursorLockMode.None;
                GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = false;
                if (InteractInterfaceObject == null)
                {
                    return; // exit method for example
                }
                else
                {
                    InteractInterfaceObject.SetActive(true);
                }
                if (InteractInterfacePlayer == null)
                {
                    return; // exit method for example
                }
                else
                {
                    InteractInterfacePlayer.SetActive(true);
                }
               
            }
            else if (InMenu == true)
            {
                Cursor.lockState = CursorLockMode.Locked;
                GameObject.Find("FPSController").GetComponent<FirstPersonController>().enabled = true;
                InMenu = false;
                if (InteractInterfaceObject == null)
                {
                    return; // exit method for example
                }
                else
                {
                    InteractInterfaceObject.SetActive(false);
                }
                if (InteractInterfacePlayer == null)
                {
                    return; // exit method for example
                }
                else
                {
                    InteractInterfacePlayer.SetActive(false);
                }
            }
        }
 
    }
}
