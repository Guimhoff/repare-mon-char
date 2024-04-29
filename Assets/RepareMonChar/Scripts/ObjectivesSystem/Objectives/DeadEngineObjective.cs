using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEngineObjective : Objective
{
    private DeadEngineLogic engine;

    private void Start()
    {
        engine = GetComponent<DeadEngineLogic>();
    }

    public override bool IsComplete()
    {
        return engine.keyTurned;
    }

}
