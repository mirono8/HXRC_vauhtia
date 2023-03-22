using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnapPoint : MonoBehaviour {
    [Tooltip("(IF SpawnObject() IS USED): Object to spawn here as the ungrabbable version of the original.")]
    public GameObject ungrabbableObject;

    [Tooltip("The gameobject that triggers the UnityEvent.")]
    public GameObject myTool;
    public UnityEvent myEvent;
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponentInParent<Rigidbody>().gameObject == myTool) {
            Destroy(other.GetComponentInParent<Rigidbody>().gameObject);
            myEvent.Invoke();
        }
    }
    public void SpawnObject() {
        var spawnedObj = Instantiate(ungrabbableObject, transform.position, transform.rotation);
    }
}
