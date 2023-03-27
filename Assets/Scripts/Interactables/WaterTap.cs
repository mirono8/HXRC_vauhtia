using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTap : Triggerable
{

    public GameObject tap;

    public GameObject walter;
    public void OpenWater()
    {
        triggered = true;

        //SoundEffectsCoffee._instance.aSource.loop = true;
        SoundEffectsCoffee._instance.TapSound(false);

        walter.SetActive(true);
        //vesijuttuja t�h�n

    }

    public void CloseWater()
    {
        triggered = false;

        SoundEffectsCoffee._instance.aSource.loop = false;
        SoundEffectsCoffee._instance.aSource.Stop();
        SoundEffectsCoffee._instance.aSource.clip = SoundEffectsCoffee._instance.tap[3];
        SoundEffectsCoffee._instance.aSource.PlayOneShot(SoundEffectsCoffee._instance.aSource.clip);

        SoundEffectsCoffee._instance.aSource.clip = null;
        SoundEffectsCoffee._instance.aSource.loop = false;
        walter.SetActive(false);
        //vesijuttuja t�h�n

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
