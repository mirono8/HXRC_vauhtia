using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskInit : MonoBehaviour
{
    //[SerializeField]
    [HideInInspector]
    public TaskListItem taskListItem;
    [HideInInspector]
    public TaskListItem.Steps step;

    
    private void Start()
    {
        // _taskList= new();
        taskListItem = new();
        step = new();

    }
    public void TaskListInitialization(int task_id)
    {
        //Debug.Log("kahvi_init: " + TaskList._taskListInstance.taskList.Count);
        /*  taskListItem.t_name = "Kahvinkeitto";
        taskListItem.t_stepTotal = 15;
        taskListItem.t_stepCurrent = 0;*/

        
        int i = 0;
        TaskList._taskListInstance.taskList.Add(new TaskListItem() // lis‰t‰‰n singleton task-listaan uusi task
        {
            t_name = "Kahvinkeitto",
            t_stepTotal = 15,
            t_stepsComplete = 0,
            stepsList = new List<TaskListItem.Steps>()
        });

        step = new TaskListItem.Steps(); //luodaan instanssi stepist‰
        step.requiredSteps = new List<int>();
        step.stepName = "Suodatinpussin ottaminen laatikosta";
        step.rigid_order = false; step.isOptional = false;
        step.myIndex = i;
        TaskList._taskListInstance.taskList[task_id].stepsList.Add(step); //lis‰t‰‰n step taskiin
        i++;

        step = new TaskListItem.Steps();
        step.requiredSteps = new List<int>();
        step.stepName = "Suodatinpussin laittaminen keittimeen";
        step.rigid_order = true; step.isOptional = false;
        step.myIndex = i;
        TaskList._taskListInstance.taskList[task_id].stepsList.Add(step);
        TaskList._taskListInstance.taskList[task_id].stepsList[i].requiredSteps.Add(0); //annetaan arvoksi jonkin muun stepin index, joka pit‰‰ suorittaa ennen t‰t‰
        i++;

        step = new TaskListItem.Steps();
        step.requiredSteps = new List<int>();
        step.stepName = "Kahvipurujen ottaminen kaapista";
        step.rigid_order = false; step.isOptional = false;
        step.myIndex = i;
        TaskList._taskListInstance.taskList[task_id].stepsList.Add(step);
        i++;

        step = new TaskListItem.Steps();
        step.requiredSteps = new List<int>();
        step.stepName = "Kahvipurujen mittaaminen suodatinpussiin";
        step.rigid_order = true; step.isOptional = false;
        step.myIndex = i;
        TaskList._taskListInstance.taskList[task_id].stepsList.Add(step);
        TaskList._taskListInstance.taskList[task_id].stepsList[i].requiredSteps.Add(0);
        TaskList._taskListInstance.taskList[task_id].stepsList[i].requiredSteps.Add(1);
        TaskList._taskListInstance.taskList[task_id].stepsList[i].requiredSteps.Add(2);
        i++;

        step = new TaskListItem.Steps();
        step.requiredSteps = new List<int>();
        step.stepName = "Vesikannun otto tiskikaapista";
        step.rigid_order = false; step.isOptional = false;
        step.myIndex = i;
        TaskList._taskListInstance.taskList[task_id].stepsList.Add(step);
        i++;

        step = new TaskListItem.Steps();
        step.requiredSteps = new List<int>();
        step.stepName = "Vesihanan avaaminen ja kannun t‰yttˆ";
        step.rigid_order = true; step.isOptional = false;
        step.myIndex = i;
        TaskList._taskListInstance.taskList[task_id].stepsList.Add(step);
        TaskList._taskListInstance.taskList[task_id].stepsList[i].requiredSteps.Add(4);
        i++;

        step = new TaskListItem.Steps();
        step.requiredSteps = new List<int>();
        step.stepName = "Veden kaataminen kahvinkeittimeen";
        step.rigid_order = true; step.isOptional = false;
        step.myIndex = i;
        TaskList._taskListInstance.taskList[task_id].stepsList.Add(step);
        TaskList._taskListInstance.taskList[task_id].stepsList[i].requiredSteps.Add(4);
        TaskList._taskListInstance.taskList[task_id].stepsList[i].requiredSteps.Add(5);
        i++;

        step = new TaskListItem.Steps();
        step.requiredSteps = new List<int>();
        step.stepName = "Kannen sulkeminen";
        step.rigid_order = true; step.isOptional = false;
        step.myIndex = i;
        TaskList._taskListInstance.taskList[task_id].stepsList.Add(step);
        TaskList._taskListInstance.taskList[task_id].stepsList[i].requiredSteps.Add(6);
        i++;

        step = new TaskListItem.Steps();
        step.requiredSteps = new List<int>();
        step.stepName = "Tarkistetaan s‰hkˆjohto";
        step.myIndex = i;
        step.rigid_order = false; step.isOptional = false;
        TaskList._taskListInstance.taskList[task_id].stepsList.Add(step);
        i++;

        step = new TaskListItem.Steps();
        step.requiredSteps = new List<int>();
        step.stepName = "Laitetaan kahvinkeitin p‰‰lle";
        step.rigid_order = true; step.isOptional = false;
        step.myIndex = i;
        TaskList._taskListInstance.taskList[task_id].stepsList.Add(step);
        TaskList._taskListInstance.taskList[task_id].stepsList[i].requiredSteps.Add(3);
        TaskList._taskListInstance.taskList[task_id].stepsList[i].requiredSteps.Add(6);
        TaskList._taskListInstance.taskList[task_id].stepsList[i].requiredSteps.Add(7);
        TaskList._taskListInstance.taskList[task_id].stepsList[i].requiredSteps.Add(8);
        i++;

        step = new TaskListItem.Steps();
        step.requiredSteps = new List<int>();
        step.stepName = "Annetaan kahvin tippua";
        step.rigid_order = true; step.isOptional = false;
        step.myIndex = i;
        TaskList._taskListInstance.taskList[task_id].stepsList.Add(step);
        TaskList._taskListInstance.taskList[task_id].stepsList[i].requiredSteps.Add(9);
        i++;

        step = new TaskListItem.Steps();
        step.requiredSteps = new List<int>();
        step.stepName = "Otetaan kahvimuki kaapista";
        step.rigid_order = false; step.isOptional = false;
        step.myIndex = i;
        TaskList._taskListInstance.taskList[task_id].stepsList.Add(step);
        i++;

        step = new TaskListItem.Steps();
        step.requiredSteps = new List<int>();
        step.stepName = "Kaadetan kahvi mukiin";
        step.rigid_order = true; step.isOptional = false;
        step.myIndex = i;
        TaskList._taskListInstance.taskList[task_id].stepsList.Add(step);
        TaskList._taskListInstance.taskList[task_id].stepsList[i].requiredSteps.Add(10);
        TaskList._taskListInstance.taskList[task_id].stepsList[i].requiredSteps.Add(11);
        i++;

        step = new TaskListItem.Steps(); // vois ottaa kokonaan pois listalta ja ollaa vaa general juttu?
        step.requiredSteps = new List<int>();
        step.stepName = "Lis‰t‰‰n mahdolliset lis‰ykset kahviin";
        step.rigid_order = true; step.isOptional = true;
        step.myIndex = i;
        TaskList._taskListInstance.taskList[task_id].stepsList.Add(step);
        TaskList._taskListInstance.taskList[task_id].stepsList[i].requiredSteps.Add(12);
        i++;

        step = new TaskListItem.Steps();
        step.requiredSteps = new List<int>();
        step.stepName = "Sekoitetaan lusikalla";
        step.rigid_order = true; step.isOptional = false; step.isFinal = true;
        step.myIndex = i;
        TaskList._taskListInstance.taskList[task_id].stepsList.Add(step);
        TaskList._taskListInstance.taskList[task_id].stepsList[i].requiredSteps.Add(12);
        i++;

        Debug.Log(TaskList._taskListInstance.taskList[task_id].t_name);

        foreach (var step in TaskList._taskListInstance.taskList[task_id].stepsList)
        {
            Debug.Log(step.stepName);
        }

    
    }

    private void OnApplicationQuit() //pit‰‰ ehk‰ vaihtaa onapplicationfocus kun buildaa?
    {
        /*   thisTask.taskName = "";
           thisTask.tasksTotal = 0;
           thisTask.tasksCurrent = 0;
           thisTask.completionOrder = null;
           thisTask.taskCompletionOrder.Clear();
           thisTask.taskCompletionOrder = null;
           thisTask.stepNames = null;*/

        //_taskList = null;
        taskListItem = null;
        step = null;
        TaskList._taskListInstance.taskList.Clear();
    }
}
