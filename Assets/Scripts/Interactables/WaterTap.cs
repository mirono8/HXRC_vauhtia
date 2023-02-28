using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTap : Triggerable
{

    public GameObject tap;
    public void OpenWater()
    {
        triggered = true;

        

        //vesijuttuja tähän

    }

    public void CloseWater()
    {
        triggered = false;


       
        //vesijuttuja tähän

    }

    private void OnEnable()
    {
        OpenWater();
    }

    private void OnDisable()
    {
        CloseWater();
    }

}
