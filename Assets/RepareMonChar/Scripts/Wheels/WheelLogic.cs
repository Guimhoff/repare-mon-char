using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WheelLogic : MonoBehaviour
{
    public float Radius = .4f;

    public bool Dismounted;

    public MeshCollider attachedCollider;
    public MeshCollider detachedCollider;

    public WheelSocket Socket;
    
    public Vector3 Axis = new Vector3(1, 0, 0);

    private void Start()
    {
        if (Socket == null && transform.parent != null)
            Socket = transform.parent.GetComponent<WheelSocket>();

        if (!Dismounted)
            Socket.Mount(Radius);
    }

    private void FixedUpdate()
    {
        if (Dismounted)
            GetComponent<Rigidbody>().isKinematic = false;
        else
            MountedFixedUpdate();

    }

    private void MountedFixedUpdate()
    {
        GetComponent<Rigidbody>().isKinematic = true;
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
        Socket.Dismount();
        Socket = null;
        transform.parent = null;
        GetComponent<XRGrabInteractable>().trackRotation = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;

        detachedCollider.enabled = true;
        attachedCollider.enabled = false;
    }

    public void Mount(GameObject newSocket)
    {
        print("Mounting");

        Dismounted = false;
        transform.SetParent(newSocket.transform);

        transform.rotation = newSocket.transform.rotation;

        Socket = newSocket.GetComponent<WheelSocket>();
        Socket.Mount(Radius);

        transform.position = newSocket.transform.position;
        //transform.localPosition = Vector3.zero;

        GetComponent<XRGrabInteractable>().trackRotation = false;

        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        detachedCollider.enabled = false;
        attachedCollider.enabled = true;
    }
}
