using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsByTask : MonoBehaviour
{
    //tän voi recyclee

    public static TargetsByTask targetInitInstance { get; private set; }

    public AI mummo;

    public List<string> grabTags;
    public List<string> dropTags;
    public List<string> interactTags;

    /*TaskTargets.GrabTargets tempGrabT;
    TaskTargets.DropTargets tempDropT;
    TaskTargets.InteractTargets tempInteractT;*/

  /*  private void Awake()
    {
        if (targetInitInstance != null && targetInitInstance != this)
        {
            Destroy(this);  //error buildis, fix
        }
        else
        {
            targetInitInstance = this;
        }
    }

    public void InitKahvinkeittoTargets() 
    {
        
        for (int i = 0; i < grabTags.Count; i++)
        {
            tempGrabT = new TaskTargets.GrabTargets();
            
            
            GameObject item = GameObject.FindGameObjectWithTag(grabTags[i]);

            tempGrabT.target = item;

            tempGrabT.name = grabTags[i];

            mummo.grabTargets.Insert(i, tempGrabT);

        }

        for (int i = 0; i < dropTags.Count; i++)
        {
            tempDropT = new TaskTargets.DropTargets();
            try
            {
                GameObject item = GameObject.FindGameObjectWithTag(dropTags[i]);
                tempDropT.target = item;
            }
            catch (System.Exception)
            {
                Debug.Log("No gameobject with tag" + dropTags[i] + " was found");
                break;
            }

            tempDropT.name = dropTags[i];
            

            mummo.dropTargets.Insert(i, tempDropT);

        }

        for (int i = 0; i < interactTags.Count; i++)
        {
            tempInteractT = new TaskTargets.InteractTargets();

            try
            {
                GameObject item = GameObject.FindGameObjectWithTag(interactTags[i]);
                tempInteractT.target = item;
            }
            catch (System.Exception)
            {
                Debug.Log("No gameobject with tag" + interactTags[i] + " was found");
                break;
            }

            tempInteractT.name = interactTags[i];


            mummo.interactTargets.Insert(i, tempInteractT);

        }

    }*/
}
