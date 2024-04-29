using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscrewObjective : Objective
{
    public override bool IsComplete()
    {
        return !GetComponentInParent<ScrewLogic>().IsScrewed;
    }

}

