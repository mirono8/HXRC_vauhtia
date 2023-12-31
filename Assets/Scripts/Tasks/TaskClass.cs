using System;
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

    protected int step_id_value;

    private bool done = false;
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

    public bool CheckReqCompletion(AI mummo, int step) //ei katso useampaa required askelta, palauttaa ensimmäisestä
    {
        var completedCount = 0;
        for (int x = 0; x < TaskList._taskListInstance.taskList[mummo.tracker.doingNow].stepsList[step].requiredSteps.Count; x++)
        {
            if (TaskList._taskListInstance.taskList[mummo.tracker.doingNow].stepsList[TaskList._taskListInstance.taskList[mummo.tracker.doingNow].stepsList[step].requiredSteps[x]].isCompleted)
            {
                Debug.Log("Prerequisite steps for this step have been completed");
                Debug.Log("Prereq step: "+TaskList._taskListInstance.taskList[mummo.tracker.doingNow].stepsList[TaskList._taskListInstance.taskList[mummo.tracker.doingNow].stepsList[step].requiredSteps[x]].stepName);
                completedCount++;

                if(completedCount == TaskList._taskListInstance.taskList[mummo.tracker.doingNow].stepsList[step].requiredSteps.Count)
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
        mummo.anims.SetLookTarget(mummo.grabThis.position);


        var grabOffset = gameObject;

        mummo.anims.animator.SetBool("holding", true);

        foreach (Transform child in mummo.grabThis.transform)
        {
            if (child.CompareTag("grabAI"))
                 grabOffset = child.gameObject;
            
        }

        if (!mummo.hasItem)
        {
            if (mummo.grabThis.GetComponent<Collider>() != null)
            {
                mummo.grabThis.GetComponent<Collider>().enabled = true;
            }


            if (mummo.anims.FacingLookTarget())
            {
                //
            }
            else
            {
               // mummo.anims.ChangeRotationStatus(true);
                yield return new WaitUntil(mummo.anims.FacingLookTarget);
               // mummo.anims.ChangeRotationStatus(false);
            }

            
            if (!DoesThisHaveRequiredSteps(mummo, step_id_value))
            {

                mummo.anims.GrabAnim();
                yield return new WaitUntil(mummo.anims.EndAnimResumeTask);

                
                mummo.grabThis.transform.parent = mummo.mummoGrabber;
                mummo.grabThis.transform.localPosition = new Vector3(0, 0, 0) - grabOffset.transform.localPosition;
                mummo.grabThis.transform.rotation = mummo.mummoGrabber.localRotation;//Quaternion.Euler(mummo.mummoGrabber.localRotation.x - grabOffset.transform.rotation.x, mummo.mummoGrabber.localRotation.y - grabOffset.transform.rotation.y, mummo.mummoGrabber.localRotation.z - grabOffset.transform.rotation.z); //Quaternion.Euler(new Vector3(72.2023849f, 114.251907f, 203.216797f)); 

                mummo.hasItem = true;

                for (int i = 0; i < mummo.grabTargets.grabTargets.Count; i++)
                {
                    if(mummo.grabThis.name == mummo.grabTargets.grabTargets[i].target.name)
                    {
                        if(!mummo.grabTargets.grabTargets[i].isRetrieved)
                            mummo.grabTargets.grabTargets[i].isRetrieved = true;
                    }
                }

                //return true;
            }
            else
            {
                if (CheckReqCompletion(mummo, step_id_value))
                {

                    mummo.anims.GrabAnim();
                    yield return new WaitUntil(mummo.anims.EndAnimResumeTask);

                      mummo.grabThis.transform.parent = mummo.mummoGrabber;
                      mummo.grabThis.transform.localPosition = new Vector3(0, 0, 0) - grabOffset.transform.localPosition;
                    mummo.grabThis.transform.rotation = mummo.mummoGrabber.localRotation;//Quaternion.Euler(mummo.mummoGrabber.localRotation.x - grabOffset.transform.rotation.x, mummo.mummoGrabber.localRotation.y - grabOffset.transform.rotation.y, mummo.mummoGrabber.localRotation.z - grabOffset.transform.rotation.z); //Quaternion.Euler(new Vector3(72.2023849f, 114.251907f, 203.216797f));

                    mummo.hasItem = true;

                    for (int i = 0; i < mummo.grabTargets.grabTargets.Count; i++)
                    {
                        if (mummo.grabThis.name == mummo.grabTargets.grabTargets[i].target.name)
                        {
                            if (!mummo.grabTargets.grabTargets[i].isRetrieved)
                                mummo.grabTargets.grabTargets[i].isRetrieved = true;
                        }
                    }


                    //return true;
                }
                else
                {
                    Debug.Log("Cant grab object, not retrieved. called from GrabObject");
                    mummo.InstructionMiss(1);
                    //return false;
                }
            }
        }
        else
        {
            Debug.Log("mummo already has item, called from GrabObject");
            mummo.InstructionMiss(1);
           // return false;
        }
        done = true;
    }


    public IEnumerator DropObject()
    {
        mummo.anims.animator.SetBool("holding", false);

        mummo.anims.SetLookTarget(mummo.dropHere.position);
        if (mummo.hasItem)
        {
            if(mummo.dropHere.GetComponent<Collider>() != null)
            {
                mummo.dropHere.GetComponent<Collider>().enabled = true;
            }

            if (mummo.anims.FacingLookTarget())
            {
                //
            }
            else
            {
               // mummo.anims.ChangeRotationStatus(true);
                yield return new WaitUntil(mummo.anims.FacingLookTarget);
               // mummo.anims.ChangeRotationStatus(false);
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
        done = true;
    }


    public void RotateTowardsTaskTarget(Vector3 t)
    {
        mummo.anims.SetLookTarget(t);

        if (mummo.anims.FacingLookTarget())
        {
            //
        }
        else
        {
            mummo.anims.ChangeRotationStatus(true);
        }
    }

    public bool ObjectManipulationDone()
    {
        if (done)
        {
            done = false; return true;
        }
        else
            return false;
    }

    public bool IsTargetRetrieved(GameObject t)
    {
        for (int i = 0; i < mummo.grabTargets.grabTargets.Count; i++)
        {
            if (t.name == mummo.grabTargets.grabTargets[i].target.name)
            {
                if (!mummo.grabTargets.grabTargets[i].isRetrieved)
                    return false;
                else
                    return true;
            }
        }

        Debug.Log("target not in grabbable list, returning false");

        return false;
    }
}
