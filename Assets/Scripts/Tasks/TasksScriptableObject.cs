using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Tooltip("Not needed")]
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Tasks", order = 1)]
public class TasksScriptableObject : ScriptableObject
{
    public string taskName;

    public int tasksCurrent;
    public int tasksTotal;

    public List<string> stepNames = new List<string>();
    public List<int> completionOrder = new List<int>();

    public Dictionary<string, int> taskCompletionOrder = new Dictionary<string, int>();

}
