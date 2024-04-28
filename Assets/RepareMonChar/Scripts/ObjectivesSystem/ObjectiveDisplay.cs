using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class ObjectiveDisplay : MonoBehaviour
{
    public GameManagementSystem GMSystem;
    private TextMeshProUGUI textMesh;

    public GameObject buttonOn;
    public GameObject buttonOff;
    public GameObject startButton;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = GMSystem.GetDisplayText();

        GMSystem.NewDisplayEvent += HandleNewObjectiveEvent;
        GMSystem.HighlightChangeEvent += HandleHighlightChangeEvent;
        GMSystem.TimerStateEvent += HandleTimerStateEvent;

        UpdateButtonDisplay(GMSystem.highlight, GMSystem.highlightConfigurable);
        UpdateStartButtonDisplay(GMSystem.TimerState);
    }

    void HandleNewObjectiveEvent(object sender, NewDisplayEventArgs e)
    {
        textMesh.text = e.Text;
    }

    void HandleHighlightChangeEvent(object sender, HighlightChangeEventArgs e)
    {
        UpdateButtonDisplay(e.IsOn, e.IsConfigurable);
    }

    private void UpdateButtonDisplay(bool isOn, bool isConfigurable)
    {
        if (!isConfigurable)
        {
            buttonOn.SetActive(false);
            buttonOff.SetActive(false);
            return;
        }

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

    void HandleTimerStateEvent(object sender, TimerStateEventArgs e)
    {
        UpdateStartButtonDisplay(e.TimerState);
    }

    private void UpdateStartButtonDisplay(TimerState timerState)
    {
        if (timerState == TimerState.Zeroed)
        {
            startButton.SetActive(true);
            return;
        }

        startButton.SetActive(false);
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

    public void StartButtonAction()
    {
        GMSystem.StartTimer();
    }
}
