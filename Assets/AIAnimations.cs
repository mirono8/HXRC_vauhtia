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
       /* if(mummo.isListening)
            LookTowardsPlayer();*/
        
    }

    
    public void LookTowardsPlayer()
    {
           mummo.mummoHead.LookAt(Camera.main.transform.position);      
        
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
            animator.SetTrigger("reset"); // t‰‰ j‰tt‰‰ mummon jumii
            //SetDone(); 
        }//animator.ResetTrigger("dropTrigger");
    }

    public void ActionAnim()
    {
        done = false;
        animator.SetTrigger("actionTrigger");
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
