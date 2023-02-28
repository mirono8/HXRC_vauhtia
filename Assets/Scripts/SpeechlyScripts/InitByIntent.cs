using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InitByIntent
{
    public static void InitOtaLaita(AI _mummo, string i_grabThis, string i_dropHere)
    {

        _mummo.grabThis = _mummo.grabTargets.Find(target => target.name == i_grabThis).target.transform;
        Debug.Log("Case 'ota " + i_grabThis + "'");

        _mummo.dropHere = _mummo.dropTargets.Find(target => target.name == i_dropHere).target.transform;
        
        Debug.Log("Laita " + i_grabThis + " paikkaan " + i_dropHere);

    }

    public static bool InitInteract(AI _mummo, string i_target, bool i_multipleBinaryTarget) // bool true jos haluaa useampaan otteeseen aktivoida jotain
    {
        _mummo.interactThis = _mummo.interactTargets.Find(t => t.name == i_target).target;

        if (i_multipleBinaryTarget)
            return true;
        else
            return false;
    }

}
