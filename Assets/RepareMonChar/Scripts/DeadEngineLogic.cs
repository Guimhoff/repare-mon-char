using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;



public class DeadEngineLogic : MonoBehaviour
{
    public EngineLogic engine;

    public AudioSource audioSource;

    public bool keyTurned = false;

    private void FixedUpdate()
    {

    }

    public void checkEngine()
    {
        if (engine.keyTurned)
        {
            audioSource.Play();
            keyTurned = true;
        }
    }


}
