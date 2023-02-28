using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterCoffee : Triggerable
{
    private void OnEnable()
    {
        GetComponent<MeshRenderer>().enabled = true;
        triggered = true;
    }
}
