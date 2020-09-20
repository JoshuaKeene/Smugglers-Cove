using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive_ShipWheel : InteractiveObject
{
    
    
    public override void ExecuteInteractiveAction()
    {
        gameObject.GetComponent<Animator>().Play(AnimationManager.CombinationWheel, -1, 0f);
        gameObject.GetComponent<MeshCollider>().enabled = false;
        StartCoroutine("WaitForAnim");
    }

    public IEnumerator WaitForAnim()
    {
        yield return new WaitForSeconds(11f);
        gameObject.GetComponent<MeshCollider>().enabled = true;
        gameObject.GetComponent<Animator>().Play(AnimationManager.Nothing, -1, 0f);
    }

    
}
