using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterTriggerable : Triggerable
{
    public MeshRenderer closed;
    public MeshRenderer open;
    public Transform drip;
    private void OnEnable()
    {

        closed.enabled = false;
        open.enabled = true;
        triggered = true;
        open.GetComponentInParent<Transform>().position = drip.transform.position; 
    }
}
