using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButton : Triggerable
{
    private void OnEnable()
    {
        triggered = true;
        //vihree valo ja sihin‰‰
        SoundEffectsCoffee._instance.BrewSound();
    }
}
