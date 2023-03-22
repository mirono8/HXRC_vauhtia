using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Fusion.XR.Host.Grabbing;

public class SnapPoint : MonoBehaviour {
    [Tooltip("(IF SpawnObject() IS USED): Object to spawn here as the ungrabbable version of the original.")]
    public GameObject ungrabbableObject;

    [Tooltip("The gameobject that triggers the UnityEvent.")]
    public GameObject myTool;
    public UnityEvent myEvent;

    public bool snapCompleted;
    private void OnTriggerStay(Collider other) {
        if (snapCompleted == false) {
            if (other.GetComponentInParent<Rigidbody>().gameObject == myTool) {
                if (other.GetComponentInParent<PhysicsGrabbable>().currentGrabber == null) { //Do stuff only after grip is released
                    Destroy(other.GetComponentInParent<Rigidbody>().gameObject);
                    myEvent.Invoke();
                    snapCompleted = true;
                }
            }
        }
    }
    public void SpawnObject() {
        var spawnedObj = Instantiate(ungrabbableObject, transform.position, transform.rotation);
    }
}
