using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObjective : Objective
{
    private EngineLogic engine;

    private void Start()
    {
        engine = GetComponent<EngineLogic>();
    }

    public override bool IsComplete()
    {
        return engine.keyTurned;
    }

}
