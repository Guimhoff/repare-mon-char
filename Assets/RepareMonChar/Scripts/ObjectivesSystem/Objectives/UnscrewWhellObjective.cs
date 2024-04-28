using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnscrewWhellObjective : Objective
{
    private WheelSocket wheelSocket;

    private void Start()
    {
        wheelSocket = GetComponent<WheelSocket>();
    }

    public override bool IsComplete()
    {
        return wheelSocket.IsFree();
    }

}
