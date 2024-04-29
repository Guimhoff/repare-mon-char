using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class ScrewLogic : MonoBehaviour
{
    private bool _isScrewed = true;

    public bool IsScrewed = true;


    private void FixedUpdate()
    {
        if (IsScrewed != _isScrewed)
        {
            _isScrewed = IsScrewed;
            if (IsScrewed)
            {
                transform.Find("Bolt").gameObject.SetActive(true);
            }
            else
            {
                transform.Find("Bolt").gameObject.SetActive(false);
            }
        }
    }

    public void ToggleScrew()
    {
        IsScrewed = !IsScrewed;
    }


}
