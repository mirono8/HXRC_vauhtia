using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AITools : MonoBehaviour
{
    public bool toolIsActive;

    [System.Serializable]
    public class Tools
    {
        public string toolName;

        public GameObject toolObj;

        public bool toolLearned;
    }

    public List<Tools> tools;

    /*public GameObject SetUpTool(string toolName, Transform placeHere)  //Anna toolseille kategoria ett‰ onko one time deal forget about it vai joku mik‰ j‰‰ mummolle k‰teen muita tekemisi‰ varten?
    {
        for (int i = 0; i < tools.Count; i++)   /// t‰n koko paskan vois muuttaa vaa enablecomponent
        {
            if (tools[i].toolName == toolName)
            {
                if (tools[i].toolLearned)
                {
                    GameObject iObj;
                    iObj = Instantiate(tools[i].toolObj);
                    
                    iObj.transform.parent = placeHere;

                    iObj.transform.localPosition = new Vector3(0,0,0);
                    
                    iObj.transform.localScale = new Vector3(1,1,1);

                    return iObj;
                }
                else
                {
                    Debug.Log("Tool " + toolName.ToLower() + " usage not learned");
                    return null; // t‰ss‰ siirry oppimaan maybe(?)
                }
            }
            
        }
        return null;
    }*/

    public void SetupTool(string tool)
    {
        for (int i = 0; i < tools.Count; i++)
        {
            if (tools[i].toolName == tool)
            {
                if (tools[i].toolLearned)
                {
                    if (!toolIsActive)
                    {
                        tools[i].toolObj.SetActive(true);
                        toolIsActive = true;
                    }
                    else
                    {
                        VanishTools();
                        tools[i].toolObj.SetActive(true);
                    }
                }
                else
                {
                    Debug.Log(tools[i].toolName + " not learned");
                    //T‰ss‰ VR esimerkki tool k‰yttˆ juttu
                }
            }
        }
    }

    public void VanishTools()
    {
        for (int i = 0; i < tools.Count; i++)
        {
            if (tools[i].toolObj.activeSelf == true)
            {
                tools[i].toolObj.SetActive(false);
            }
        }

        toolIsActive = false;
    }
}
