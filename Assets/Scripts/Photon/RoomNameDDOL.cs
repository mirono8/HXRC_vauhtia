using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomNameDDOL : MonoBehaviour
{
    public string customRoomName;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    public void RoomNameChanged(string newRoomName)
    {
        customRoomName = newRoomName;
    }
}
