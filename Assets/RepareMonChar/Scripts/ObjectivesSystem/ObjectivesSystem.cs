using SerializableCallback;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

[ExecuteInEditMode]
public class ObjectivesSystem : MonoBehaviour
{
    protected static ObjectivesSystem objectivesSystem;

    public static void Create()
    {
        if (objectivesSystem != null)
        {
            Debug.LogError("An objectives system already exists");
            return;
        }

        var gameController = new GameObject();
        gameController.name = "Game Controller";
        gameController.transform.SetAsFirstSibling();
        objectivesSystem = gameController.AddComponent<ObjectivesSystem>();
    }

    public static ObjectivesSystem Get()
    {
        if (objectivesSystem.IsDestroyed())
            objectivesSystem = null;

        return objectivesSystem;
    }

    public List<Objective> objectives = new();

    public void NewObjective()
    {
        objectives.Add(new());
    }





    private void Update()
    {
        //foreach (Objectives objective in objectives)
        //{
        //    objective.Update();
        //}
    }

}
