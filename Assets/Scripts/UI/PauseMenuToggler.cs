using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenuToggler : MonoBehaviour
{
    public GameObject menuBg;
    public GameObject vrCam;
    public float menuDisableDist = 5f;
    public bool menuActive = false;
    public float menuDist = 1.1f;

    void Update()
    {
        if (Input.GetButtonDown("XRI_Left_MenuButton")) //|| Input.GetButtonDown("XRI_Right_PrimaryButton"))
        {
            if (menuActive == false)
            {
                ActivateMenu();
            }
            else
            {
                DisableMenu();
            }
        }
        if (menuActive == true)
        {
            if (Vector3.Distance(gameObject.transform.position, vrCam.transform.position) > menuDisableDist)
            {
                DisableMenu();
            }
        }
    }
    public void ActivateMenu()
    {
        //var menuRt = menuObj.GetComponent<RectTransform>();

        // Set the position of the canvas
        gameObject.transform.position = vrCam.transform.position + vrCam.transform.forward * menuDist;
        // Set the rotation of the canvas to match the camera's rotation
        gameObject.transform.rotation = Quaternion.LookRotation(transform.position - vrCam.transform.position);

        // menuRt.transform.position = vrCam.transform.position + new Vector3(0, 0, 1);
        // menuRt.transform.rotation = vrCam.transform.rotation;
        menuBg.SetActive(true);
        menuActive = true;
    }
    public void DisableMenu()
    {
        menuBg.SetActive(false);
        menuActive = false;
    }
}
