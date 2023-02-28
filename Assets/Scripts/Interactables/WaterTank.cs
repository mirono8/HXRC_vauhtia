using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTank : Triggerable
{
    private void Update()
    {
        if(triggered)
        {
            Debug.Log("Tank filled");
            triggered= false;
        }
    }
}
