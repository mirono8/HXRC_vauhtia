using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTap : Triggerable
{

    public GameObject tap;
    public void OpenWater()
    {
        triggered = true;

        SoundEffectsCoffee._instance.aSource.loop = true;
        SoundEffectsCoffee._instance.TapSound(false);
        

        //vesijuttuja tähän

    }

    public void CloseWater()
    {
        triggered = false;

        
        SoundEffectsCoffee._instance.aSource.Stop();
        SoundEffectsCoffee._instance.aSource.clip = null;
        SoundEffectsCoffee._instance.aSource.loop = false;
        //vesijuttuja tähän

    }

    private void OnEnable()
    {
        OpenWater();
    }

    private void OnDisable()
    {
        CloseWater();
    }

    private void Update()
    {
        
    }
}
