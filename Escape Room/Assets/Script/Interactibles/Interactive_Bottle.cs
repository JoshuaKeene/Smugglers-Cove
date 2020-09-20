using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive_Bottle : InteractiveObject
{
    [Header("Statue Link")]
    public GameObject Statue;
    internal bool Drunk;

    private MeshCollider BottleCollider;
    private MeshRenderer BottleRenderer;

    private static bool firstInteraction = true;

    private void Start()
    {
        BottleCollider = gameObject.GetComponent<MeshCollider>();
        BottleRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    public override void ExecuteInteractiveAction()
    {
        if (firstInteraction) { DialogueManager.Manager.Dialogue("Woah... That's some strong stuff.", null); firstInteraction = false; }

        BottleCollider.enabled = false;
        BottleRenderer.enabled = false;
        Drunk = true;

        Statue.tag = "Interactable";
        StartCoroutine("DrunkDuration");
        PostProcessManager.TheManager.ChromaticAberration_Drunk(true);

        AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.Drink, null);
    }

    public IEnumerator DrunkDuration()
    {
        yield return new WaitForSeconds(15f);
        
        if (Statue.GetComponent<Interactive_Statue>().Interacting)
        {
            StartCoroutine("DrunkDuration"); 
        }
        else
        {
            SoberUp();
        }
    }

    public void SoberUp()
    {
        Statue.tag = "Untagged";

        Drunk = false;
        BottleCollider.enabled = true;
        BottleRenderer.enabled = true;
        PostProcessManager.TheManager.ChromaticAberration_Drunk(false);
    }
}
