using SerializableCallback;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public enum GameMode
{
    Freeplay,
    StepByStep,
    timed,
}

public class GameManagementSystem : MonoBehaviour
{
    public GameMode gameMode;

    public string loadingText;

    [Header("Freeplay")]

    [Header("Step by step")]

    public string objectivesCompleted;

    public List<Objective> objectives;
    public int currentObjective = -1;

    public bool highlightConfigurable;
    public bool highlight;
    public Material highlighted;

    //[Header("Timed")]


    private void Update()
    {
        if (currentObjective > objectives.Count - 1)
            return;

        if (currentObjective < 0 || objectives[currentObjective].IsComplete())
            NextObjective();
    }

    private void NextObjective()
    {
        if (currentObjective >= 0)
            objectives[currentObjective].RemoveCurrentObjective(this);
        currentObjective++;
        RaiseNewObjectiveEvent();

        if (currentObjective >= objectives.Count)
            return;

        if (highlight)
            objectives[currentObjective].SetCurrentObjective(this);
    }

    // Objectives display

    public delegate void NewObjectiveEventHandler(object sender, NewObjectiveEventArgs e);

    public event NewObjectiveEventHandler NewObjectiveEvent;

    protected virtual void RaiseNewObjectiveEvent()
    {
        NewObjectiveEvent?.Invoke(this, new NewObjectiveEventArgs(GetObjectiveText()));
    }

    public string GetObjectiveText()
    {
        if (currentObjective < 0)
            return loadingText;

        if (currentObjective >= objectives.Count)
            return objectivesCompleted;

        return objectives[currentObjective].text;
    }

    // Highlight config

    public void SetHighlightOn()
    {
        if (currentObjective >= 0 && currentObjective < objectives.Count)
            objectives[currentObjective].SetCurrentObjective(this);

        highlight = true;
        RaiseHighlightChangeEvent();
    }

    public void SetHighlightOff()
    {
        if (currentObjective >= 0 && currentObjective < objectives.Count)
            objectives[currentObjective].RemoveCurrentObjective(this);

        highlight = false;
        RaiseHighlightChangeEvent();
    }

    public delegate void HighlightChangeEventHandler(object sender, HighlightChangeEventArgs e);

    public event HighlightChangeEventHandler HighlightChangeEvent;

    protected virtual void RaiseHighlightChangeEvent()
    {
        HighlightChangeEvent?.Invoke(this, new HighlightChangeEventArgs(highlight));
    }

    // Timed



    // Scenes
    
    public void LoadMenuScene()
    {
        // TODO
    }

    public void ReloadScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        StartCoroutine(LoadScene(currentSceneName));
    }

    IEnumerator LoadScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
