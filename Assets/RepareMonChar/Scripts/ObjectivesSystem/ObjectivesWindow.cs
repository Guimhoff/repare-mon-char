using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


[Serializable]
[ExecuteInEditMode]
public class ObjectivesWindow: EditorWindow
{
    public Component component;
    public List<PropertyInfo> properties;
    string[] options = { "Rigidbody", "Box Collider", "Sphere Collider" };
    int index = 0;


    [MenuItem("GameController/Objectives")]
    static void Init()
    {
        var window = GetWindow<ObjectivesWindow>();
        window.position = new Rect(0, 0, 180, 80);
        window.Show();
    }

    void OnGUI()
    {
        var objectiveSystem = ObjectivesSystem.Get();
        if (objectiveSystem == null)
        {
            if (GUI.Button(new Rect(0, 25, position.width, position.width/5), "Create Objectives System"))
                ObjectivesSystem.Create();
            return;
        }

        float cursor = 25;

        foreach (Objective objective in objectiveSystem.objectives)
        {
            DisplayObjective(objective, new Rect(0, cursor, position.width, position.width / 5));
            cursor += position.width / 5;
        }

        if (GUI.Button(new Rect(0, cursor, position.width, position.width / 5), "Create new objective"))
            objectiveSystem.NewObjective();


        //index = EditorGUI.Popup(
        //    new Rect(0, 0, position.width, 20),
        //    "Component:",
        //    index,
        //    options);

    }

    private void DisplayObjective(Objective objective, Rect position)
    {
        objective.component = (Component)EditorGUI.ObjectField(position, "Component", objective.component, typeof(Component), true);
    }
}
