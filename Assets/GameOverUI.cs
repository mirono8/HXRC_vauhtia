using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public GameObject menuBg;
    public GameObject vrCam;
    public float menuDist = 1.1f;

    public int commandsGiven;
    public int commandsUnderstood;
    public int chaosesCaused;

    public TextMeshProUGUI timeText;
    public TextMeshProUGUI commandsText;
    public TextMeshProUGUI doneCmdsText;
    public TextMeshProUGUI chaosText;
    public TextMeshProUGUI feedbackText;

    public void ActivateMenu() {
        // Set the position of the canvas
        gameObject.transform.position = vrCam.transform.position + vrCam.transform.forward * menuDist;
        // Set the rotation of the canvas to match the camera's rotation
        gameObject.transform.rotation = Quaternion.LookRotation(transform.position - vrCam.transform.position);

        

        menuBg.SetActive(true);
    }

    private void Update()
    {
        // Set the position of the canvas
        gameObject.transform.position = vrCam.transform.position + vrCam.transform.forward * menuDist;
        // Set the rotation of the canvas to match the camera's rotation
        gameObject.transform.rotation = Quaternion.LookRotation(transform.position - vrCam.transform.position);

        timeText.text = Time.time.ToString();
        commandsText.text = commandsGiven.ToString();
        doneCmdsText.text = commandsUnderstood.ToString();
        chaosText.text = chaosesCaused.ToString() + " kertaa";
    }
}
