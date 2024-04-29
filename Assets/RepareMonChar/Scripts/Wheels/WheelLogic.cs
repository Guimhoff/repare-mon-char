using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WheelLogic : MonoBehaviour
{
    public bool Dismounted;
    public ConfigurableJoint joint;

    public WheelSocket Socket;
    
    public Vector3 Axis = new Vector3(1, 0, 0);

    private void Start()
    {
        if (joint == null)
            joint = GetComponent<ConfigurableJoint>();

        if (Socket == null)
            Socket = transform.parent.GetComponent<WheelSocket>();

        if (Dismounted)
            Dismount();
    }

    private void FixedUpdate()
    {
        if (Dismounted)
            return;
        else
            MountedFixedUpdate();

    }

    private void MountedFixedUpdate()
    {
        if (Socket.AvailableFreeSpace() == 0)
            GetComponent<XRGrabInteractable>().enabled = false;
        else
            GetComponent<XRGrabInteractable>().enabled = true;


        float freeSpace = Socket.AvailableFreeSpace();

        if (Mathf.Abs(freeSpace - joint.linearLimit.limit) > .1f)
        {
            joint.connectedAnchor = new Vector3(freeSpace, 0, 0);
            joint.linearLimit = new SoftJointLimit { limit = freeSpace };
        }

        if (Socket.IsFree() && Vector3.Dot(transform.localPosition - joint.anchor, Axis) > Socket.FreeingSpace)
            Dismount();
    }

    public void Dismount()
    {
        print("Dismounting");

        Dismounted = true;
        Destroy(joint);
        Socket.Dismount();
        Socket = null;
        transform.parent = null;
        GetComponent<XRGrabInteractable>().trackRotation = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void Mount(GameObject newSocket)
    {
        print("Mounting");

        Dismounted = false;
        transform.SetParent(newSocket.transform);

        transform.rotation = newSocket.transform.rotation;
        Vector3 tempPos = transform.localPosition;

        transform.position = newSocket.transform.position;
        Socket = newSocket.GetComponent<WheelSocket>();
        Socket.Mount();
        joint = gameObject.AddComponent<ConfigurableJoint>();
        ConfigureJoint(newSocket);
        joint.connectedBody = newSocket.GetComponent<Rigidbody>();
        // transform.localPosition = tempPos;
        GetComponent<XRGrabInteractable>().trackRotation = false;
    }

    private void ConfigureJoint(GameObject newSocket)
    {
        joint.anchor = Vector3.zero;
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = Vector3.zero;
        joint.axis = Axis;
        joint.secondaryAxis = Vector3.up;
        joint.xMotion = ConfigurableJointMotion.Limited;
        joint.yMotion = ConfigurableJointMotion.Locked;
        joint.zMotion = ConfigurableJointMotion.Locked;
        joint.angularXMotion = ConfigurableJointMotion.Locked;
        joint.angularYMotion = ConfigurableJointMotion.Locked;
        joint.angularZMotion = ConfigurableJointMotion.Locked;
        joint.linearLimit = new SoftJointLimit { limit = 0.1f };

        joint.massScale = 10000f;
        joint.connectedMassScale = 1f;
    }
}
