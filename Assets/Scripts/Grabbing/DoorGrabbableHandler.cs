using Fusion.XR.Host.Grabbing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGrabbableHandler : MonoBehaviour
{
    public Animator hingeAnimCtr;
    [SerializeField] private bool myDoorOpen = false;

    private void Start()
    {
        hingeAnimCtr = GetComponentInParent<Animator>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if (Input.GetButtonDown("XRI_Left_GripButton") || Input.GetButtonDown("XRI_Right_GripButton"))
            {
                //print("opening door!");
                DoorOpenClose();
            }
        }
    }
    public void DoorOpenClose()
    {
        if (myDoorOpen)
        {
            hingeAnimCtr.Play("DoorClose");
            myDoorOpen= false;
        }
        else
        {
            hingeAnimCtr.Play("DoorOpen");
            myDoorOpen= true;
        }
    }
}
