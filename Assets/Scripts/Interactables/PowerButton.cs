using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButton : Triggerable
{
    public MeshRenderer coffee;
    private void OnEnable()
    {
        triggered = true;
        //vihree valo ja sihin‰‰
        SoundEffectsCoffee._instance.BrewSound();
        StartCoroutine(CoffeeDone());

    }

    private IEnumerator CoffeeDone()
    {
        yield return new WaitForSeconds(10);
        coffee.enabled = true;
    }
}
