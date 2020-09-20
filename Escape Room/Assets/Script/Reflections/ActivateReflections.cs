using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateReflections : MonoBehaviour
{
    public GameObject Main;
    public GameObject Menu;
    private bool MenuProbeIsActive = true;

    private void OnTriggerEnter(Collider other)
    {
        print("1");
        if ((other.CompareTag("Player")) && (MenuProbeIsActive == true))
        {
            Menu.SetActive(false);
            Main.SetActive(true);
            MenuProbeIsActive = false;

        }
        else if ((other.CompareTag("Player")) && (MenuProbeIsActive == false))
        {
            Menu.SetActive(true);
            Main.SetActive(false);
            MenuProbeIsActive = true;
        }
    }
}
