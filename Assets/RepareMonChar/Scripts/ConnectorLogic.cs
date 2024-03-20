using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ConnectorLogic : MonoBehaviour
{


    public char color;
    public bool isConnected = false;
    public char clipColor = '\0';
    public float distance_toconnect = 0.1f;
    private int index = -1;

    public ClipLogic[] crocodileClipArray = new ClipLogic[4];

    public void fixCrocodileClip(ClipLogic crocodileClip)
    {
        crocodileClip.transform.position = transform.position;

        crocodileClip.transform.rotation = Quaternion.identity;

        clipColor = crocodileClip.color;

        isConnected = true;
    }

    private void FixedUpdate()
    {
        int i = 0;

        // bool needVerif = !(Vector3.Distance(transform.position, crocodileClipArray[i].transform.position) < distance_toconnect);

        if (index == -1){
            while (i < 4)
            {
                if ((Vector3.Distance(transform.position, crocodileClipArray[i].transform.position) < distance_toconnect))
                {
                    fixCrocodileClip(crocodileClipArray[i]);

                    index = i;

                    break;

                }

                i = i + 1;


            }
        }
        else if (!(Vector3.Distance(transform.position, crocodileClipArray[i].transform.position) < distance_toconnect)) { 
            index = -1;
            isConnected = false;
            clipColor = '\0';
        }
    }

}

