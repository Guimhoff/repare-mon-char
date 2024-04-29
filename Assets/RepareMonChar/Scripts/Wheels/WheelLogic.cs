using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WheelLogic : MonoBehaviour
{
    public float Radius = .4f;

    public bool Dismounted;
    public FixedJoint joint;

    public MeshCollider attachedCollider;
    public MeshCollider detachedCollider;

    public WheelSocket Socket;
    
    public Vector3 Axis = new Vector3(1, 0, 0);

    private void Start()
    {
        if (joint == null)
            joint = GetComponent<FixedJoint>();

        if (Socket == null && transform.parent != null)
            Socket = transform.parent.GetComponent<WheelSocket>();

        if (!Dismounted)
            Socket.Mount(Radius);
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
        float freeSpace = Socket.AvailableFreeSpace();

        if (freeSpace == 0)
            GetComponent<XRGrabInteractable>().enabled = false;
        else
            GetComponent<XRGrabInteractable>().enabled = true;
    }

    public void GrabWheel()
    {
        float freeSpace = Socket.AvailableFreeSpace();

        if (freeSpace > 0)
        {
            Dismount();
        }
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

        detachedCollider.enabled = true;
        attachedCollider.enabled = false;
    }

    public void Mount(GameObject newSocket)
    {
        print("Mounting");

        Dismounted = false;
        transform.SetParent(newSocket.transform);

        transform.rotation = newSocket.transform.rotation;
        Vector3 tempPos = transform.localPosition;

        Socket = newSocket.GetComponent<WheelSocket>();
        Socket.Mount(Radius);
        transform.position = newSocket.transform.position;
        //transform.localPosition = Vector3.zero;
        GetComponent<XRGrabInteractable>().trackRotation = false;

        joint = gameObject.AddComponent<FixedJoint>();
        ConfigureJoint(newSocket);

        detachedCollider.enabled = false;
        attachedCollider.enabled = true;
    }

    private void ConfigureJoint(GameObject newSocket)
    {
        joint.connectedBody = newSocket.GetComponent<Rigidbody>();
        joint.massScale = 100;
        joint.connectedMassScale = 1;
    }
}
