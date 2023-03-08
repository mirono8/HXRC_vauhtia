using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundEffectDown : MonoBehaviour
{
    public string soundToPlay;
    private void OnTriggerEnter(Collider other)
    {
        switch (soundToPlay)
        {
            case "suodatinpussi": SoundEffectsCoffee._instance.FilterSound(); break;
            case "kahvikuppi": SoundEffectsCoffee._instance.MugSound(); break;
            case "kahvipannuIn": SoundEffectsCoffee._instance.PotSound(false); break;
            default: break;
        }
    }
}
