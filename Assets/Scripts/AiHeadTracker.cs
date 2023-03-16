using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiHeadTracker : MonoBehaviour
{
    public AI mummo;
    public Animator animator;

    private void OnAnimatorIK()
    {

        if (mummo.isListening)
        {
            animator.SetLookAtWeight(1);
            animator.SetLookAtPosition(Camera.main.transform.position);
        }
        else
        {
            animator.SetLookAtWeight(0);
        }
    }
}
