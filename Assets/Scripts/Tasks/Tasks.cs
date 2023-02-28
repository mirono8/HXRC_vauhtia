using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Tasks : MonoBehaviour  //Task-Objects (actions) for AI
{
    public class GrabAndDo : TaskClass
    {
        private Transform grabThis;
        private Transform target;

        public void Init(Transform tt, Transform t, int id, GameObject h)
        {
            grabThis = tt;
            target = t;
            step_id_value = id;
            //optional = o;
            //rigid_order = r;
            taskHolder = h;

            InitMummo();

            //Instanssin jälkeen initialisoidaan parametrit
        }

        

        public bool InteractWithGrabbed(int i)  //Katso kädessä olevan esineen step requirementit ja togglee sen triggerable
        {
            var g = grabThis.GetComponent<Triggerable>();
            
            Debug.Log(i + " i");
            Debug.Log(step_id_value + " step_id_value");

            if (g != null)
            {
                if (!g.enabled && mummo.interactThis.GetComponent<Triggerable>().enabled)  
                {
                    if (g.triggered == false)
                    {
                        if (!DoesThisHaveRequiredSteps(mummo, i))
                        {
                            g.enabled = true;
                            g.triggered = true;

                            return true;
                        }
                        else
                        {
                            if (CheckReqCompletion(mummo, step_id_value))
                            {
                                g.enabled = true;
                                g.triggered = true;

                                return true;
                            }
                            else return
                                    false;
                        }
                    }
                    else
                    {
                        Debug.Log("I, " + grabThis + " have been triggered already");
                        return false;
                    }
                }
                else if(g.enabled && g.triggered)
                {
                    Debug.Log("triggerable continuation");
                    g.enabled = false;
                    return true;
                }
                else
                {
                    Debug.Log("Target is not enabled, called from InteractWithGrabbed");
                    return false;
                }
            }
            else
            {

                if (!mummo.interactThis.GetComponent<Triggerable>().enabled)
                    mummo.interactThis.GetComponent<Triggerable>().enabled = true;
                Debug.Log("No triggerable present in grabbed obj, return true");
                return true; 
            }
        }
    }

    public class InteractWith : TaskClass
    {
        private GameObject interactHere;
        private bool isClosed;
        private bool isOpenable;
        public void Init(GameObject interactHere, GameObject holder, int index, bool closed, bool unlimited)
        {
            this.interactHere = interactHere;
            //optional = o;
            //rigid_order = r;
            taskHolder = holder;
            isClosed = closed;
            isOpenable = unlimited;
            if (index > -1)
            {
                step_id_value = index;
            }
            InitMummo();

            //Instanssin jälkeen initialisoidaan parametrit
        }

        public void InteractBinary(bool t, bool o)  // avaa joku avattava obj
        {
            var i = interactHere.GetComponent<Triggerable>();

            Debug.Log("Interacting with " + i);
            if (o)
            {
                if (t && !i.enabled)
                {

                    i.enabled = true; //open

                }
                else if (t && i.enabled)
                {
                    Debug.Log("cant open that which is already open");
                }
                else if (!t && !i.enabled)
                {
                    Debug.Log("cant close that which is already close");
                }
                else
                {
                    i.enabled = false; //close
                }
            }
            else
            {
                if (!i.enabled)
                {
                    i.enabled = true;
                }
                else
                {
                    Debug.Log("Already on");
                }
            }
        }

        public void LearnTool(string t)
        {
            for (int i = 0; i < mummo.aiTools.tools.Count; i++)
            {
                if (mummo.aiTools.tools[i].toolName == t)
                {
                    mummo.aiTools.tools[i].toolLearned = true;
                }
            }
        }

        public void Combine(Transform intoThis)
        {
            mummo.interactThis.transform.SetParent(intoThis);
            mummo.interactThis.transform.localScale = intoThis.localScale;
        }

    }

    public class GeneralInteraction : TaskClass
    {
        public void Init(GameObject h)
        {
            taskHolder = h;

            InitMummo();

            //Instanssin jälkeen initialisoidaan parametrit
        }

        public void TooVagueFail() //activoi triggerable
        {
            var t = mummo.interactThis.GetComponent<Triggerable>();

            t.triggered = true;
            t.enabled= true;

        }
    }


    public IEnumerator GrabTargetPutDownStep(Transform grabTarget, Transform place, int stepIndex, GameObject taskHolder) //Ota asia ja aseta jonnekin
    {
        var g = taskHolder.AddComponent<GrabAndDo>();
        g.Init(grabTarget, place, stepIndex, taskHolder);

        g.mummo.isListening = false;

        if (g.GrabObject())
        {
            if (g.DoesThisHaveRequiredSteps(g.mummo, stepIndex))
            {
                if (g.CheckReqCompletion(g.mummo, stepIndex))
                {

                    //animaatio vvv
                    yield return new WaitForSeconds(2);
                    g.DropObject();
                    g.SendCompletedTask(stepIndex);

                }
                
            }
            else
            {
                //animaatio vvv
                yield return new WaitForSeconds(2);
                g.DropObject();
                g.SendCompletedTask(stepIndex);
            }
            
        }
        else
        {
            Debug.Log("Mummo already has item, called from GrabTargetPutDown");
            g.mummo.InstructionMiss(4);
        }

        g.mummo.isListening = true;

        Destroy(g);
    }

    public IEnumerator GrabTargetInsert(Transform grabTarget, Transform interactTarget, int stepIndex, GameObject taskHolder) // ota obj, suorita jotain sillä, aseta alas
    {
        var g = taskHolder.AddComponent<GrabAndDo>();
        g.Init(grabTarget, interactTarget, stepIndex, taskHolder);

        g.mummo.isListening = false;

        if (g.GrabObject())
        {
            //animaatio vvv
            yield return new WaitForSeconds(5);
            if (g.InteractWithGrabbed(stepIndex))
            {
                g.DropObject();
                g.SendCompletedTask(stepIndex);
            }
            else
            {
                g.DropObject();
                Debug.Log("Could not interact");
            }
        }
        else
        {
            Debug.Log("Mummo already has item or prerequisites are not done, called from GrabTargetInsert");
            g.mummo.InstructionMiss(4);
        }

        g.mummo.isListening = true;

        Destroy(g);
    }

    public IEnumerator OpenCloseThis(GameObject target, GameObject taskHolder, int stepIndex, bool closed, bool openable) //avaa avatta asia
    {
        var iw = taskHolder.AddComponent<InteractWith>();
        iw.Init(target, taskHolder, stepIndex, closed, openable);

        iw.mummo.isListening = false;

        yield return new WaitForSeconds(5);
        iw.InteractBinary(closed,openable);
        if (stepIndex > -1)
        {
            iw.SendCompletedTask(stepIndex);
        }

        iw.mummo.isListening = true;

        Destroy(iw);
    }

    public IEnumerator InteractFail(GameObject target, GameObject taskholder) //Mönkä
    {
        var f = taskholder.AddComponent<GeneralInteraction>();
        f.Init(taskholder);

        f.mummo.isListening = false;

        if (!f.GrabObject())
        {
            Debug.Log("Mummo already has item, called from InteractFail");
            f.mummo.InstructionMiss(4);
        }
    
        f.TooVagueFail();
        f.DropObject();

        
        yield return new WaitForSeconds(5);
        f.mummo.isListening = true;

        Destroy(f);
    }

    public IEnumerator ToggleOnce(GameObject target, GameObject taskholder, int stepIndex)
    {
        var to = taskholder.AddComponent<InteractWith>();
        to.Init(target, taskholder, stepIndex, false, false);

        to.mummo.isListening = false;

        
        yield return new WaitForSeconds(5);
        if (stepIndex > -1)
        {
            if (to.DoesThisHaveRequiredSteps(to.mummo, stepIndex))
            {
                if (to.CheckReqCompletion(to.mummo, stepIndex))
                {
                    to.InteractBinary(false, false);
                    to.SendCompletedTask(stepIndex);
                }

            }
            else
            {
                to.InteractBinary(false, false);
                to.SendCompletedTask(stepIndex);
            }
            
        }
        else
            to.InteractBinary(false, false);

        to.mummo.isListening = true;
        Destroy(to);
    }

    public IEnumerator MummoWait(GameObject taskholder, int step)
    {
        var mw = taskholder.AddComponent<GeneralInteraction>();
        UnityEngine.Debug.Log("Mummo odottaa");
        mw.Init(taskholder);
        mw.mummo.isListening = false;


        if (mw.DoesThisHaveRequiredSteps(mw.mummo, step))
        {
            if (mw.CheckReqCompletion(mw.mummo, step))
            {
                yield return new WaitForSeconds(5); //ODotusanimaatio
                mw.SendCompletedTask(step);
            }
            else
            {
                Debug.Log("REquirements for waiting not complete");
            }
        }
        else
            Debug.Log("norequired steps for wait, there should be");
       
        mw.mummo.isListening = true;

        Destroy(mw);
    }

    public IEnumerator CombineHoldingItems(GameObject interactThis, Transform withThis, GameObject taskholder)  //joku checkki interactThis että onko kiinni jos asia
    {
        var chi = taskholder.AddComponent<InteractWith>();

        chi.Init(interactThis,taskholder, -10, false, false);
        chi.mummo.isListening = false;

        chi.GrabObject();

        yield return new WaitForSeconds(5); //Animaatio

        chi.Combine(withThis);

        chi.DropObject();

        chi.mummo.isListening = true;

        Destroy(chi);
    }

    public IEnumerator FreeGrabDrop(Transform grabThis, GameObject taskholder)
    {
        var fgd = taskholder.AddComponent<GeneralInteraction>();

        fgd.Init(taskholder);

        fgd.mummo.isListening = false;

        fgd.GrabObject();

        yield return new WaitForSeconds(5); //anim

        fgd.DropObject();

        fgd.mummo.isListening = true;

        Destroy (fgd);
    }
}
