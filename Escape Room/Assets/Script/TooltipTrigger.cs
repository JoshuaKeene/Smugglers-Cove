using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipTrigger : MonoBehaviour
{
    [TextArea(1, 10)]
    public string Message;
    public float Duration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !UIManager.TheUI.MainCanvas.GetComponent<MainMenu>().InMenu)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            UIManager.TheUI.TooltipMessage(Message, Duration);
        }
    }
}

