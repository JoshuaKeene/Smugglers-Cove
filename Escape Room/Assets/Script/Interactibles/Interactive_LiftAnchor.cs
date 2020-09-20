using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive_LiftAnchor : InteractiveObject
{
    public Animator LiftAnim;
    public GameObject Lift;
    public string InteractItemName;

    private GameObject LiftZone;

    private void Start()
    {
        LiftZone = Lift.transform.Find("Trigger").gameObject;
    }

    public override void ExecuteInteractiveAction()
    {
        base.ExecuteInteractiveAction();

        print("CLICK");

        StartCoroutine(ActivateInXSec(2));

        if (InventoryManager.TheInventory.CurrentInventoryIndex < InventoryManager.TheInventory.Items.Count)
        {
            if (InventoryManager.TheInventory.Items[InventoryManager.TheInventory.CurrentInventoryIndex].Name == InteractItemName)
            {
                if (LiftZone.GetComponent<LiftTrigger>().PlayerInLift)
                {
                    LiftAnim.Play(AnimationManager.Lift_Lower);
                    AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.RopeCut, null);

                    Lift.GetComponent<AudioSource>().PlayOneShot(AudioManager.GlobalSFXManager.LiftLower);

                    gameObject.SetActive(false);
                }
                else
                {
                    DialogueManager.Manager.Dialogue("I should probably stand on the lift before cutting this.", null);
                }
            }
            else
            {
                UIManager.TheUI.TooltipMessage("Sharp item required.", 3f);
            }
        }
        else
        {
            UIManager.TheUI.TooltipMessage("You have no item eqquiped.", 3f);
        }           
    }
}
