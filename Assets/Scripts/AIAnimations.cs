using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimations : MonoBehaviour 
{
    public AI mummo;
    public Animator animator;

    private bool done = false;
    public  bool ikActive = false;
    private float lookAtWeight;

    private bool headReset;

    [SerializeField]
    private float dampVelocity = 0;
    private void Update()
    {
        if (mummo.CalculateHeadVsPlayer() < 90 ) //90f(mummo.mummoHead.rotation.y < 0.45f) && (mummo.mummoHead.rotation.y > -0.45f)
        {
            if ((mummo.mummoHead.rotation.y > 0.5f) || (mummo.mummoHead.rotation.y < -0.5f))
            {
                //Pää ei käänny enää
                Debug.Log(lookAtWeight);
              //  lookAtWeight = 0.5f;
                lookAtWeight = Mathf.SmoothDamp(lookAtWeight, 0.5f, ref dampVelocity, 3f);  //kinda wonky

            }
            else
            {
                // lookAtWeight = Mathf.Lerp(lookAtWeight, 1, Time.deltaTime * 2.5f);
                lookAtWeight = Mathf.SmoothDamp(lookAtWeight, 1, ref dampVelocity, 2f);
            }
        }
        else
        {
            headReset = true;
            //lookAtWeight = Mathf.Lerp(lookAtWeight, 0, Time.deltaTime * 2.5f);   
            lookAtWeight = Mathf.SmoothDamp(lookAtWeight, 0, ref dampVelocity, 2f);

        }

       
    }

    private void OnAnimatorIK()
    {
        if (ikActive)
        {
            if (mummo.isListening)
            {

                animator.SetLookAtWeight(lookAtWeight);
                animator.SetLookAtPosition(Camera.main.transform.position);
            }
        }
    }
    public void GrabAnim()
    {
        done = false;
       // animator.SetLayerWeight(2, 1);
        animator.ResetTrigger("dropTrigger");
        animator.SetTrigger("grabTrigger");
         
    }

    public void DropAnim()
    {
        done = false;
        //animator.SetLayerWeight(2, 0);
        animator.ResetTrigger("grabTrigger");
        animator.SetTrigger("dropTrigger");
    }

    public void WalkAnim(bool go)
    {
        done = false;
        //animator.SetLayerWeight(0, 1);
        if (go)
            animator.SetTrigger("walkTrigger");
        else
        {
            //animator.SetLayerWeight(0, 0);
            animator.SetTrigger("reset"); // tää jättää mummon jumii
            //SetDone(); 
        }//animator.ResetTrigger("dropTrigger");
    }

    public void ActionAnim()
    {
        done = false;
        animator.SetTrigger("actionTrigger");
        //animator.ResetTrigger("dropTrigger");
    }
    public void WhatAnim()
    {
        done = false;
        animator.SetTrigger("whatTrigger");
        //animator.ResetTrigger("dropTrigger");
    }

    public void SetDone()
    {
        done = true;
        //animator.SetTrigger("reset");
    }

    public bool EndAnimResumeTask()
    {
        return done;
    }
}
