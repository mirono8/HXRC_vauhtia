using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Targets", menuName ="ScriptableObjects/TaskTargets")]
public class TaskTargets : MonoBehaviour
{

     [Serializable]
     public class GrabTargets
     {
         [SerializeField] public string name;
         [SerializeField] public GameObject target;
     }

     [Serializable]
     public class DropTargets
     {
         [SerializeField] public string name;
         [SerializeField] public GameObject target;
     }

     [Serializable]
     public class InteractTargets
     {
         [SerializeField] public string name;
         [SerializeField] public GameObject target;
     }



}
