using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ObjectiveDisplay : MonoBehaviour
{
    public ObjectivesSystem objectivesSystem;
    private TextMeshProUGUI textMesh;

    public GameObject buttonOn;
    public GameObject buttonOff;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = objectivesSystem.GetObjectiveText();

        objectivesSystem.NewObjectiveEvent += HandleNewObjectiveEvent;
        objectivesSystem.HighlightChangeEvent += HandleHighlightChangeEvent;

        UpdateButtonDisplay(objectivesSystem.highlight);
    }

    void HandleNewObjectiveEvent(object sender, NewObjectiveEventArgs e)
    {
        textMesh.text = e.Text;
    }

    void HandleHighlightChangeEvent(object sender, HighlightChangeEventArgs e)
    {
        UpdateButtonDisplay(e.IsOn);
    }

    private void UpdateButtonDisplay(bool isOn)
    {
        if (!objectivesSystem.highlightConfigurable)
            return;

        if (isOn)
        {
            buttonOn.SetActive(true);
            buttonOff.SetActive(false);
        }
        else
        {
            buttonOn.SetActive(false);
            buttonOff.SetActive(true);
        }
    }

    public void ButtonOnAction()
    {
        objectivesSystem.SetHighlightOff();
    }

    public void ButtonOffAction()
    {
        objectivesSystem.SetHighlightOn();
    }
}
