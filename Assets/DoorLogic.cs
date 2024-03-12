using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    public float TargetOne = 0f;
    public float TargetTwo = 75f;
    public float PivotValue = 40f;

    public float LowerLimit = 0f;
    public float UpperLimit = 77f;

    public bool IsOpen = true;

    public bool SpringEnabled = true;

    private HingeJoint hinge;
    private JointSpring Spring = new() { spring = 100f, damper = 50f };
    private Rigidbody rb;

    private void Start()
    {
        hinge = GetComponent<HingeJoint>();
        rb = GetComponent<Rigidbody>();
        hinge.useSpring = SpringEnabled;
        Spring = hinge.spring;
    }

    

    private void FixedUpdate()
    {
        if (!IsOpen)
        {
            return;
        }

        if (hinge.angle < -.1f)
        {
            hinge.limits = new JointLimits { min = 0f, max = 0f };
            IsOpen = false;
            return;
        }

        if (hinge.angle > PivotValue)
        {
            Spring.targetPosition = TargetTwo;
            hinge.spring = Spring;
        }
        else
        {
            Spring.targetPosition = TargetOne;
            hinge.spring = Spring;
        }
    }

    public void OpenDoor()
    {
        if (IsOpen)
        {
            return;
        }

        IsOpen = true;
        hinge.limits = new JointLimits { min = LowerLimit -.5f, max = UpperLimit };
        rb.angularVelocity = hinge.axis * 400f;
    }
}
