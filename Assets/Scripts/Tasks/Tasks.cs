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

            //Instanssin j�lkeen initialisoidaan parametrit
        }

        

        public bool InteractWithGrabbed(int i)  //Katso k�dess� olevan esineen step requirementit ja togglee sen triggerable
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

            //Instanssin j�lkeen initialisoidaan parametrit
        }

        public void InteractBinary(bool closed, bool openable)  // avaa joku avattava obj
        {
            var interactTarget = interactHere.GetComponent<Triggerable>();

            Debug.Log("Interacting with " + interactTarget);
            if (openable)
            {
                if (!interactTarget.isOpen && !interactTarget.enabled)
                {

                    interactTarget.enabled = true; //open
                    interactTarget.isOpen = true;

                }
                else
                {
                    interactTarget.isOpen = false;
                    interactTarget.enabled = false; //close
                }
               /* else if (!closed && !interactTarget.enabled)
                {
                    Debug.Log("cant close that which is already close");
                }
                else
                {
                    interactTarget.enabled = false; //close
                }*/
            }
            else
            {
                if (!interactTarget.enabled)
                {
                    interactTarget.enabled = true;
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

            //Instanssin j�lkeen initialisoidaan parametrit
        }

        public void TooVagueFail() //activoi triggerable
        {
            var t = mummo.interactThis.GetComponent<Triggerable>();

            t.triggered = true;
            t.enabled= true;

        }

        public void InsertAndCount()
        {
            var t = mummo.interactThis.GetComponent<Triggerable>();

            
            t.counter++;
        }
    }


    public IEnumerator GrabTargetPutDownStep(Transform grabTarget, Transform place, int stepIndex, GameObject taskHolder) //Ota asia ja aseta jonnekin
    {
        var g = taskHolder.AddComponent<GrabAndDo>();
        g.Init(grabTarget, place, stepIndex, taskHolder);

        g.mummo.isListening = false;

        if (g.mummo.IsMovementNecessary(grabTarget))
        {
            g.mummo.anims.WalkAnim(true);
            yield return new WaitUntil(g.mummo.CloseEnough);
            g.mummo.anims.WalkAnim(false);
        }

        
        //animaatio
        //yield return new WaitUntil(g.mummo.anims.EndAnimResumeTask);
        StartCoroutine(g.GrabObject()); 
        

            yield return new WaitUntil(g.mummo.anims.EndAnimResumeTask);

        if (g.DoesThisHaveRequiredSteps(g.mummo, stepIndex))
        {
            if (g.CheckReqCompletion(g.mummo, stepIndex))
            {


                if (g.mummo.IsMovementNecessary(g.mummo.dropHere))
                {
                    g.mummo.anims.WalkAnim(true);
                    yield return new WaitUntil(g.mummo.CloseEnough);
                    g.mummo.anims.WalkAnim(false);
                }


                // yield return new WaitForSeconds(2.5f);//animaatio
                g.DropObject();

                g.SendCompletedTask(stepIndex);

            }

        }
        else
        {
            //animaatio vvv
            if (g.mummo.IsMovementNecessary(g.mummo.dropHere))
            {
                g.mummo.anims.WalkAnim(true);
                yield return new WaitUntil(g.mummo.CloseEnough);
                g.mummo.anims.WalkAnim(false);
            }
           
            // yield return new WaitForSeconds(2.5f); //animaatio vvv

            g.DropObject();
            //  yield return new WaitUntil(g.mummo.anims.EndAnimResumeTask);
            g.SendCompletedTask(stepIndex);
        }




        g.mummo.isListening = true;

        Destroy(g);
    }

    public IEnumerator GrabTargetInsert(Transform grabTarget, Transform interactTarget, int stepIndex, GameObject taskHolder) // ota obj, suorita jotain sill�, aseta alas
    {
        var g = taskHolder.AddComponent<GrabAndDo>();
        g.Init(grabTarget, interactTarget, stepIndex, taskHolder);

        g.mummo.isListening = false;

        if (g.mummo.IsMovementNecessary(grabTarget))
        {
            g.mummo.anims.WalkAnim(true);
            yield return new WaitUntil(g.mummo.CloseEnough);
            g.mummo.anims.WalkAnim(false);
        }

       

        if (g.DoesThisHaveRequiredSteps(g.mummo, stepIndex))
        {
            if (g.CheckReqCompletion(g.mummo, stepIndex))
            {
                StartCoroutine(g.GrabObject());


                yield return new WaitUntil(g.mummo.anims.EndAnimResumeTask);
                //animaatio vvv

                if (g.mummo.IsMovementNecessary(g.mummo.interactThis.transform))
                {
                    g.mummo.anims.WalkAnim(true);
                    yield return new WaitUntil(g.mummo.CloseEnough);
                    g.mummo.anims.WalkAnim(false);
                }

                

                if (g.InteractWithGrabbed(stepIndex))
                {
                    g.mummo.anims.ActionAnim();

                    yield return new WaitUntil(g.mummo.anims.EndAnimResumeTask);
                    if (g.mummo.IsMovementNecessary(g.mummo.dropHere))
                    {
                        g.mummo.anims.WalkAnim(true);
                        yield return new WaitUntil(g.mummo.CloseEnough);
                        g.mummo.anims.WalkAnim(false);
                    }

                    

                    g.DropObject();
                    g.SendCompletedTask(stepIndex);
                }
                else
                {
                    if (g.mummo.IsMovementNecessary(g.mummo.dropHere))
                    {
                        g.mummo.anims.WalkAnim(true);
                        yield return new WaitUntil(g.mummo.CloseEnough);
                        g.mummo.anims.WalkAnim(false);
                    }

                    

                    g.DropObject();
                    Debug.Log("Could not interact");
                    g.mummo.mummoDialog.DontUnderstand();
                }
            }
            else
            {
                g.mummo.mummoDialog.DontUnderstand();

            }
        }
        else
        {
            StartCoroutine(g.GrabObject());


            yield return new WaitUntil(g.mummo.anims.EndAnimResumeTask);
            //animaatio vvv

            if (g.mummo.IsMovementNecessary(g.mummo.interactThis.transform))
            {
                g.mummo.anims.WalkAnim(true);
                yield return new WaitUntil(g.mummo.CloseEnough);
                g.mummo.anims.WalkAnim(false);
            }

           

            if (g.InteractWithGrabbed(stepIndex))
            {
                g.mummo.anims.ActionAnim();

                yield return new WaitUntil(g.mummo.anims.EndAnimResumeTask);
                if (g.mummo.IsMovementNecessary(g.mummo.dropHere))
                {
                    g.mummo.anims.WalkAnim(true);
                    yield return new WaitUntil(g.mummo.CloseEnough);
                    g.mummo.anims.WalkAnim(false);
                }

                

                g.DropObject();

                g.SendCompletedTask(stepIndex);
            }
        }

        g.mummo.isListening = true;

        Destroy(g);
    }

    public IEnumerator OpenCloseThis(GameObject target, GameObject taskHolder, int stepIndex, bool closed, bool openable) //avaa avatta asia
    {
        var iw = taskHolder.AddComponent<InteractWith>();
        iw.Init(target, taskHolder, stepIndex, closed, openable);

        iw.mummo.isListening = false;

        if (iw.mummo.IsMovementNecessary(target.transform))
        {
            iw.mummo.anims.WalkAnim(true);
            yield return new WaitUntil(iw.mummo.CloseEnough);
            iw.mummo.anims.WalkAnim(false);
        }
        

        iw.mummo.anims.ActionAnim();

        yield return new WaitUntil(iw.mummo.anims.EndAnimResumeTask);

        iw.InteractBinary(closed,openable);
        if (stepIndex > -1)
        {
            iw.SendCompletedTask(stepIndex);
        }

        iw.mummo.isListening = true;

        Destroy(iw);
    }

    public IEnumerator InteractFail(GameObject target, GameObject taskholder) //M�nk�
    {
        var f = taskholder.AddComponent<GeneralInteraction>();
        f.Init(taskholder);

        f.mummo.isListening = false;

        if (f.mummo.IsMovementNecessary(f.mummo.grabThis))
        {
            f.mummo.anims.WalkAnim(true);
            yield return new WaitUntil(f.mummo.CloseEnough);
            f.mummo.anims.WalkAnim(false);
        }

        

        StartCoroutine(f.GrabObject());

        yield return new WaitUntil(f.mummo.anims.EndAnimResumeTask);
        /*if (!f.GrabObject())
        {
            Debug.Log("Mummo already has item, called from InteractFail");
            f.mummo.InstructionMiss(4);
        }*/

        f.mummo.anims.ActionAnim();

        yield return new WaitUntil(f.mummo.anims.EndAnimResumeTask);

        f.TooVagueFail();

        if (f.mummo.IsMovementNecessary(f.mummo.dropHere))
        {   
            f.mummo.anims.WalkAnim(true);
            yield return new WaitUntil(f.mummo.CloseEnough);
            f.mummo.anims.WalkAnim(false);
        }


        f.DropObject();

        f.mummo.InstructionMiss(2);
        f.mummo.isListening = true;

        Destroy(f);
    }

    public IEnumerator ToggleOnce(GameObject target, GameObject taskholder, int stepIndex)
    {
        var to = taskholder.AddComponent<InteractWith>();
        to.Init(target, taskholder, stepIndex, false, false);

        to.mummo.isListening = false;

        
        
        if (stepIndex > -1)
        {
            if (to.DoesThisHaveRequiredSteps(to.mummo, stepIndex))
            {
                if (to.CheckReqCompletion(to.mummo, stepIndex))
                {
                    if (to.mummo.IsMovementNecessary(target.transform))
                    {   
                        to.mummo.anims.WalkAnim(true);
                        yield return new WaitUntil(to.mummo.CloseEnough);
                        to.mummo.anims.WalkAnim(false);
                    }

                    

                    to.mummo.anims.ActionAnim();

                    yield return new WaitUntil(to.mummo.anims.EndAnimResumeTask);

                    to.InteractBinary(false, false);

                    to.SendCompletedTask(stepIndex);
                }

            }
            else
            {
                if (to.mummo.IsMovementNecessary(target.transform))
                {   
                    to.mummo.anims.WalkAnim(true);
                    yield return new WaitUntil(to.mummo.CloseEnough);
                    to.mummo.anims.WalkAnim(false);
                }
                

                to.mummo.anims.ActionAnim();

                yield return new WaitUntil(to.mummo.anims.EndAnimResumeTask);
                to.InteractBinary(false, false);
                to.SendCompletedTask(stepIndex);
            }

        }
        else
        {
            if (to.mummo.IsMovementNecessary(target.transform))
            {
                to.mummo.anims.WalkAnim(true);
                yield return new WaitUntil(to.mummo.CloseEnough);
                to.mummo.anims.WalkAnim(false);
            }
            

            to.mummo.anims.ActionAnim();

            yield return new WaitUntil(to.mummo.anims.EndAnimResumeTask);
            to.InteractBinary(false, false);
        }

        to.mummo.isListening = true;
        to.mummo.KahviDo(4, 10);  //ODOTUS T�SS� HETI PAINAMISEN J�LKEEN AUTOMAATTISESTI
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
                mw.mummo.mummoDialog.Monologue();
                yield return new WaitForSeconds(10); //ODotusanimaatio
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

    public IEnumerator CombineHoldingItems(GameObject interactThis, Transform withThis, GameObject taskholder)  //joku checkki interactThis ett� onko kiinni jos asia
    {
        var chi = taskholder.AddComponent<InteractWith>();

        chi.Init(interactThis,taskholder, -10, false, false);
        chi.mummo.isListening = false;

        StartCoroutine(chi.GrabObject());

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

        if (fgd.mummo.IsMovementNecessary(grabThis))
        {
            fgd.mummo.anims.WalkAnim(true);
            yield return new WaitUntil(fgd.mummo.CloseEnough);
            fgd.mummo.anims.WalkAnim(false);
        }

        

        StartCoroutine(fgd.GrabObject());

        yield return new WaitUntil(fgd.mummo.anims.EndAnimResumeTask); //anim


        if (fgd.mummo.IsMovementNecessary(fgd.mummo.dropHere))
        {
            fgd.mummo.anims.WalkAnim(true);
            yield return new WaitUntil(fgd.mummo.CloseEnough);
            fgd.mummo.anims.WalkAnim(false);
        }
        

        fgd.DropObject();

        fgd.mummo.isListening = true;

        Destroy(fgd);
    }

    public IEnumerator FreeGrabInsert(GameObject taskholder)
    {
        var fi = taskholder.AddComponent<GeneralInteraction>();

        fi.Init(taskholder);

        fi.mummo.isListening = false;

        if (fi.mummo.IsMovementNecessary(fi.mummo.grabThis))
        {
            fi.mummo.anims.WalkAnim(true);
            yield return new WaitUntil(fi.mummo.CloseEnough);
            fi.mummo.anims.WalkAnim(false);
        }

        

        StartCoroutine(fi.GrabObject());

        yield return new WaitUntil(fi.mummo.anims.EndAnimResumeTask);

        if (fi.mummo.IsMovementNecessary(fi.mummo.grabThis))
        {
            fi.mummo.anims.WalkAnim(true);
            yield return new WaitUntil(fi.mummo.CloseEnough);
            fi.mummo.anims.WalkAnim(false);
        }
        

        fi.mummo.anims.ActionAnim();
        yield return new WaitUntil(fi.mummo.anims.EndAnimResumeTask);

        fi.InsertAndCount();

        if (fi.mummo.IsMovementNecessary(fi.mummo.grabThis))
        {
            fi.mummo.anims.WalkAnim(true);
            yield return new WaitUntil(fi.mummo.CloseEnough);
            fi.mummo.anims.WalkAnim(false);
        }
        

        fi.DropObject();
       
        

        fi.mummo.isListening = true;

        Destroy(fi);
    }
}
