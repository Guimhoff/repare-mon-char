using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableObjective : Objective
{
    public EngineLogic engine;

    private void Start()
    {
    }

    public override bool IsComplete()
    {
        return engine.state_goodConnection;
    }

}
