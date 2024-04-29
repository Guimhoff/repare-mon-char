using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewObjective : Objective
{
    public override bool IsComplete()
    {
        return GetComponentInParent<ScrewLogic>().IsScrewed;
    }

}

