using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive_Switch : InteractiveObject //Attached to switch gameobject
{
    private bool SwitchOn = false;
    
    public override void ExecuteInteractiveAction()
    {
        base.ExecuteInteractiveAction();

        if (!SwitchOn)
        {
            gameObject.GetComponent<Animator>().Play("SwitchOnAnimation"); //or gameObject.GetComponent<Animator>().SetBool ... etc
            SwitchOn = true;
        }
        else
        {
            gameObject.GetComponent<Animator>().Play("SwitchOffAnimation"); //or gameObject.GetComponent<Animator>().SetBool ... etc
            SwitchOn = false;
        }
        
        StartCoroutine(ActivateInXSec(2));       
    }
}
