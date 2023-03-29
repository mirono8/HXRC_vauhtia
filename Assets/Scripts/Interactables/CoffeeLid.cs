using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeLid : Triggerable 
{
    public GameObject lid;
    private void Start()
    {
        
    }
    private void OnEnable()
    {

        Debug.Log("coffee open");
        lid.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        isOpen = true;
        
    }

    private void OnDisable()
    {
        Debug.Log("coffee close");
        lid.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
        isOpen = false;

    }
}
