using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stain : Triggerable
{
    [Serializable]
    public class StainList
    {
        public GameObject stainObj;
    }

    public List<StainList> list;
    private void OnEnable()
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i].stainObj.SetActive(true);
            
        }
        triggered = true;
    }



}
