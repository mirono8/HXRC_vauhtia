using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskList : MonoBehaviour
{
    public static TaskList _taskListInstance { get; private set; }

    private void Awake()
    {
        if (_taskListInstance != null && _taskListInstance != this)
        {
            Destroy(this);
        }
        else
        {
            _taskListInstance = this;
        }
        //taskList.Clear(); //Ei saa laittaa tasklistiin ennen buildia listoja, muuten tarvitsee tämän
    }
    public List<TaskListItem> taskList;
    private void Start()
    {
        
        taskList = new List<TaskListItem>();
    }

}
