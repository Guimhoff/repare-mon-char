using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectiveDisplay : MonoBehaviour
{
    public ObjectivesSystem objectivesSystem;
    private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = objectivesSystem.GetObjectiveText();

        objectivesSystem.NewObjectiveEvent += HandleNewObjectiveEvent;
    }

    void HandleNewObjectiveEvent(object sender, NewObjectiveEventArgs e)
    {
        textMesh.text = e.Text;
    }
}
