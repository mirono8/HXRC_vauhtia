using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeePot : Triggerable
{
    public CoffeeCup mugI;
    private void Update()
    {
        if (triggered && !mugI.triggered)
        {
            mugI.Filled();
            
        }
    }

    private void OnEnable()
    {
        
        SoundEffectsCoffee._instance.PotSound(true);
    }
}
