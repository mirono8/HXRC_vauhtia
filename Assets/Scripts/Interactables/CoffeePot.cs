using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeePot : Triggerable
{
    public CoffeeCup mugI;
    public MeshRenderer mesh;
    private void Update()
    {
        if (triggered && !mugI.triggered)
        {
            mugI.Filled();
            
        }
    }

    private void OnEnable()
    {
        mesh.enabled = true;
        SoundEffectsCoffee._instance.PotSound(true);
    }
}
