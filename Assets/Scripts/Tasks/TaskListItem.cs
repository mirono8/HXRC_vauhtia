using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TaskListItem //Body for task in a list, contains a list for steps
{
    public string t_name;

    [Tooltip("How many steps have been completed, should match with stepCompletionOrder most of the time in checks")]
    public int t_stepsComplete;

    [Tooltip("How many steps there are in total in this task")]
    public int t_stepTotal;

    [Tooltip("Is this task done?")]
    public bool t_isCompleted;

   
    [System.Serializable]
    public class Steps
    {
        [HideInInspector]
        public string stepName;

        [SerializeField]
        [Tooltip("Index of this step, for clarity")]
        public int myIndex;

        [Tooltip("The order this step was completed in")]
        public int stepCompletionOrder;

        [Tooltip("Is this step done?")]
        public bool isCompleted;

        [Tooltip("True = doesn't have to be completed, False = has to be completed")]
        public bool isOptional;

        [Tooltip("True = requiredSteps have to be done first, False = can be done in any order (other steps may require this step)")]
        public bool rigid_order;

        [Tooltip("Can the AI complete this Ûn it's own?")]
        public bool isLearned = false;

        [Tooltip("Final step before task is complete")]
        public bool isFinal = false;

        public List<int> requiredSteps;
        //Tee joka stepille (jos rigid_order==true) lista integerej‰, jotka viittavat niiden steppien indexiin stepsListassa, joiden pit‰‰ olla completed ennenkuin t‰m‰n stepin voi suorittaa
    }

    [Tooltip("List of steps in a task")]
    public List<Steps> stepsList;
}
