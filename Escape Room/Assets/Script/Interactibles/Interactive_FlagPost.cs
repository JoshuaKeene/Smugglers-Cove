using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive_FlagPost : InteractiveObject
{
    public bool Raised = true;
    public Animator FlagAnimator;
    
    public override void ExecuteInteractiveAction()
    {
        base.ExecuteInteractiveAction();

        if (Raised == true)
        {
            print("LOWER!");
            FlagAnimator.Play(AnimationManager.FlagPost_Lower, -1, 0f);
            Raised = false;

            Tooltip = " Raise";

        }
        else
        {
            print("HOIST!");
            FlagAnimator.Play(AnimationManager.FlagPost_Raise, -1, 0f);
            Raised = true;

            Tooltip = " Lower";
        }

        print("CLICK");

        StartCoroutine(ActivateInXSec(2));
                    
    }

}
