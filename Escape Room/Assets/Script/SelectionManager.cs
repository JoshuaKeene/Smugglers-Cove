using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    public Material highlightMaterial;
    public Material defaultMaterial;
    public GameObject FPSCharacter;
    public GameObject InteractPrompt;

    private Transform _selection;

    // Update is called once per frame
    void Update()
    {
        if (_selection != null)
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            InteractPrompt.SetActive(false);
            _selection = null;
        }
        
        var ray = FPSCharacter.GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3f))
        {
            //Debug.Log("HIT!");
            var selection = hit.transform;
            if (selection.CompareTag("Selectable"))
            {
                //Debug.Log("SELECTABLE!");
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    //Debug.Log("MATCHANGE!");
                    InteractPrompt.SetActive(true);
                    selectionRenderer.material = highlightMaterial;
                }

                _selection = selection;
            }
        }
    }
}
