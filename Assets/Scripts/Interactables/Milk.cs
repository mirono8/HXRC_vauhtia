using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milk : Triggerable
{
    public Material coffeeMat;
    public MeshRenderer targetMesh;
    private void OnEnable()
    {
        triggered = true;
        targetMesh.material = coffeeMat;
    }
}
