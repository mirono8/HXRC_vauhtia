using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimations : MonoBehaviour 
{
    public AI mummo;
    public Animator animator;



    public void GrabAnim()
    {
        animator.SetTrigger("grabTrigger");
    }

    public void DropAnim()
    {

        animator.SetTrigger("dropTrigger");

    }
}
