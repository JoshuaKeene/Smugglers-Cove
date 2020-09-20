using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    [Header("Base Interactive Variables")]
    public string Tooltip;
    public bool CanBeInteractedWith;

    public virtual void ExecuteInteractiveAction()
    {
        CanBeInteractedWith = false;
    }

    public IEnumerator ActivateInXSec(int x)
    {
        yield return new WaitForSeconds(x);
        CanBeInteractedWith = true;
    }
}

