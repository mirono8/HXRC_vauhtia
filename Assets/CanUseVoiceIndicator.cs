using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanUseVoiceIndicator : MonoBehaviour
{
    
    public Texture offTexture;
    public Texture onTexture;
    public bool isOnTexture;

    public void SwitchTexture() {
        if (isOnTexture) {
            GetComponent<RawImage>().texture = offTexture;
            isOnTexture = false;
        }
        else {
            GetComponent<RawImage>().texture = onTexture;

            isOnTexture = true;
        }
    }

    private void Update()
    {
        SwitchTexture();
    }
}
