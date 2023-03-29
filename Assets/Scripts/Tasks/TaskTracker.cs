using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class TaskTracker : MonoBehaviour  //Initializes task for scene, tracks it and gives feedback
{
    //public TasksScriptableObject thisTask;

    // public TaskList _taskList;
    /*[SerializeField]
    [HideInInspector]
    public TaskListItem taskListItem;
    [HideInInspector]
    public TaskListItem.Steps step;
    
    */


    public int doingNow;
    public GameObject victoryCanvas;
    public GameObject speechlyCanvas;
    public GameOverUI endCanvas;
    public MummoDialog mummoDialog;
    private AI mummo;
    private void Start()
    {
        // _taskList=
        // ();
        /* taskListItem = new();
         step = new();*/
        doingNow = 0; //kahvinkeitto :D

        mummo = FindObjectOfType<AI>();
    }
    public void StepComplete(int step_id_value)
    {
        Debug.Log(step_id_value);
        // currentTask = thisTask.stepNames[step_id_value].ToString(); 
        var currentStep = TaskList._taskListInstance.taskList[doingNow].stepsList[step_id_value];
        var currentTask = TaskList._taskListInstance.taskList[doingNow];

        if (currentStep.requiredSteps.Any())
        {
            Debug.Log("Required step amount: " + currentStep.requiredSteps.Count);
        }
        else
        {
            Debug.Log("No pre-requisite steps");
        }

        /*  if (thisTask.tasksTotal != thisTask.tasksCurrent)
          {
              if (!thisTask.taskCompletionOrder.ContainsKey(currentTask))
              {
                  thisTask.taskCompletionOrder.Add(currentTask, step_id_value);

                  UnityEngine.Debug.Log("Dictionary " + thisTask.taskCompletionOrder.ElementAt(thisTask.tasksCurrent));
                  UnityEngine.Debug.Log("Dictionary count " + thisTask.taskCompletionOrder.Count());
                  thisTask.tasksCurrent++;
              }
          } 
          else
          {
              Debug.Log("All tasks donered");
          }*/

        if (currentTask.t_stepTotal != currentTask.t_stepsComplete)
        {
            if (currentStep.isCompleted == false && currentStep.requiredSteps.Count == 0 && currentStep.rigid_order == false)
            {
                currentStep.isCompleted = true;
                currentStep.stepCompletionOrder = currentTask.t_stepsComplete;
                currentTask.t_stepsComplete++;
                Debug.Log("Step complete, no pre-requisites");

            }
            else if (currentStep.isCompleted == false && currentStep.requiredSteps.Count > 0 && currentStep.rigid_order == true)
            {
                bool allPrereqsDone = true;

                for (int i = 0; i < currentStep.requiredSteps.Count; i++)
                {
                    if (!CheckPrerequisites(currentStep, currentTask, i))
                    {
                        allPrereqsDone = false;
                    }
                }
                if (allPrereqsDone == true)
                {
                    currentStep.isCompleted = true;
                    currentStep.stepCompletionOrder = currentTask.t_stepsComplete;
                    currentTask.t_stepsComplete++;
                    Debug.Log("Step with pre-requisite steps completed");
                }

            }
            else
                Debug.Log("Step already done");
        }
        else
        {
            Debug.Log("All steps for this task are done");
        }

        if (currentStep.isFinal)
            PlayerFeedback(doingNow);
    }

    private static bool CheckPrerequisites(TaskListItem.Steps currentStep, TaskListItem currentTask, int i)
    {
        if (!currentTask.stepsList[currentStep.requiredSteps[i]].isCompleted) //katsotaan tämän askeleen requiredSteps:n isComplete
        {
            Debug.Log("Pre-requisite step at index " + currentTask.stepsList[currentStep.requiredSteps[i]].myIndex + " named " + currentTask.stepsList[currentStep.requiredSteps[i]].stepName + " not complete error");
            return false;
        }
        else
        {
            return true;
        }
    }


    public void PlayerFeedback(int task_id)
    {
        var currentTask = TaskList._taskListInstance.taskList[task_id];

        for (int x = 0; x < currentTask.t_stepTotal; x++)
        {
            if (x != currentTask.stepsList[x].stepCompletionOrder)
            {  //Onko askel listassa eri kohdassa kuin pitäisi?
                Debug.Log("Teit vaiheen numero " + (1 + currentTask.stepsList[x].stepCompletionOrder) + ": '" + currentTask.stepsList[currentTask.stepsList[x].stepCompletionOrder].stepName.ToString()
                    + "' kohdassa numero " + (x + 1) + ": '" + currentTask.stepsList[x].stepName.ToString() + "'");
                endCanvas.feedbackText.text = "Teit vaiheen numero " + (1 + currentTask.stepsList[x].stepCompletionOrder) + ": '" + currentTask.stepsList[currentTask.stepsList[x].stepCompletionOrder].stepName.ToString()
                    + "' kohdassa numero " + (x + 1) + ": '" + currentTask.stepsList[x].stepName.ToString() + "'";
            }
            if (!currentTask.stepsList.Contains(currentTask.stepsList[x]))
            {
                Debug.Log("Jätit vaiheen numero " + ": '" + currentTask.stepsList[x].stepName.ToString() + "' tekemättä");
                endCanvas.feedbackText.text = "Jätit vaiheen numero " + ": '" + currentTask.stepsList[x].stepName.ToString() + "' tekemättä";
            }
        }

        currentTask.t_isCompleted = true;

        victoryCanvas.SetActive(true);
        speechlyCanvas.SetActive(false);
        if (endCanvas != null)
            endCanvas.ActivateMenu();

        mummoDialog.CoffeeFinish();

        mummo.anims.ThumbsUpAnim();
        
    }
    
    private void Update()
    {
        /*  if (!TaskList._taskListInstance.taskList[doingNow].t_isCompleted && (TaskList._taskListInstance.taskList[doingNow].t_stepsComplete == TaskList._taskListInstance.taskList[doingNow].t_stepTotal)) 
              //kun tehty kaikki(?) askeleet, annetaan palaute
          {
              UnityEngine.Debug.Log("Feedback incoming");
              PlayerFeedback(doingNow);

          }*/
    }
}
