using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimations : MonoBehaviour 
{
    public AI mummo;
    public Animator animator;

    private bool done = false;

    private void OnAnimatorIK()
    {

        if (mummo.isListening && (mummo.CalculateHeadAngle() < 70f))
        {
            
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(Camera.main.transform.position);
        }
        else
        {
            animator.SetLookAtWeight(0);
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
