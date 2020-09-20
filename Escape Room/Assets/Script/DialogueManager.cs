using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Manager;
    private DialogueScript TheDialogueScript;

    void Start()
    {
        Manager = this;
        TheDialogueScript = gameObject.GetComponent<DialogueScript>();
    }

    public bool IsTalking()
    {
        if (TheDialogueScript.IsTalking) return true;

        return false;
    }

    public void Dialogue(string dialogue, AudioClip audio)
    {
        if (TheDialogueScript.IsTalking) return;
        TheDialogueScript.AddBranch(dialogue, audio);
    }
}
