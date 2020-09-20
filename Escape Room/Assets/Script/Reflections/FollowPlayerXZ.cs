using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerXZ : MonoBehaviour
{
    public Transform PlayerPosition;
 
    void Update()
    {
        transform.position = new Vector3(PlayerPosition.position.x, transform.position.y, PlayerPosition.position.z);
    }
}
