using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class TaskClass : MonoBehaviour   //Default parameters for a Task
{

    public AI mummo;
    public TaskTracker tracker;
    public GameObject taskHolder;

    protected int step_id_value ;
   /* protected bool optional;
    protected bool rigid_order; //Cant be done out of order*/


    public void SendCompletedTask(int task_id_value)
    {
        mummo.mummoDialog.FillerTalk(0);
        tracker.StepComplete(task_id_value);
    }

    public void InitMummo()
    {
        mummo = GameObject.FindGameObjectWithTag("Mummo").GetComponent<AI>();
        tracker = GameObject.FindGameObjectWithTag("TaskTracker").GetComponent<TaskTracker>();
    }

    public bool DoesThisHaveRequiredSteps(AI mummo, int step)
    {
        if (TaskList._taskListInstance.taskList[mummo.tracker.doingNow].stepsList[step].requiredSteps.Count > 0)
        {
            Debug.Log("Step does have required steps");
            return true;
        }
        else
        {
            Debug.Log("Step does not have required steps");
            return false;
        }
    }

    public bool CheckReqCompletion(AI mummo, int step)
    {
        for (int x = 0; x < TaskList._taskListInstance.taskList[mummo.tracker.doingNow].stepsList[step].requiredSteps.Count;)
        {
            if (TaskList._taskListInstance.taskList[mummo.tracker.doingNow].stepsList[TaskList._taskListInstance.taskList[mummo.tracker.doingNow].stepsList[step].requiredSteps[x]].isCompleted)
            {
                Debug.Log("Prerequisite steps for this step have been completed");
                Debug.Log("Prereq step: "+TaskList._taskListInstance.taskList[mummo.tracker.doingNow].stepsList[TaskList._taskListInstance.taskList[mummo.tracker.doingNow].stepsList[step].requiredSteps[x]].stepName);
                return true;
            }
            else
            {
                Debug.Log("Prerequisite steps for this step have not been completed");
                return false;
            }

        }
        return false;
    }

    

    public IEnumerator GrabObject()
    {
        if (!mummo.hasItem)
        {
            if (mummo.grabThis.GetComponent<Collider>() != null)
            {
                mummo.grabThis.GetComponent<Collider>().enabled = true;
            }

            if (!DoesThisHaveRequiredSteps(mummo, step_id_value))
            {

                mummo.anims.GrabAnim();
                yield return new WaitUntil(mummo.anims.EndAnimResumeTask);
                mummo.grabThis.transform.parent = mummo.mummoGrabber;
                    mummo.grabThis.transform.localPosition = new Vector3(0, 0, 0);
                    mummo.grabThis.transform.localRotation = mummo.mummoGrabber.localRotation; //Quaternion.Euler(new Vector3(72.2023849f, 114.251907f, 203.216797f));
                    mummo.hasItem = true;
                

                //return true;
            }
            else
            {
                if (CheckReqCompletion(mummo, step_id_value))
                {

                    mummo.anims.GrabAnim();
                    yield return new WaitUntil(mummo.anims.EndAnimResumeTask);
                    mummo.grabThis.transform.parent = mummo.mummoGrabber;
                    mummo.grabThis.transform.localPosition = new Vector3(0, 0, 0);
                    mummo.grabThis.transform.localRotation = mummo.mummoGrabber.localRotation; //Quaternion.Euler(new Vector3(72.2023849f, 114.251907f, 203.216797f));
                    mummo.hasItem = true;
                    
                   // return true;
                }
                else
                {
                    Debug.Log("Cant grab object, not retrieved. called from GrabObject");
                    mummo.InstructionMiss(4);
                  //  return false;
                }
            }
        }
        else
        {
            Debug.Log("mummo already has item, called from GrabObject");
            mummo.InstructionMiss(4);
            //return false;
        }
    }


    public IEnumerator DropObject()
    {
        if (mummo.hasItem)
        {
            if(mummo.dropHere.GetComponent<Collider>() != null)
            {
                mummo.dropHere.GetComponent<Collider>().enabled = true;
            }

            mummo.anims.DropAnim();
            yield return new WaitUntil(mummo.anims.EndAnimResumeTask);
            mummo.grabThis.transform.parent = mummo.dropHere;
            mummo.grabThis.transform.localPosition = new Vector3(0, 0, 0);
            mummo.grabThis.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            mummo.hasItem = false;
            mummo.grabThis = null;
            mummo.dropHere = null;
            
        }
    }

    
}
