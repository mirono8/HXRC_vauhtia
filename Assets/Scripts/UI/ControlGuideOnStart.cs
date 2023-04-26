using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlGuideOnStart : MonoBehaviour
{
    public GameObject vrCam;

    private void Start()
    {
        gameObject.transform.position = vrCam.transform.position + vrCam.transform.forward * 1f;
        gameObject.transform.position = new Vector3(transform.position.x, 1.22f, transform.position.z);
        
    }
    private void Update()
    {
        gameObject.transform.rotation = Quaternion.LookRotation(transform.position - vrCam.transform.position);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
