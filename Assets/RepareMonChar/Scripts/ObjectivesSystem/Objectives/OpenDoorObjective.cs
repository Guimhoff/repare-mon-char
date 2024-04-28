using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorObjective : Objective
{
    private DoorLogic door;

    private void Start()
    {
        door = GetComponent<DoorLogic>();
    }

    public override bool IsComplete()
    {
        return door.IsOpen;
    }

}
