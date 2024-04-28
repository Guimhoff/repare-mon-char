using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class ObjectiveDisplay : MonoBehaviour
{
    public GameManagementSystem GMSystem;
    private TextMeshProUGUI textMesh;

    public GameObject buttonOn;
    public GameObject buttonOff;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = GMSystem.GetObjectiveText();

        GMSystem.NewObjectiveEvent += HandleNewObjectiveEvent;
        GMSystem.HighlightChangeEvent += HandleHighlightChangeEvent;

        UpdateButtonDisplay(GMSystem.highlight);
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
        if (!GMSystem.highlightConfigurable)
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
        GMSystem.SetHighlightOff();
    }

    public void ButtonOffAction()
    {
        GMSystem.SetHighlightOn();
    }

    public void MenuButtonAction()
    {
        GMSystem.LoadMenuScene();
    }

    public void ResetButtonAction()
    {
        GMSystem.ReloadScene();
    }

}
