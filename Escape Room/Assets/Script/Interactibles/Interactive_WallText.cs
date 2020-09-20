using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive_WallText : InteractiveObject
{
    [Header("Wall Text Variables")]
    public float Duration;
    
    public override void ExecuteInteractiveAction()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        //gameObject.GetComponent<DialogueScript>().StartingDialogueBranch = 0;
        gameObject.GetComponent<DialogueScript>().DialogueInit();
        StartCoroutine("DisableCollider", Duration);
    }

    public IEnumerator DisableCollider(float Duration)
    {
        yield return new WaitForSeconds(Duration);
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
