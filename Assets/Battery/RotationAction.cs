//Susing System.Collections;
//using System.Collections.Generic;

using UnityEngine.Events;
using UnityEngine;
using System;

public class RotationAction : MonoBehaviour
{
    public bool isDoorOpen = false;
    public float doorOpenAngle = 90f; // Angle d'ouverture de la porte
    public float doorCloseAngle = 0f; // Angle de fermeture de la porte

    // Appelé lorsque l'interaction VR se produit (par exemple, lorsque le joueur saisit la poignée de la porte)
    public void HandleVRInteraction()
    {
        int a = 0;
        //isDoorOpen = !isDoorOpen;
        //if (isDoorOpen)
        //{
        //    transform.rotation = Quaternion.Euler(0f, doorOpenAngle, 0f);
        //}
        //else
        //{
        //    transform.rotation = Quaternion.Euler(0f, doorCloseAngle, 0f);
        //}
    }
}
