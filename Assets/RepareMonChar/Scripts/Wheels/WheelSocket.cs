using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WheelSocket : MonoBehaviour
{
    public float DisabledRadius = .1f;
    public float FullRadius = .4f;

    public float FreeingSpace = .06f;
    public float maxFreeSpace = .1f;

    public float CatchingDistance = .05f;

    private Collider _triggerCollider;

    private void Start()
    {
        _triggerCollider = GetComponent<Collider>();
        
        if(GetComponentInChildren<WheelLogic>() != null)
            _triggerCollider.enabled = false;
        else
            _triggerCollider.enabled = true;
    }

    public float AvailableFreeSpace()
    {
        return IsFree() ? maxFreeSpace : 0;
    }

    public bool IsFree()
    {
        foreach (Transform child in transform)
        {
            if (child.name != "Screw")
            {
                continue;
            }

            ScrewLogic screw = child.GetComponent<ScrewLogic>();

            if (screw.IsScrewed)
            {
                return false;
            }
        }

        return true;
    }

    private void OnTriggerStay(Collider collision)
    {
        
        if (collision.gameObject.GetComponent<WheelLogic>() != null)
        {
            WheelLogic wheelLogic = collision.gameObject.GetComponent<WheelLogic>();
            if (wheelLogic.Dismounted && Vector3.Distance(collision.transform.position, transform.position) < CatchingDistance)
            {
                wheelLogic.Mount(gameObject);
                Mount();
            }
        }
    }

    private void DeactivateScrew()
    {
        foreach (Transform child in transform)
        {
            if (child.name != "Screw")
            {
                continue;
            }

            child.GetComponent<CapsuleCollider>().enabled = false;
        }
    }

    private void ActivateScrew()
    {
        foreach (Transform child in transform)
        {
            if (child.name != "Screw")
            {
                continue;
            }

            child.GetComponent<CapsuleCollider>().enabled = true;
        }
    }

    public void Dismount()
    {
        WheelCollider wheelCollider = GetComponent<WorldPosFromWheelCollider>().WheelCollider;
        wheelCollider.radius = DisabledRadius;
        _triggerCollider.enabled = true;
        DeactivateScrew();
    }

    public void Mount()
    {
        WheelCollider wheelCollider = GetComponent<WorldPosFromWheelCollider>().WheelCollider;
        wheelCollider.radius = FullRadius;
        _triggerCollider.enabled = false;
        ActivateScrew();
    }
}
