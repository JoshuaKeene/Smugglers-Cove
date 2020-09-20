using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenHutDoor : MonoBehaviour
{
    public GameObject Door;
    private bool Open = false;
    
    void Update()
    {
        if (gameObject.GetComponent<Interactive_FlagPost>().Raised == true && Open == true)
        {
            Open = false;
            Door.GetComponent<Animator>().Play(AnimationManager.HutDoor_Close); 
        }
        else if (gameObject.GetComponent<Interactive_FlagPost>().Raised == false && Open == false) 
        {
            Open = true;
            Door.GetComponent<Animator>().Play(AnimationManager.HutDoor_Open); 
        }
    }
}
