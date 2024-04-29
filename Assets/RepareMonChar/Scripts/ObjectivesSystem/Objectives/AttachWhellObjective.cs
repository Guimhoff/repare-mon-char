using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachWhellObjective : Objective
{

    public override bool IsComplete()
    {
        return !GetComponent<WheelLogic>().Dismounted;
    }

}
