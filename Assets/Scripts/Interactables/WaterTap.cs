using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTap : Triggerable
{

    public GameObject tap;
    public void OpenWater()
    {
        triggered = true;

        

        //vesijuttuja t�h�n

    }

    public void CloseWater()
    {
        triggered = false;


       
        //vesijuttuja t�h�n

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
