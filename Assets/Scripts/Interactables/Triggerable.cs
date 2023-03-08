using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Triggerable :  MonoBehaviour
{
    public bool triggered;

    public int counter = 0;

    [HideInInspector]
    public bool isOpen;
}
