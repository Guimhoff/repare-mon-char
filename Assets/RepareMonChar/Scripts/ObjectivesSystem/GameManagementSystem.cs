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
    Timed,
}

public enum TimerState
{
    Zeroed,
    Started,
    Finished,
}

public class GameManagementSystem : MonoBehaviour
{
    public GameMode gameMode;

    public string loadingText;

    [Header("Freeplay")]
    public string freeplayText;

    [Header("Step by step")]

    public string objectivesCompleted;

    public List<Objective> objectives;
    public int currentObjective = -1;

    public bool highlightConfigurable;
    public bool highlight;
    public Material highlighted;

    [Header("Timed")]

    public List<GameObject> FreezedObjects;
    public TimerState TimerState = TimerState.Zeroed;
    private double startTime = 0;
    private TimeSpan time = TimeSpan.Zero;

    private void Start()
    {
        if (gameMode != GameMode.StepByStep)
        {
            highlightConfigurable = false;
            highlight = false;
            RaiseHighlightChangeEvent();
        }

        if (gameMode == GameMode.Timed)
        {
            foreach (GameObject gameObject in FreezedObjects)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (gameMode == GameMode.Timed && TimerState != TimerState.Started)
            return;

        if (currentObjective > objectives.Count - 1)
            return;

        if (currentObjective < 0 || objectives[currentObjective].IsComplete())
            NextObjective();

        if (gameMode == GameMode.Timed)
        {
            if (currentObjective >= objectives.Count)
                EndTimer();
            RaiseNewDisplayEvent();
        }
    }

    private void NextObjective()
    {
        if (currentObjective >= 0)
            objectives[currentObjective].RemoveCurrentObjective(this);
        currentObjective++;
        RaiseNewDisplayEvent();

        if (currentObjective >= objectives.Count)
            return;

        if (highlight)
            objectives[currentObjective].SetCurrentObjective(this);
    }

    public string GetDisplayText()
    {
        if (gameMode == GameMode.Freeplay)
            return freeplayText;

        if (gameMode == GameMode.StepByStep)
            return GetObjectiveText();

        if (gameMode == GameMode.Timed)
            return GetTimedText();

        return "";
    }

    // Objectives display

    public delegate void NewDisplayEventHandler(object sender, NewDisplayEventArgs e);

    public event NewDisplayEventHandler NewDisplayEvent;

    protected virtual void RaiseNewDisplayEvent()
    {
        NewDisplayEvent?.Invoke(this, new NewDisplayEventArgs(GetDisplayText()));
    }

    private string GetObjectiveText()
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
        if (!highlightConfigurable)
            return;

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
        HighlightChangeEvent?.Invoke(this, new HighlightChangeEventArgs(highlight, highlightConfigurable));
    }

    // Timed

    public void StartTimer()
    {
        if (gameMode != GameMode.Timed)
            return;

        if (TimerState != TimerState.Zeroed)
            return;

        foreach (GameObject gameObject in FreezedObjects)
        {
            gameObject.SetActive(true);
        }

        TimerState = TimerState.Started;
        startTime = Time.realtimeSinceStartupAsDouble;
        RaiseTimerStateEvent();
    }

    private string GetTimedText()
    {
        if (TimerState == TimerState.Zeroed)
            return "";

        if (TimerState == TimerState.Started)
        {
            TimeSpan currentTime = TimeSpan.FromSeconds(Time.realtimeSinceStartupAsDouble - startTime);

            return FormatTime(currentTime);
        }

        return "Temps réalisé : " + FormatTime(time);
    }

    private string FormatTime(TimeSpan timeSpan)
    {
        return Mathf.Floor((float)timeSpan.TotalMinutes).ToString() + ":" + timeSpan.Seconds.ToString() + "." + timeSpan.Milliseconds.ToString();
    }

    public void EndTimer()
    {
        if (TimerState != TimerState.Started)
            return;

        TimerState = TimerState.Finished;
        time = TimeSpan.FromSeconds(Time.realtimeSinceStartupAsDouble - startTime);
    }

    public delegate void TimerStateEventHandler(object sender, TimerStateEventArgs e);

    public event TimerStateEventHandler TimerStateEvent;

    protected virtual void RaiseTimerStateEvent()
    {
        TimerStateEvent?.Invoke(this, new TimerStateEventArgs(TimerState));
    }

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
