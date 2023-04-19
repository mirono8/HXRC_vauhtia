using Photon.Voice;
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
        //vesijuttuja tähän

    }

    public void CloseWater()
    {
        triggered = false;
        var source = SoundEffectsCoffee._instance.sources.Find(x => x.sourceName == "Tap");


        source.source.loop = false;
        source.source.Stop();
        source.source.clip = SoundEffectsCoffee._instance.tap[3];
        source.source.PlayOneShot(source.source.clip);

        source.source.clip = null;
        source.source.loop = false;
        walter.SetActive(false);
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
