using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxToggler : MonoBehaviour
{
    public List<GameObject> effects = new List<GameObject>();
    public bool isOn = false;

    public void VfxOn()
    {
        foreach (GameObject effect in effects)
        {
            effect.SetActive(true);
        }
        isOn = true;
    }
    public void VfxOff()
    {
        foreach (GameObject effect in effects)
        {
            effect.SetActive(false);
        }
        isOn = false;
    }
}
