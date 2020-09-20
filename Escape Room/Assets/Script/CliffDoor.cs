using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CliffDoor : MonoBehaviour
{
    public GameObject LinkedPodium;
    private bool Opened = false;

    // Update is called once per frame
    void Update()
    {
        if (LinkedPodium.GetComponent<Interactive_Podium>().Solved && !Opened)
        {
            Opened = true;
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        gameObject.GetComponent<Animator>().Play(AnimationManager.CliffDoor_Open);
        gameObject.GetComponent<AudioSource>().PlayOneShot(AudioManager.GlobalSFXManager.StoneDoor);
    }
}
