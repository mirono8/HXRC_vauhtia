using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TaskTargets;

public class SoundEffectsCoffee : MonoBehaviour
{
    
    public AudioSource aSource;

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
        aSource.PlayOneShot(clip);
    }
    public void MugSound()
    {
        clip = mug.PickRandom();
        aSource.PlayOneShot(clip);
    }

    public void PotSound(bool potOut)
    {
        if (potOut)
        {
            clip = pot[0];
            aSource.PlayOneShot(clip);
            Debug.Log("naxx out");
        }
        else
        {
            clip = pot[1];
            aSource.PlayOneShot(clip);
            Debug.Log("naxx in");

        }

    }

    public void PourSound(bool water)
    {
        if (water)
        {
            clip = pour[0];
            aSource.PlayOneShot(clip);
        }
        else
        {
            clip = pour[1];
            aSource.PlayOneShot(clip);
        }
    }

    public void TapSound(bool jug)
    {
        if (jug)
        {
            clip = tap[0];
            aSource.PlayOneShot(clip);
        }
        else
        {
            clip = tap[1];
            //aSource.clip = clip;
            aSource.PlayOneShot(clip);
            clip = tap[2];
            aSource.clip = clip;
            aSource.loop = true;
            aSource.Play();
            
        }
    }

    public void FilterSound()
    {
        clip = filter.PickRandom();
        aSource.PlayOneShot(clip);
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
