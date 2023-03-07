using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechlyRestarter : MonoBehaviour
{
    public GameObject speechly;
    public static SpeechlyRestarter _restarterInstance { get; private set; }
    private void Awake()
    {
        if (_restarterInstance != null && _restarterInstance != this)
        {
            Destroy(this);
        }
        else
        {
            _restarterInstance = this;
        }

    }
    public void RestartSpeechly()
    {
        speechly.SetActive(false);
        speechly.SetActive(true);
    }
    
}
