using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Objective: MonoBehaviour
{
    public string text;

    public virtual void SetCurrentObjective() { }
    public virtual void RemoveCurrentObjective() { }
    public abstract bool IsComplete();
}
