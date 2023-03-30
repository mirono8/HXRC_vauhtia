using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InitByIntent
{
    public static void InitOtaLaita(AI _mummo, string i_grabThis, string i_dropHere)
    {
        if (i_grabThis != null)
        {
            _mummo.grabThis = _mummo.grabTargets.grabTargets.Find(target => target.name == i_grabThis).target.transform;
            Debug.Log("Case 'ota " + i_grabThis + "'");
        }
        _mummo.dropHere = _mummo.dropTargets.dropTargets.Find(target => target.name == i_dropHere).target.transform;
        
        Debug.Log("Laita " + i_grabThis + " paikkaan " + i_dropHere);

     //   _mummo.tracker.CreateSingular(_mummo.grabThis, _mummo.dropHere, null);
    }

    public static bool InitInteract(AI _mummo, string i_target, bool i_multipleBinaryTarget) // bool true jos haluaa useampaan otteeseen aktivoida jotain
    {
        _mummo.interactThis = _mummo.interactTargets.interactTargets.Find(t => t.name == i_target).target;

        if (i_multipleBinaryTarget)
            return true;
        else
            return false;
    }

}
