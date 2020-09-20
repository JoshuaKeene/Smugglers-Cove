using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftTrigger : MonoBehaviour
{
    public GameObject Player;
    public GameObject Lift;

    public bool PlayerInLift = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.transform.parent = Lift.transform;
            PlayerInLift = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.transform.parent = null;
            PlayerInLift = false;
        }
    }

}
