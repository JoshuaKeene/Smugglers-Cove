using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftActivator : MonoBehaviour
{
    private Animator LiftAnimator;
    public GameObject LiftTrigger;
    public GameObject Lift;
    BoxCollider LiftTriggerCollider;

    public GameObject Player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LiftAnimator = Lift.GetComponent<Animator>();
            LiftTriggerCollider = LiftTrigger.GetComponent<BoxCollider>();
            LiftTriggerCollider.enabled = false;
            LiftAnimator.SetBool("Activate", true);

            Player.transform.parent = Lift.transform;
            Invoke("Unparent", 3);
        }
    }

    public void Unparent()
    {
        Player.transform.parent = null;
    }
}
