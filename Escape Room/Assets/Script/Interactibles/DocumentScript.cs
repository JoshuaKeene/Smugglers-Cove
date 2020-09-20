using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentScript : MonoBehaviour
{
    public Sprite DocSprite;

    public string DocName;

    [TextArea (1,20)]
    public string DocText;

    private GameObject DocCanvas;
    private GameObject DocTextUI;

    private void Start()
    {
        DocCanvas = gameObject.transform.Find("Document Canvas").gameObject;
        DocTextUI = DocCanvas.transform.Find("DocumentText").gameObject;

        DocTextUI.GetComponent<Text>().text = "<size=4>" + DocName + "</size>\n" + DocText;
    }
}
