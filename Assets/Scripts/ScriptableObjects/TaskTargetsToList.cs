using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TargetList", menuName = "ScriptableObjects/TaskTargetsList")]
public class TaskTargetsToList : ScriptableObject
{
    public List<TaskTargets.GrabTargets> grabTargets;
    public List<TaskTargets.DropTargets> dropTargets;
    public List<TaskTargets.InteractTargets> interactTargets;
}
