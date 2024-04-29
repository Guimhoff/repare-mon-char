using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableLinkLogic : MonoBehaviour
{
    public GameObject objectA; // GameObject associ� � ce maillon du c�ble
    public GameObject objectB; // GameObject associ� au maillon suivant dans le c�ble

    public float springForce = 1f; // Force du ressort
    public float springDamping = 0.1f; // Amortissement du ressort
    public float maxSpeed = 3f; // Limite de vitesse maximale
    public float distance = 0.25f;

    private Rigidbody rb; // Rigidbody attach� � ce maillon du c�ble
    private SpringJoint springJoint; // SpringJoint attach� � ce maillon du c�ble

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // R�cup�rer le Rigidbody attach� � ce maillon du c�ble

        /*if (objectA)
        {
            // Ajouter un SpringJoint et le lier � l'objet A
            springJoint = gameObject.AddComponent<SpringJoint>();
            springJoint.connectedBody = objectA.GetComponent<Rigidbody>();
            springJoint.spring = springForce;
            springJoint.damper = springDamping;
        }*/
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // V�rifier si la vitesse de l'objet d�passe la limite maximale
        if (rb.velocity.magnitude > maxSpeed)
        {
            // Calculer la force n�cessaire pour ramener l'objet en dessous de la limite de vitesse
            Vector3 brakeForce = -rb.velocity.normalized * (rb.velocity.magnitude - maxSpeed);

            // Appliquer la force de freinage � l'objet
            rb.AddForce(brakeForce, ForceMode.Acceleration);
        }
        if (objectA)
        {
            // Calculer la direction de A � cet objet
            Vector3 directionToA = objectA.transform.position - transform.position;

            // Normaliser la direction et la multiplier par la distance fixe pour obtenir la position cible
            Vector3 targetPosition = objectA.transform.position - directionToA.normalized * distance;

            // Calculer la direction vers la position cible
            Vector3 springDirectionA = targetPosition - transform.position;

            // Calculer la force de ressort vers la position cible
            Vector3 springForceA = springForce * springDirectionA;

            // Appliquer la force de ressort sur l'objet A
             rb.AddForce(springForceA * Time.fixedDeltaTime);

            // Appliquer un amortissement au ressort
             Vector3 objectAVelocity = rb.velocity;
             rb.AddForce(-springDamping * objectAVelocity * Time.fixedDeltaTime);
        }

        if (objectB)
        {
            // Calculer la direction de B � cet objet
            Vector3 directionToB = objectB.transform.position - transform.position;

            // Normaliser la direction et la multiplier par la distance fixe pour obtenir la position cible
            Vector3 targetPosition = objectB.transform.position - directionToB.normalized * distance;

            // Calculer la direction vers la position cible
            Vector3 springDirectionB = targetPosition - transform.position;

            // Calculer la force de ressort vers la position cible
            Vector3 springForceB = springForce * springDirectionB;

            // Appliquer la force de ressort sur l'objet B
            rb.AddForce(springForceB * Time.fixedDeltaTime);

            // Appliquer un amortissement au ressort
            Vector3 objectBVelocity = rb.velocity;
            rb.AddForce(-springDamping * objectBVelocity * Time.fixedDeltaTime);
        }
    }
}
