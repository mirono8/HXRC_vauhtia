using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterCoffee : Triggerable
{
    public GameObject groundCoffee;
    public Transform filter;  //Avattu filter (Filterbag)
    private void OnEnable()
    {
        groundCoffee.GetComponent<MeshRenderer>().enabled = true;
        groundCoffee.GetComponent<Transform>().position = filter.position;
        triggered = true;
    }
}
