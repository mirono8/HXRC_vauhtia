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

    private string toolName;
    public bool snapCompleted;

    private void Start()
    {
        this.enabled = false;
    }
    private void OnTriggerStay(Collider other) {
        if (snapCompleted == false && other.CompareTag("Grabbable")) {
            if (other.GetComponentInParent<Rigidbody>().gameObject == myTool) {
                if (other.GetComponentInParent<PhysicsGrabbable>().currentGrabber == null) { //Do stuff only after grip is released
                    Destroy(other.GetComponentInParent<Rigidbody>().gameObject);
                    myEvent.Invoke();  //t�h�n halutaan per��n inspectorissa aitools.setcurrentdemotool ja stringin� sama nimi kuin aitools-listassa
                    snapCompleted = true;
                }
            }
        }
    }
    public void SpawnObject() {
        var spawnedObj = Instantiate(ungrabbableObject, transform.position, transform.rotation);
    }

    private void OnEnable()   //colliderit = snappays vain mummon demo-tilassa
    {
        gameObject.GetComponentInChildren<Collider>().enabled = true;
    }

    private void OnDisable()
    {
        gameObject.GetComponentInChildren<Collider>().enabled = false;
    }
}
