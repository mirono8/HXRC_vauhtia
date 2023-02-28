using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCan : Triggerable
{
    public Triggerable pourHere;

    private void OnEnable()
    {
        Debug.Log("water can full ");
    }

    private void OnDisable()
    {
        pourHere.triggered = true;

        Debug.Log("water can empty");
    }
}
