using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
    public float InteractionLength;
    //public GameObject RaycastOrigin;
    public Text Tooltip;

    private GameObject PhysicalObject;
    public GameObject HoldOrigin;
    public float ThrowStrength;

    //public GameObject FPSCharacter;
    private Transform _selection;

    private Shader defaultShader;
    private Shader highlightShader;

    private bool Fade = false;
    private bool Selection = false;

    //public GameObject LastInteractedObject;

    private enum RaycastObject
    {
        Nothing,
        Interactable,
        Physical,
        Pickupable,
        Document,
    }
    private RaycastObject LastRaycastedObject;
    public GameObject TargetObject;

    // Start is called before the first frame update
    void Start()
    {
        defaultShader = Shader.Find("Standard");
        highlightShader = Shader.Find("Toon/Basic Outline"); //"Outlined/Regular"
        LastRaycastedObject = RaycastObject.Nothing;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = gameObject.GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;

        if (_selection != null)
        {
            TargetObject = null;
            LastRaycastedObject = RaycastObject.Nothing;

            var selectionOutline = _selection.GetComponent<Outline>();
            selectionOutline.enabled = false;
            _selection = null;
        }

        if (!PhysicalObject && !UIManager.TheUI.AreAnyUIsActive())
        {
            if (Physics.Raycast(ray, out hit, InteractionLength))
            {
                //Have I hit something?
                if (hit.transform)
                {
                    var selection = hit.transform;
                    if (hit.transform.CompareTag("Interactable") | hit.transform.CompareTag("Pickupable") | hit.transform.CompareTag("Physical") | hit.transform.CompareTag("Document"))
                    {
                        //Debug.Log("SELECTABLE!");
                        var selectionOutline = selection.GetComponent<Outline>();
                        if (selectionOutline != null)
                        {
                            selectionOutline.enabled = true;
                        }

                        _selection = selection;
                    }

                    if (hit.transform.CompareTag("Interactable"))
                    {
                        TargetObject = hit.transform.gameObject;
                        if (TargetObject.GetComponent<InteractiveObject>().CanBeInteractedWith)
                        {
                            LastRaycastedObject = RaycastObject.Interactable;
                            Tooltip.text = "[E]" + TargetObject.GetComponent<InteractiveObject>().Tooltip;
                        }

                    }
                    else if (hit.transform.CompareTag("Pickupable"))
                    {
                        LastRaycastedObject = RaycastObject.Pickupable;
                        TargetObject = hit.transform.gameObject;
                        //TooltipFade();
                        Tooltip.text = "[E] Pick Up";
                    }
                    else if (hit.transform.CompareTag("Physical"))
                    {
                        LastRaycastedObject = RaycastObject.Physical;
                        TargetObject = hit.transform.gameObject;
                        Tooltip.text = "[RMB] Hold Object";
                    }
                    else if (hit.transform.CompareTag("Document"))
                    {
                        LastRaycastedObject = RaycastObject.Document;
                        TargetObject = hit.transform.gameObject;
                        Tooltip.text = "[E] Read";
                    }
                }
            }
        }

        if (_selection == null)
        {
            if (Selection)
            {
                if (Fade)
                {
                    Fade = false;
                    Selection = false;
                    Tooltip.GetComponent<Animator>().Play(AnimationManager.InteractPrompt_FadeOut, -1, 0f);
                }
            }
        }
        else
        {
            if (!Selection)
            {
                Selection = true;
                Fade = true;
                Tooltip.GetComponent<Animator>().Play(AnimationManager.InteractPrompt_FadeIn, -1, 0f);
                StartCoroutine("WaitForFadeOut");
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(LastRaycastedObject == RaycastObject.Interactable)
            {
                //LastInteractedObject = TargetObject;
                TargetObject.GetComponent<InteractiveObject>().ExecuteInteractiveAction();
            }
            else if(LastRaycastedObject == RaycastObject.Pickupable)
            {
                //Inventory
                AudioManager.GlobalSFXManager.PlaySFX(AudioManager.GlobalSFXManager.ItemSound, null);
                InventoryManager.TheInventory.AddItem(TargetObject.GetComponent<InventoryPickup>().AssociatedItem);
                TargetObject.SetActive(false);
            }
            else if (LastRaycastedObject == RaycastObject.Document)
            {
                //if (UIManager.TheUI.AreAnyUIsActive()) return;

                Debug.Log("READED");
                DocumentManager.TheManager.OpenDocumentPannel(TargetObject.GetComponent<DocumentScript>());
            }
        }

        //Physical Object Manipulation
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //If I am NOT holding something
            if (!PhysicalObject)
            {
                if(TargetObject)
                {
                    if(LastRaycastedObject == RaycastObject.Physical)
                    {
                        PhysicalObject = TargetObject;
                        PhysicalObject.GetComponent<Collider>().enabled = false;
                        PhysicalObject.GetComponent<Rigidbody>().isKinematic = true;
                        PhysicalObject.GetComponent<Rigidbody>().useGravity = false;
                    }
                    
                }
            }
            //If I AM holding something
            else
            {
                PhysicalObject.GetComponent<Collider>().enabled = true;
                PhysicalObject.GetComponent<Rigidbody>().isKinematic = false;
                PhysicalObject.GetComponent<Rigidbody>().useGravity = true;
                PhysicalObject.GetComponent<Rigidbody>().velocity = (transform.position + (transform.forward * ThrowStrength)) - transform.position;
                PhysicalObject = null;
            }
        }

        if(PhysicalObject)
        {
            PhysicalObject.transform.position = Vector3.Lerp(PhysicalObject.transform.localPosition, HoldOrigin.transform.position, 0.95f);
        }
    }

    public IEnumerator WaitForFadeOut()
    {
        yield return new WaitForSeconds(0.1f);
        Tooltip.text = "";
    }
}
