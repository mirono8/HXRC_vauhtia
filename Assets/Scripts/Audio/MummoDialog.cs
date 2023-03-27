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
        i = Random.Range(0, 3);
        if (i == 0)
            clip = dialog[1];
        else if (i == 1)
            clip = dialog[5];
        else
            clip = dialog[6];

        aSource.PlayOneShot(clip);
    }

    public void Agree()
    {
        i = Random.Range(0, 2);
        if (i == 0)
            clip = dialog[7];
        else
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
        i = Random.Range(0, 2);
        if (i == 0)
            clip = dialog[12];
        else
            clip = dialog[3];

        aSource.PlayOneShot(clip);
    }

    public void AlreadyDone()
    {
        clip = dialog[4];

        aSource.PlayOneShot(clip);
    }

    public void Whoops()
    {
        i = Random.Range(0, 2);
        if (i == 0)
            clip = dialog[8];
        else
            clip = dialog[9];

        aSource.PlayOneShot(clip);
    }

    public void CoffeeFinish()
    {
        clip = dialog[13];
        aSource.PlayOneShot(clip);
    }

    public void WellInstructed()
    {
        clip = dialog[10];
        aSource.PlayOneShot(clip);
    }

    public void How()
    {
        clip = dialog[11];
        aSource.PlayOneShot(clip);
    }

    public void FillerTalk(int x)
    {
        i = Random.Range(0, 4);
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
