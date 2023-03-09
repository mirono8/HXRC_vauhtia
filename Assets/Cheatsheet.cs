using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cheatsheet : MonoBehaviour
{
    public TMP_Text text;
    void Start()
    {
        for (int i = 0; i < TaskList._taskListInstance.taskList[0].t_stepTotal; i++)  // 0 = kahvinkeittoscene
        {
            if(TaskList._taskListInstance.taskList[0].stepsList[i].isCompleted)
                text.text += i + ". " + TaskList._taskListInstance.taskList[0].stepsList[i].stepName.ToString() + ", tehty!" + "\n";  //tää tarvis updaten ja paremman tulostuksen, kuten listan tai jotai
            else
                text.text += i + ". " + TaskList._taskListInstance.taskList[0].stepsList[i].stepName.ToString() + "\n";
        }
    }

    
}
