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

    private TextMeshProUGUI timeText;
    private TextMeshProUGUI commandsText;
    private TextMeshProUGUI doneCmdsText;
    private TextMeshProUGUI gradeText;

    private void Start()
    {
        timeText = GameObject.Find("Stat0").GetComponentInChildren<TextMeshProUGUI>();
        commandsText = GameObject.Find("Stat1").GetComponentInChildren<TextMeshProUGUI>();
        doneCmdsText = GameObject.Find("Stat2").GetComponentInChildren<TextMeshProUGUI>();
        gradeText = GameObject.Find("Stat3").GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ActivateMenu() {
        // Set the position of the canvas
        gameObject.transform.position = vrCam.transform.position + vrCam.transform.forward * menuDist;
        // Set the rotation of the canvas to match the camera's rotation
        gameObject.transform.rotation = Quaternion.LookRotation(transform.position - vrCam.transform.position);

        timeText.text = Time.time.ToString();
        commandsText.text = commandsGiven.ToString();
        doneCmdsText.text = commandsUnderstood.ToString();
        //calculate end grading
        int score = 1000 - (int)(Time.time % 60) - (5 * (commandsGiven - commandsUnderstood)); //1000:sta v‰hennet‰‰n kuluneet sekunnit ja ohi menneet ohjeet * 5 
        gradeText.text = score.ToString();

        menuBg.SetActive(true);
    }
}
