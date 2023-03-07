using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCan : Triggerable
{
    public Triggerable pourHere;
    public MeshRenderer mesh;
    private void OnEnable()
    {
        Debug.Log("water can full ");
        mesh.enabled = true;
    }

    private void OnDisable()
    {
        pourHere.triggered = true;
        mesh.enabled = false;
        Debug.Log("water can empty");
    }
}
