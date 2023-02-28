using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTargets : MonoBehaviour 
{
    [Serializable]
    public class GrabTargets
    {
        public string name;
        public GameObject target;
    }

    [Serializable]
    public class DropTargets
    {
        public string name;
        public GameObject target;
    }

    [Serializable]
    public class InteractTargets
    {
        public string name;
        public GameObject target;
    }

}
