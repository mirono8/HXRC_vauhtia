using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Speechly.SLUClient;
using TMPro;
using System.Diagnostics;
using System.Linq;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;
using System;



public class AI : MonoBehaviour
{
    [Header("AI Targets")]
    [Tooltip("Possible grab targets")]
    public List<TaskTargets.GrabTargets> grabTargets;

    [Tooltip("Possible drop targets")]
    public List<TaskTargets.DropTargets> dropTargets;

    [Tooltip("Possible general interaction targets")]
    public List<TaskTargets.InteractTargets> interactTargets;


    [Space(10)]
    [SerializeField]
    [Tooltip("For grabbing")]
    public Transform grabThis;

    [Tooltip("Set object here")]
    public Transform dropHere;

    [Tooltip("Where to look")]
    public Transform lookTarget;

    [Tooltip("Move close to this")]
    public Transform moveTowardsThis;

    [Tooltip("Interact with this")]
    public GameObject interactThis;

    
    [Header("AI Tasks")]
    [Tooltip("TaskHolder")]
    public GameObject taskHolder;
    public TaskTracker tracker;


    [HideInInspector]
    public Tasks tasks;
    [HideInInspector]
    public TaskInit taskInit;

    [Tooltip("Task name here")]
    public string currentTask;

    private Vector3 newVector;


    [Header("AI Limbs")]
    [Tooltip("Slot for grabbed object")]
    public Transform mummoGrabber;
    [Tooltip("This turns toward looktarget, player by default")]
    public Transform mummoHead;
    [Tooltip("Upper body that turns toward player")]
    public Transform mummoBody;
    public Transform mummoOrigin;

    [Header("Booleans")]
    [Tooltip("Will listen for commands")]
    public bool isListening = true;
    [Tooltip("Holds something")]
    public bool hasItem = false;

    [Space(10)]
    public AITools aiTools;
    public TMP_Text aiDialog;

    private Vector3 bodyDir;
    [SerializeField]
    private float bodyAngle;
    public  float turnSpeed;
    private Vector3 headDir;

    [SerializeField]
    private float headAngle;

    public Vector3 handDir;
    private Vector3 originDir;

    private Quaternion quatHead;
    private Quaternion quatBody;
    private Quaternion quatOrigin;

    //public int i = 0;
    private void Awake()
    {
        tasks = taskHolder.AddComponent<Tasks>();
        taskInit = taskHolder.AddComponent<TaskInit>();
        aiTools = this.GetComponent<AITools>();
        switch (currentTask) {
            case "Kahvinkeitto": taskInit.KahviInit(0); break;
                
        }
    }

    private void Update()
    {
        // FacePlayer();
       handDir = mummoGrabber.transform.forward;
       if (Input.GetButtonDown("TestInput"))
        {
            UnityEngine.Debug.Log("Testing");
            //SetUpTool("Paksunnos", dropHere);
        }
    }

    [Tooltip("Give the AI a target")]
    public void GetTarget(Transform t)
    {
        grabThis = t;
    }

    public void MoveTarget()
    {
        
        newVector += new Vector3(0, 0, +20f);
        grabThis.transform.localPosition = newVector;

    }
    
    public void KahviDo(int toDo, int stepIndex)
    {
        var currentStep = TaskList._taskListInstance.taskList[tracker.doingNow].stepsList[stepIndex];

        switch (toDo)
        {
            case 0:
                if (!currentStep.isCompleted)
                {
                    StartCoroutine(tasks.GrabTargetPutDownStep(grabThis, dropHere, stepIndex, taskHolder)); // Ota target(valittu ��nikomennolla), ja laita muualle, merkkaa askeleen (stepIndex) tehdyksi
                    break;
                }
                else
                {
                    UnityEngine.Debug.Log(currentStep.stepName + " is already completed");
                    InstructionMiss(2);
                    break;
                }
            case 1:
                if (!currentStep.isCompleted)
                {
                    StartCoroutine(tasks.GrabTargetInsert(grabThis, dropHere, stepIndex, taskHolder)); // Ota target(valittu ��nikomennolla) , ja suorita k�ytt�en target:ia ja merkkaa askel (stepIndex) tehdyksi
                    break;
                }
                else
                {
                    UnityEngine.Debug.Log(currentStep.stepName + " is already completed");
                    InstructionMiss(2);
                    break;
                }
            case 2:
                if (!currentStep.isCompleted)
                {
                    StartCoroutine(tasks.OpenCloseThis(interactThis, taskHolder, stepIndex, false, true)); // sulje kansi ja suorita step :D
                    break;
                }
                else
                {
                    UnityEngine.Debug.Log(currentStep.stepName + " is already completed");
                    InstructionMiss(2);
                    break;
                }
            case 3:
                if(!currentStep.isCompleted)
                {
                    StartCoroutine(tasks.ToggleOnce(interactThis, taskHolder, stepIndex));  //Laita jokin p��lle kerran
                    break;
                }
                else
                {
                    UnityEngine.Debug.Log(currentStep.stepName + " is already completed");
                    InstructionMiss(2);
                    break;
                }
            case 4:
                StartCoroutine(tasks.MummoWait(taskHolder, stepIndex));
                break;

            default: break;
        }

        UnityEngine.Debug.Log("Kahvi DO: " + toDo);
    }

