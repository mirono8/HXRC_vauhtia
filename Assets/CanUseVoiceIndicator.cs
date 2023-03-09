using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanUseVoiceIndicator : MonoBehaviour
{
    
    public Texture offTexture;
    public Texture onTexture;
    public bool isOnTexture;
    public bool toggledOn;
    public bool toggledOff;

    public void TextureOn()
    {
        GetComponent<RawImage>().texture = offTexture;
        toggledOn = true;
        toggledOff = false;
    }
    public void TextureOff()
    {
        GetComponent<RawImage>().texture = onTexture;
        toggledOff = true;
        toggledOn = false;
    }

    private void Update()
    {
        if (isOnTexture)
        {
            if (!toggledOn)
            TextureOn();
        }
        else if (!toggledOff)
        {
            TextureOff();
        }
    }
}
