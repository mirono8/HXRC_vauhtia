using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimations : MonoBehaviour 
{
    public AI mummo;
    public Animator animator;

    private bool done = false;
    private void Update()
    {
        if(mummo.isListening)
            LookTowardsPlayer(true);
        else
            LookTowardsPlayer(false);
    }

    public void LookTowardsPlayer(bool x)
    {
        if (x)
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
        animator.SetTrigger("grabTrigger");
        //animator.ResetTrigger("grabTrigger");
         
    }

    public void DropAnim()
    {
        done = false;
        animator.SetTrigger("dropTrigger");
        //animator.ResetTrigger("dropTrigger");
    }

    public void SetDone()
    {
        done = true;
    }

    public bool EndAnimResumeTask()
    {
        return done;
    }
}
