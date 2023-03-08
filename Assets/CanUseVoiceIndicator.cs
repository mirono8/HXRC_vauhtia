using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanUseVoiceIndicator : MonoBehaviour
{
    public Texture offTexture;
    public Texture onTexture;
    private bool isOnTexture;

    public void TextureOn()
    {
        GetComponent<RawImage>().texture = offTexture;
        isOnTexture = false;
    }
    public void TextureOff()
    {
        GetComponent<RawImage>().texture = onTexture;
        isOnTexture = true;
    }
}
