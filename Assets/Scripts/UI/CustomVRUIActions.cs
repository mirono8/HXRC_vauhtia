using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
