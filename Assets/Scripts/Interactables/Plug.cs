using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plug : Triggerable
{
    // Start is called before the first frame update

    public Cable johto;

    private void OnEnable()
    {
        if(!triggered)
            johto.enabled = true;

        //triggered = true;   //obsolete script

    }
}
