using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TaskTargets;

public class SoundEffectsCoffee : MonoBehaviour
{
    
    public AudioSource aiSource;

    public AI mummo;

    public AudioClipsScriptable mug;
    public AudioClipsScriptable pot;
    public AudioClipsScriptable tap;
    public AudioClipsScriptable filter;
    public AudioClipsScriptable pour;
    public AudioClipsScriptable brew;

    private AudioClip clip;

    private string x;

    private string y;

    private bool test = false;

    public static SoundEffectsCoffee _instance { get; private set; }


    [Serializable]
    public class AudioSourcesCoffee
    {
        public string sourceName;
        public AudioSource source;
    }

    public List<AudioSourcesCoffee> sources;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
        
    }
    
    public void BrewSound()
    {
        clip = brew[0];

        var source = sources.Find(x => x.sourceName == "CoffeeMachine");

        source?.source.PlayOneShot(clip);

        if (source == null)
            Debug.Log("no source found");

    }
    public void MugSound()
    {
        clip = mug.PickRandom();
        aiSource.PlayOneShot(clip);
    }

    public void PotSound(bool potOut)
    {
        if (potOut)
        {
            clip = pot[0];
            var source = sources.Find(x => x.sourceName == "CoffeeMachine");

            source?.source.PlayOneShot(clip);
        }
        else
        {
            clip = pot[1];
            var source = sources.Find(x => x.sourceName == "CoffeeMachine");

            source?.source.PlayOneShot(clip);

        }

    }

    public void PourSound(bool water)
    {
        if (water)
        {
            clip = pour[0];
            aiSource.PlayOneShot(clip);
        }
        else
        {
            clip = pour[1];
            aiSource.PlayOneShot(clip);
        }
    }

    public void TapSound(bool jug)
    {
        if (jug)
        {
            clip = tap[0];
            var source = sources.Find(x => x.sourceName == "Tap");

            source?.source.PlayOneShot(clip);
            if (source == null)
                Debug.Log("no source found");
        }
        else
        {
            clip = tap[1];
            //aSource.clip = clip;
            var source = sources.Find(x => x.sourceName == "Tap");

            source?.source.PlayOneShot(clip);
            if (source == null)
                Debug.Log("no source found");
            clip = tap[2];
            source.source.clip = clip;
            source.source.loop = true;
            source.source.Play();
            
        }
    }

    public void FilterSound()
    {
        clip = filter.PickRandom();
        var source = sources.Find(x => x.sourceName == "CoffeeMachine");

        source?.source.PlayOneShot(clip);
    }

    private void Update()
    {
        if (test) {
            if (mummo.hasItem)
            {
                for (int i = 0; i < mummo.grabTargets.grabTargets.Count; i++)
                {
                    y = mummo.grabTargets.grabTargets.Find(target => target.target.name == mummo.grabThis.GetChild(0).name).name;
                    if (y == null)
                        return;

                    switch (y)
                    {
                        case "suodatinpussi": FilterSound(); break;
                        case "kahvikuppi": MugSound(); break;
                        case "kahvipannu": PotSound(true); break;
                        default: break;
                    }
                }
            }
        }
    }

}
