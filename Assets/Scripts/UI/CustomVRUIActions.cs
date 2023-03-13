using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CustomVRUIActions : MonoBehaviour
{
    public void OnHover()
    {
        print("VR UI Entered");
    }
    public void OnClick()
    {
        print("VR UI Clicked");
    }
    public void CloseApp()
    {
        Application.Quit();
    }
    public void SetRoomName(string changedRoomName)
    {
        if (FindObjectOfType<RoomNameDDOL>() != null)
        {
            FindObjectOfType<RoomNameDDOL>().RoomNameChanged(changedRoomName);
        }
    }
    public void ToggleCanvas(GameObject canvasObject)
    {
        if (canvasObject.GetComponent<Canvas>().enabled == false)
        {
            canvasObject.GetComponent<Canvas>().enabled = true;
        }
        else
        {
            canvasObject.GetComponent<Canvas>().enabled = false;
        }
    }
    public void ToggleMute()
    {
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
        var audio = GetComponentInParent<PauseMenuToggler>().vrCam.GetComponent<AudioListener>();
        if (audio != null)
        {
            audio.enabled = false;
            text.text = "Unmute";
        }
        else
        {
            audio.enabled = true;
            text.text = "Mute";
        }
    }
    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