    public void GeneralDo(int toDo, bool open) //Asioita mit� AI voi tehd� mit� ei lasketa askeleiksi
    {
        switch (toDo)
        {
            case 0: StartCoroutine(tasks.OpenCloseThis(interactThis, taskHolder, -10, open, true)); break;  //avaa tai sulje
            case 1: StartCoroutine(tasks.InteractFail(interactThis, taskHolder)); break; //liian vague k�sky
            case 2: StartCoroutine(tasks.FreeGrabDrop(grabThis, taskHolder)); break;
        } //TOOLS INTERACTIONS
    }

    public void ToolsDo(int toDo)
    {
        switch (toDo)
        {
            case 0: StartCoroutine(tasks.CombineHoldingItems(interactThis, grabThis, taskHolder)); break; // Asia vasemmasta kädestä kiinni oikeaan käteen (grabThis)
        }
    }
   /* public void SetUpTool(string toolName, Transform dropHere)
    {
        aiTools.SetUpTool(toolName, dropHere);
    }*/

    [Tooltip("Jos k�sky ep�selv�, ilmoita")]
    public void InstructionMiss(int i)
    {
        /*int i = 0;
        i = UnityEngine.Random.Range(0, 4);
        //UnityEngine.Debug.Log("Random num: " + i);*/

        switch (i)
        {
            case 0: aiDialog.text = "Voisitko toistaa?"; break;
            case 1: aiDialog.text = "En kuullut"; break;
            case 2: aiDialog.text = "Tein sen jo.."; break;
            case 3: aiDialog.text = "???"; break;
            case 4: aiDialog.text = "Mit� teen t�ll�?"; break;
        }

    }

   /* public void FacePlayer() // kokeile animaattoria ja ik-manipulointia lookat sun muut
    {
        originDir = mummoOrigin.transform.forward;
        bodyDir = mummoBody.transform.forward;
        headDir = mummoHead.transform.forward;

        headAngle = Vector3.Angle(headDir, originDir);
        bodyAngle = Vector3.Angle(bodyDir, originDir);

        quatOrigin = Quaternion.Euler(originDir);
        quatHead = Quaternion.LookRotation(Camera.main.transform.position - mummoHead.transform.position);
        quatBody = Quaternion.LookRotation(Camera.main.transform.position - mummoBody.transform.position);

        if (headAngle > 15f && bodyAngle < 60 && isListening)
        {
            quatBody.x = 0f;
            quatBody.z = 0f;
            Quaternion bodyRotationX = Quaternion.Slerp(mummoBody.transform.rotation, quatBody, turnSpeed * Time.deltaTime);
            mummoBody.transform.rotation = bodyRotationX;
            goBeyond = true; //t�st� metodi jossa p�� menee yli 90 astetta ja nromalisoituu jos body l�htee originiin
        }
        else //t�h�n joku fullbody py�r�hdys
        {
            goBeyond = false;
            //quatHead = Quaternion.Euler(0, 0, 0);
            quatBody = Quaternion.Euler(0, 0, 0);
            //  mummoHead.transform.rotation = Quaternion.Slerp(mummoHead.transform.rotation, quatHead, (1.5f * turnSpeed) * Time.deltaTime);

            Quaternion bodyRotationX = Quaternion.Slerp(mummoBody.transform.rotation, quatOrigin, (1.5f * turnSpeed) * Time.deltaTime);
            mummoBody.transform.rotation = bodyRotationX;
        }


        if (headAngle < 70f && isListening)
        {
            mummoHead.transform.rotation = Quaternion.Slerp(mummoHead.transform.rotation, quatHead, turnSpeed * Time.deltaTime);
        } 
        else
        {
            ResetHeadRotation(quatOrigin);
        }
    }

    public void ResetHeadRotation(Quaternion origin)
    {
        mummoHead.transform.rotation = Quaternion.Slerp(mummoHead.transform.rotation, origin, (1.2f * turnSpeed) * Time.deltaTime);
    }*/
}