using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PanicButton : MonoBehaviour
{
    public UnityEvent onButtonPressed;
    private Animator animr;
    
    void Start()
    {
        animr = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        animr.Play("ButtonPress");
        onButtonPressed.Invoke();
    }
}
