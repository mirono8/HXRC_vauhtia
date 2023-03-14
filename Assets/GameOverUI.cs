using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public GameObject menuBg;
    public GameObject vrCam;
    public float menuDist = 1.1f;

    public TextMeshProUGUI timeText;
    public void ActivateMenu() {
        //var menuRt = menuObj.GetComponent<RectTransform>();

        // Set the position of the canvas
        gameObject.transform.position = vrCam.transform.position + vrCam.transform.forward * menuDist;
        // Set the rotation of the canvas to match the camera's rotation
        gameObject.transform.rotation = Quaternion.LookRotation(transform.position - vrCam.transform.position);

        timeText.text = Time.time.ToString();

        menuBg.SetActive(true);
    }
}
