using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBooleanHandler : MonoBehaviour
{
    public AITools aiTools;

    public bool wasEmpty = true;
    [System.Serializable]
    public class CommonBools
    {
        public string name;
        public bool value;
        public string tool;

    }

    [System.Serializable]
    public class ToolBools
    {
        public string name;
        public bool value;
    }

    public List<CommonBools> common;
    public List<ToolBools> tools;

    public void SetTrue(string name)
    {
        for (int i = 0; i < common.Count; i++)
        {
            if (common[i].name == name)
            {
                common[i].value = true;
                wasEmpty = false;
            }
        }

        for(int x = 0; x < tools.Count; x++)
        {
            if (tools[x].name == name)
            {
                tools[x].value = true;
                wasEmpty = false;
            }
        }
    }

    public void NullBools()
    {
        for (int i = 0; i < common.Count; i++)
        {
            common[i].value = false;
        }

        for (int x = 0; x < tools.Count; x++)
        {
            tools[x].value = false;
        }

        wasEmpty = true;
    }

    public bool IsThisTrue(string name)
    {
        for (int i = 0; i < common.Count; i++)
        {
            if (common[i].name == name)
            {
                if (common[i].value == true)
                    return true;
            }
        }

        for (int x = 0; x < tools.Count; x++)
        {
            if (tools[x].name == name)
            {
                if (tools[x].value == true)
                    return true;
            }
        }

        return false;
    }

    public bool ToolLearned(string toolName)
    {
        for (int i = 0; i < tools.Count; i++)
        {
            if (aiTools.tools[i].toolName == toolName)
            {
                if(aiTools.tools[i].toolLearned == true)
                    return true;
            }
        }
        return false;
    }
}

