using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;



public class AI : MonoBehaviour
{
    [Header("AI Targets")]
    [Tooltip("Possible grab targets")]
    public TaskTargetsToList grabTargets;

    [Tooltip("Possible drop targets")]
    public TaskTargetsToList dropTargets;

    [Tooltip("Possible general interaction targets")]
    public TaskTargetsToList interactTargets;


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
    //public TargetsByTask initTargets;

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
    [Tooltip("Permission to move")]
    public bool movementOk = false;
    [Space(10)]
    public AITools aiTools;
    public TMP_Text aiDialog;
    [Space(10)]
    public float movementSpd = 1.0f;
    public float acceptableDistance = 0.03f;
    public float currentDistance;
    [Space(10)]
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
    public NavMeshAgent agent;

    public CanUseVoiceIndicator voiceIndicator;
    
    //public int i = 0;

    public bool debugOnce = false;
    private void Awake()
    {
        tasks = taskHolder.AddComponent<Tasks>();
        taskInit = taskHolder.AddComponent<TaskInit>();
        aiTools = this.GetComponent<AITools>();
        switch (currentTask)
        {
            case "Kahvinkeitto":
                {
                    taskInit.TaskListInitialization(0);
                    //initTargets.InitKahvinkeittoTargets();
                    // TargetsByTask.targetInitInstance.InitKahvinkeittoTargets();
                    break;
                }
        }
    }

    private void Update()
    {
        var step = movementSpd * Time.deltaTime;

        voiceIndicator.isOnTexture = isListening;



        if (moveTowardsThis != null)  ////ei toimi oikein ja jostain pitää togglee movementOk ja movetowardsthis vaihto
        {
            
            currentDistance = Vector3.Distance(transform.position, new Vector3(moveTowardsThis.position.x, 0f, moveTowardsThis.position.z));


            /* if (!debugOnce)
             {
                 Debug.Log("distance not acceptable, should move");
                 debugOnce = true;
             }*/
           /* if (movementOk)
                MoveTowardsTarget(step);


            if (CloseEnough())
            {
                moveTowardsThis = null;
                movementOk = false;
            }*/

        }
        // FacePlayer();
        handDir = mummoGrabber.transform.forward;
       if (Input.GetButtonDown("TestInput"))
        {
            KahviDo(0,0);
            UnityEngine.Debug.Log("Testing");
            //SetUpTool("Paksunnos", dropHere);
        }
      
    }
    public bool CloseEnough()
    {
        return currentDistance <= acceptableDistance;
    }

    public bool IsMovementNecessary(Transform moveTowards)
    {
        moveTowardsThis = moveTowards;

        currentDistance = Vector3.Distance(transform.position, new Vector3(moveTowardsThis.position.x, 0f, moveTowardsThis.position.z));

        if (currentDistance > acceptableDistance)
        {
            Debug.Log("movement is necessary");
            agent.SetDestination(new Vector3(moveTowardsThis.position.x, 0f, moveTowardsThis.position.z));

            movementOk = true;
            return true;
        }
        else
            return false;


    }
   /* public void MoveTowardsTarget(float step)
    {
        
      //  transform.position = Vector3.MoveTowards(transform.position, new Vector3(moveTowardsThis.position.x, 0f, moveTowardsThis.position.z), step);

        if(agent.pathStatus == NavMeshPathStatus.PathComplete)
            agent.Move(agent.destination);


    }*/
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
            case 2: StartCoroutine(tasks.FreeGrabDrop(grabThis, taskHolder)); break; //ota asioita ilman step
            case 3: StartCoroutine(tasks.FreeGrabInsert(taskHolder)); break; //insert ilman step
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