using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeCup : Triggerable
{
    public void Filled()
    {
        Debug.Log("Mug filled");
        GetComponent<MeshRenderer>().enabled = true;
        triggered = true;
    }

    private void OnEnable()
    {
    }
}
