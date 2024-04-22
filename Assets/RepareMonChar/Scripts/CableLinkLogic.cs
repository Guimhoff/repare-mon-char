using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableLinkLogic : MonoBehaviour
{
    public GameObject objectA; // GameObject associ� � ce maillon du c�ble
    public GameObject objectB; // GameObject associ� au maillon suivant dans le c�ble

    public float springForce = 1f; // Force du ressort
    public float springDamping = 0.1f; // Amortissement du ressort

    private Rigidbody rb; // Rigidbody attach� � ce maillon du c�ble

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // R�cup�rer le Rigidbody attach� � ce maillon du c�ble
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
            // Calculer la direction et la force de ressort vers l'objet B (similaire � A)
            Vector3 springDirectionB = objectB.transform.position - transform.position;
            Vector3 springForceB = springForce * springDirectionB;

            // Appliquer la force de ressort sur l'objet B
            rb.AddForce(springForceB * Time.fixedDeltaTime);

            // Appliquer un amortissement au ressort (similaire � A)
            Vector3 objectBVelocity = rb.velocity;
            rb.AddForce(-springDamping * objectBVelocity * Time.fixedDeltaTime);
        }
    }
}
