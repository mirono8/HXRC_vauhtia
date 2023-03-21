using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion.XR.Host.Grabbing;

public class GrabbableResetter : MonoBehaviour
{
    private PhysicsGrabbable grabbable;
    private Vector3 startPos;
    private Quaternion startRot;

    public bool timerHasBeenSet;
    public float resetTime = 5f;
    public float timeToReset;
    private void Awake() {
        startPos = transform.position;
        startRot = transform.rotation;
    }
    private void Start() {
        grabbable = GetComponent<PhysicsGrabbable>();
    }
    // Update is called once per frame
    void Update()
    {
        if (grabbable.currentGrabber != null) {
            timerHasBeenSet = false;
        }

        if (transform.position.y <= 0.4f && grabbable.currentGrabber == null) {
            if (timerHasBeenSet == false) {
                timeToReset = Time.time + resetTime;
                timerHasBeenSet = true;
            }
            else {
                if (Time.time >= timeToReset) {
                    timerHasBeenSet = false;
                    Respawn();
                }
            }
        }
    }
    public void Respawn() {
        transform.SetPositionAndRotation(startPos, startRot);
    }
}
