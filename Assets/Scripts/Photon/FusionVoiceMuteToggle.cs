using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FusionVoiceMuteToggle : MonoBehaviour
{
    public GameObject muteIndicator;
    private AudioSource speaker;
    public Material offMat;
    public Material onMat;
    private bool indicatorOn = false;
    // Start is called before the first frame update
    void Start()
    {
        speaker = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("XRI_Left_SecondaryButton") || Input.GetButtonDown("XRI_Right_SecondaryButton"))
        {
            speaker.mute = !speaker.mute;
            ToggleIndicatorMat();
        }
    }

    public void ToggleIndicatorMat()
    {
        if (indicatorOn == false)
        {
            muteIndicator.GetComponent<Renderer>().material = onMat;
            indicatorOn = true;
        }
        else
        {
            muteIndicator.GetComponent<Renderer>().material = offMat;
            indicatorOn = false;
        }
    }
}
