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
    public GameOverUI endScreen;
    private void Start()
    {
        endScreen = GameObject.Find("GameOverCanvas").GetComponent<GameOverUI>();
    }

    public void TextureOn()
    {
        GetComponent<RawImage>().texture = onTexture;
        toggledOn = true;
    }
    public void TextureOff()
    {
        GetComponent<RawImage>().texture = offTexture;
        toggledOn = false;
        
    }

    private void Update()
    {
        if (isOnTexture)
        {
            if (toggledOn == false)
            TextureOn();
        }
        else if (toggledOn)
        {
            TextureOff();
        }
    }
}
