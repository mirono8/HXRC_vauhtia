using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundEffects", menuName = "ScriptableObjects/SoundEffects")]
public class AudioClipsScriptable : ScriptableObject, IEnumerable<AudioClip>
{
    [SerializeField]
    private AudioClip[] audioClips;
   
    public int Count { get { return audioClips.Length; } }

    public AudioClip this[int index]
    {
        get { return audioClips[index]; }
    }
    
    public AudioClip PickRandom()
    {
        int i = UnityEngine.Random.Range(0,Count);
        return audioClips[i];
    }

    public IEnumerator<AudioClip> GetEnumerator()
    {
        return (audioClips as IEnumerable<AudioClip>).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return audioClips.GetEnumerator();
    }
}
