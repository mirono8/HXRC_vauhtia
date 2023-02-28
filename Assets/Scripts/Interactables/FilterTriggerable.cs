using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterTriggerable : Triggerable
{
    public MeshRenderer closed;
    public MeshRenderer open;

    private void OnEnable()
    {
        closed.enabled = false;
        open.enabled = true;
        triggered = true;

    }
}
