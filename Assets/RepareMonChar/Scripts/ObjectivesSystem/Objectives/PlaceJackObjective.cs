using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceJackObjective : Objective
{
    public GameObject Jack;
    public float distance;

    public override bool IsComplete()
    {
        return Vector3.Distance(Jack.transform.position, transform.position) < distance;
    }

}
