using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentPlayerToLift : MonoBehaviour
{
    public GameObject Lift;
    public GameObject Player;
    public GameObject RopeAnchor;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RopeAnchor.GetComponent<MeshRenderer>().enabled == false)
        {
            print("PARENT!");
            Player.transform.parent = Lift.transform;
            Invoke("Unparent", 3);
        }
    }

    public void Unparent()
    {
        print("UNPARENT");
        Player.transform.parent = null;
    }

}
