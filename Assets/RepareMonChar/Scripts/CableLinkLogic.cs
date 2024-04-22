using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableLinkLogic : MonoBehaviour
{
    public GameObject objectA; // GameObject associé à ce maillon du câble
    public GameObject objectB; // GameObject associé au maillon suivant dans le câble

    public float springForce = 1f; // Force du ressort
    public float springDamping = 0.1f; // Amortissement du ressort

    private Rigidbody rb; // Rigidbody attaché à ce maillon du câble

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Récupérer le Rigidbody attaché à ce maillon du câble
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (objectA)
        {
            // Calculer la direction et la force de ressort vers l'objet A
            Vector3 springDirectionA = objectA.transform.position - transform.position;
            Vector3 springForceA = springForce * springDirectionA;

            // Appliquer la force de ressort sur l'objet A
            rb.AddForce(springForceA * Time.fixedDeltaTime);

            // Appliquer un amortissement au ressort
            Vector3 objectAVelocity = rb.velocity;
            rb.AddForce(-springDamping * objectAVelocity * Time.fixedDeltaTime);
        }

        if (objectB)
        {
            // Calculer la direction et la force de ressort vers l'objet B (similaire à A)
            Vector3 springDirectionB = objectB.transform.position - transform.position;
            Vector3 springForceB = springForce * springDirectionB;

            // Appliquer la force de ressort sur l'objet B
            rb.AddForce(springForceB * Time.fixedDeltaTime);

            // Appliquer un amortissement au ressort (similaire à A)
            Vector3 objectBVelocity = rb.velocity;
            rb.AddForce(-springDamping * objectBVelocity * Time.fixedDeltaTime);
        }
    }
}
