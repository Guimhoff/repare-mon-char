using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BatteryLogic : MonoBehaviour
{
    public GameObject crocodileClipA;
    public GameObject crocodileClipB;
    public bool isConnected = false;
    public float distance_toconnect = 0.1f;

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, crocodileClipA.transform.position) < distance_toconnect)
        {
            crocodileClipA.transform.position = transform.position;

            crocodileClipA.transform.rotation = Quaternion.identity;

            isConnected = true;

        }
        else if (Vector3.Distance(transform.position, crocodileClipB.transform.position) < distance_toconnect)
        {
            crocodileClipB.transform.position = transform.position;

            crocodileClipB.transform.rotation = Quaternion.identity;

            isConnected = true;

        } else
        {
            isConnected = false;
        }


    }
}
