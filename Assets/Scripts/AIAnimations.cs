using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIAnimations : MonoBehaviour
{
    public AI mummo;
    public Animator animator;

    private bool done = true;
    public bool ikActive = false;
    private float lookAtWeight;

    private float lookDir;

    [SerializeField]
    private float dampVelocity = 0;

    private Vector3 target;

    private bool rotate = false;


    [SerializeField]
    private bool saveOriginalPos = false;

    private Vector3 temp;
    private void Update()
    {
        /*// if (mummo.CalculateAngleVsPlayer() < 90 ) //90f(mummo.mummoHead.rotation.y < 0.45f) && (mummo.mummoHead.rotation.y > -0.45f)
        // {
             if ((mummo.mummoHead.rotation.y > 0.5f) || (mummo.mummoHead.rotation.y < -0.5f))
             {
             //P‰‰ ei k‰‰nny en‰‰
             // Debug.Log(lookAtWeight);
             //  lookAtWeight = 0.5f;
             //lookAtWeight = Mathf.SmoothDamp(lookAtWeight, 0.5f, ref dampVelocity, 3f);  //kinda wonky


                // BodyRotation();
             }
             else
             {
                 // lookAtWeight = Mathf.Lerp(lookAtWeight, 1, Time.deltaTime * 2.5f);

             }
         }
         else
         {

             //lookAtWeight = Mathf.Lerp(lookAtWeight, 0, Time.deltaTime * 2.5f);   
             lookAtWeight = Mathf.SmoothDamp(lookAtWeight, 0, ref dampVelocity, 2f);

         } */

        if (mummo.isListening)
        {
            SetLookTarget(Camera.main.transform.position);
            BodyRotation();

        }

        if (rotate)
        {
            BodyRotation();
        }
    }

    /* private void OnAnimatorIK()
     {
         if (ikActive)
         {
             if (mummo.isListening)
             {

                 animator.SetLookAtWeight(lookAtWeight);
                 animator.SetLookAtPosition(Camera.main.transform.position);

             }


         }
     }*/

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
            animator.SetTrigger("reset");
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

    public void PourAnim()
    {
        done = false;
        animator.SetTrigger("pourTrigger");
    }
    public void ThumbsUpAnim()
    {
        done = false;
        animator.SetTrigger("thumbsTrigger");
    }

    public void SetDone()
    {
        done = true;
        //animator.SetTrigger("reset");
    }


    public void ChangeRotationStatus(bool b)
    {
        rotate = b;
    }
    public void BodyRotation()
    {

        if (!saveOriginalPos)
        {
            saveOriginalPos = true;
            temp = gameObject.transform.forward;
        }
        Vector3 rotationOffset = target - gameObject.transform.position;
        rotationOffset.y = 0;

        lookDir = Vector3.SignedAngle(gameObject.transform.forward, rotationOffset, Vector3.up);
        animator.SetFloat("LookDirection", lookDir);

        if (!((lookDir <= 5f && !(lookDir < -5f)) || (!(lookDir > 5f) && lookDir >= -5f)))
            gameObject.transform.forward += Vector3.Lerp(gameObject.transform.forward, rotationOffset, Time.deltaTime * 1.5f);
                else
            saveOriginalPos = false;
        /*  if (lookDir <= 10f && !(lookDir < -10f) || !(lookDir > 10f) && lookDir >= -10f)
                  lookAtWeight = Mathf.SmoothDamp(lookAtWeight, 1, ref dampVelocity, 2f);
          else
          {
              gameObject.transform.forward += Vector3.Lerp(gameObject.transform.forward, rotationOffset, Time.deltaTime * 1.5f);
            //  lookAtWeight = Mathf.SmoothDamp(lookAtWeight, 0.1f, ref dampVelocity, 2f);   //jostai syyst 180 asteinen p‰‰ ku tekee taskin

          }

          */
    }

    public void CalculateLookDir()
    {
        Vector3 rotationOffset = target - gameObject.transform.position;
        rotationOffset.y = 0;

        lookDir = Vector3.SignedAngle(gameObject.transform.forward, rotationOffset, Vector3.up);
        animator.SetFloat("LookDirection", lookDir);
    }

    public void SetLookTarget(Vector3 t)
    {
        target = t;
        
    }

    public float GetLookDir()
    {
        CalculateLookDir();
        return lookDir;
    }

    public bool FacingLookTarget()  ///triple checkaa t‰‰ antaaks oikeen boolin, saatta olla ettei p‰ivity
    {
        // Debug.Log("Facing target " + target + "? " + ((mummo.anims.GetLookDir() <= 10f && !(mummo.anims.GetLookDir() < -10f)) || (!(mummo.anims.GetLookDir() > 10f) && mummo.anims.GetLookDir() >= -10f)));
        if ((mummo.anims.GetLookDir() <= 5f && !(mummo.anims.GetLookDir() < -5f)) || (!(mummo.anims.GetLookDir() > 5f) && mummo.anims.GetLookDir() >= -5f))
        {
            ChangeRotationStatus(false); return true;
        }
        else
        {
            ChangeRotationStatus(true);
            return false;
        }
    }

    public bool EndAnimResumeTask()
    {
        return done;
    }
}
