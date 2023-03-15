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
    public GameOverUI endScreen;
    private void Start()
    {
        endScreen = GameObject.Find("GameOverCanvas").GetComponent<GameOverUI>();
    }

    public void TextureOn()
    {
        GetComponent<RawImage>().texture = onTexture;
        toggledOn = true;
        toggledOff = false;
    }
    public void TextureOff()
    {
        GetComponent<RawImage>().texture = offTexture;
        toggledOff = true;
        toggledOn = false;
        endScreen.NewCommandGiven();
    }

    private void Update()
    {
        if (isOnTexture)
        {
            if (toggledOff)
            TextureOn();
        }
        else if (toggledOn)
        {
            TextureOff();
        }
    }
}
