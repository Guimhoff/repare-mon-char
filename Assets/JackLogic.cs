using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum JackState
{
    Default,
    Expanded
}

public class JackLogic : MonoBehaviour
{

    public float defaultSize = .1f;
    public float expandedSize = .4f;

    public float deltaToExpand = 20f;

    public JackState state = JackState.Default;
    
    


    public void ToggleJack()
    {
        if (state == JackState.Default)
        {
            state = JackState.Expanded;
            transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            state = JackState.Default;
            transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
    }


    private void FixedUpdate()
    {
        transform.SetLocalScaleY(Mathf.MoveTowards(transform.localScale.y, state == JackState.Default ? defaultSize : expandedSize, deltaToExpand * Time.fixedDeltaTime));
    }
}
