using SerializableCallback;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectivesSystem : MonoBehaviour
{
    public List<Objective> objectives;
    public int currentObjective = 0;

    private void Update()
    {
        if (currentObjective > objectives.Count - 1)
            return;

        if (objectives[currentObjective].IsComplete())
            NextObjective();
    }

    private void NextObjective()
    {
        objectives[currentObjective].RemoveCurrentObjective();
        currentObjective++;
        objectives[currentObjective].SetCurrentObjective();
    }

}
