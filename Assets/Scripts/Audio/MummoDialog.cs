using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummoDialog : MonoBehaviour
{
    public AudioSource aSource;

    public AI mummo;

    public AudioClipsScriptable dialog;

    private AudioClip clip;

    private int i;
    private int x;
    public void DontUnderstand()
    {
        clip = dialog[1];
        aSource.PlayOneShot(clip);
    }

    public void Agree()
    {
        clip = dialog[0];
        aSource.PlayOneShot(clip);
    }

    public void Monologue()
    {
        clip = dialog[2];
        aSource.PlayOneShot(clip);
    }

    public void WhatNext()
    {
        clip = dialog[3];
        aSource.PlayOneShot(clip);
    }

    public void AlreadyDone()
    {
        clip = dialog[4];
        aSource.PlayOneShot(clip);
    }

    public void FillerTalk(int x)
    {
        i = Random.Range(0, dialog.Count);
        if (i == 2)
        {
            if (x == 1)
            {
                Agree();
            }
            else if (x == 0)
            {
                WhatNext();
            }
            else
                return;
        }
        else
            return;
        
    }
}
