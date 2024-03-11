using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReparedWheelLogic : MonoBehaviour
{
    public GameObject wheel;

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, wheel.transform.position) < 0.1f)
        {
            wheel.SetActive(false);
            GetComponent<MeshCollider>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
            transform.Find("WheelSubPart").gameObject.SetActive(true);

            foreach (Transform child in transform.parent)
            {
                if (child.name != "Screw")
                {
                    continue;
                }

                child.GetComponent<CapsuleCollider>().enabled = true;
            }
        }
    }
}
