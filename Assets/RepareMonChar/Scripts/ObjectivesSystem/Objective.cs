using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.XR.CoreUtils;
using UnityEngine;

public abstract class Objective: MonoBehaviour
{
    public string text;

    public virtual void SetCurrentObjective(ObjectivesSystem os)
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer == null)
            return;
        
        renderer.AddMaterial(os.highlighted);
    }

    public virtual void RemoveCurrentObjective(ObjectivesSystem os)
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer == null)
            return;

        List<Material> mats = renderer.materials.ToList();
        
        foreach (Material mat in mats)
        {
            if (mat.name == os.highlighted.name + " (Instance)") // Horrible
            {
                mats.Remove(mat);
                break;
            }
        }

        renderer.materials = mats.ToArray();
    }
    public abstract bool IsComplete();
}
