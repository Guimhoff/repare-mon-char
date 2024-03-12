using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class DamagedWheel : MonoBehaviour
{

    public Collider wheelCollider;
    public GameObject ground;
    public GameObject wheel;



    public void DismountWheel()
    {
        if (!wheelCollider.bounds.Intersects(ground.transform.GetComponent<Collider>().bounds))
        {
            foreach (Transform child in transform.parent)
            {
                if (child.name != "Screw")
                {
                    continue;
                }

                ScrewLogic screw = child.GetComponent<ScrewLogic>();

                if (screw.IsScrewed)
                {
                    return;
                }
            }

            Instantiate(wheel, transform.position, transform.rotation);
            gameObject.SetActive(false);

            foreach (Transform child in transform.parent)
            {
                if (child.name != "Screw")
                {
                    continue;
                }

                child.GetComponent<CapsuleCollider>().enabled = false;
            }
        }
    }
}
