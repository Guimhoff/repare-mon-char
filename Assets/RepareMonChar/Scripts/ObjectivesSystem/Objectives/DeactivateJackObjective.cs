using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateJackObjective : Objective
{
    public override bool IsComplete()
    {
        return transform.parent.GetComponent<JackLogic>().state == JackState.Default;
    }

}
