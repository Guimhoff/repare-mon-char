using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EngineLogic : MonoBehaviour
{
    public BatteryLogic connector_a;
    public BatteryLogic connector_b;
    public BatteryLogic connector_c;
    public BatteryLogic connector_d;
    public AudioSource audioSource;
     

    private void FixedUpdate()
    {
       
    }

    public void checkEngine()
    {
        bool state = connector_a.isConnected && connector_b.isConnected && connector_c.isConnected && connector_d.isConnected;
        print(state);

        if (state)
        {
            audioSource.Play();
        }
 

    }


}
