using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [Tooltip("Where the mirror faces")]
    public Transform target;

    [Tooltip("Rotator here")]
    public Transform mirrorRotation;
    void Update()
    {
        LookAt();
    }

    public void LookAt()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction);

        rotation.eulerAngles = transform.eulerAngles - rotation.eulerAngles;

        mirrorRotation.localRotation = rotation;
    }

    public void GetPlayerTransform(Transform t)
    {
        target = t;
    }
}
