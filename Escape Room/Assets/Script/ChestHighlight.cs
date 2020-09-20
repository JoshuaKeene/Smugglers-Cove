using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHighlight : MonoBehaviour
{
    public GameObject ChestLid;

    // Update is called once per frame
    void Update()
    {
        var LidOutline = ChestLid.GetComponent<Outline>();
        if (LidOutline.enabled)
        {
            gameObject.GetComponent<Outline>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Outline>().enabled = false;
        }
    }
}
