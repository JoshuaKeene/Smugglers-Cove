using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTrigger : MonoBehaviour
{
    public GameObject AssociatedInteractor;
    public bool IsAuto;
    public GameObject ActivateDirector;
    public GameObject DeactivateDirector;
    BoxCollider TriggerCollider;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (IsAuto)
            {
                if (AssociatedInteractor.GetComponent<DialogueScript>().IsTalking) return;
                AssociatedInteractor.GetComponent<DialogueScript>().DialogueInit();
                TriggerCollider = AssociatedInteractor.GetComponent<BoxCollider>();
                TriggerCollider.enabled = false;

                if (ActivateDirector != null) { ActivateDirector.SetActive(true); }
                else { return; }

                if (DeactivateDirector != null) { DeactivateDirector.SetActive(false); }
                else { return; }
            }
        }
    }
}
