using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoomsdayClock : MonoBehaviour
{
    [SerializeField]
    private float timer = 0.0f;

    [SerializeField]
    private bool causingProblems = false;

    private AI mummo;

    public float triggerSeconds;

    public GameOverUI endScreen;
    private void Start()
    {
        var temp = GameObject.FindGameObjectWithTag("Mummo");
        mummo = temp.GetComponent<AI>();
        endScreen = GameObject.Find("GameOverCanvas").GetComponent<GameOverUI>();
    }
    void Update()
    {
        timer += Time.deltaTime;

        Countdown();
    }

    public void Countdown()
    {
        if (!mummo.isListening)
        {
            causingProblems = false;
            timer = 0.0f;
        }
        else if (timer > triggerSeconds && !causingProblems)
        {
            causingProblems = true;
            
            mummo.CreateChaos();
            mummo.isListening = false;

            endScreen.chaosesCaused++;
        }
    }
}
