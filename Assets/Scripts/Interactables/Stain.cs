using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stain : Triggerable
{
    private void OnEnable()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        triggered = true;
    }



}
