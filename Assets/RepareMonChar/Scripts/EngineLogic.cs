using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.SceneManagement;



public class EngineLogic : MonoBehaviour
{
    public ConnectorLogic connector_r_1;
    public ConnectorLogic connector_r_2;

    public ConnectorLogic connector_b_1;
    public ConnectorLogic connector_b_2;

    public TextMesh textMesh;

    public AudioSource audioSource;

    bool state_isConnected = false;

    bool state_goodConnection = false; // the connectors are connected to the same colors

    bool state_shortCircuit = false; // short circuit

    bool state_crossedCircuit = false; // crossed circuit

    private void FixedUpdate()
    {
        state_isConnected = connector_r_1.isConnected && connector_r_2.isConnected && connector_b_1.isConnected && connector_b_2.isConnected;

        state_goodConnection = (connector_r_1.clipColor == connector_r_2.clipColor) && state_isConnected; // the connectors are connected to the same colors

        state_shortCircuit = (connector_r_1.isConnected && (connector_r_1.clipColor == connector_b_1.clipColor)) || 
                             (connector_r_2.isConnected && (connector_r_2.clipColor == connector_b_2.clipColor)); // short circuit

        state_crossedCircuit = (connector_r_1.isConnected && (connector_r_1.clipColor == connector_b_2.clipColor)) ||
                               (connector_r_2.isConnected && (connector_r_2.clipColor == connector_b_1.clipColor)); // crossed circuit

        textMesh.text = "is Connected " + state_isConnected + "\n" +
            "good Connection " + state_goodConnection + "\n" +
            "short Circuit " + state_shortCircuit + "\n" +
            "crossed circuit " + state_crossedCircuit;

    }

    public void checkEngine()
    {

        print("All the battery connectors are connected: " + state_isConnected);

        if (state_goodConnection)
        {
            print("The connectors are connected to the same color cables: " + state_goodConnection);
            audioSource.Play();
        } else if (state_shortCircuit)
        {
            print("Short circuited the battery");
        } else if (state_crossedCircuit)
        {
            print("The circuit is crossed beware");
        }
 

    }


}
