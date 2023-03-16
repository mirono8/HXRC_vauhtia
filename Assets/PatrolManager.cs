using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolManager : MonoBehaviour
{
    [SerializeField]
    private Transform pointsParent;

    public List<Transform> points;

    private NavMeshAgent agent;

    private AI mummo;

    private bool isPatrolling;

    private void Start()
    {
        
        var temp = GameObject.FindGameObjectWithTag("Mummo");
        agent = temp.GetComponent<NavMeshAgent>();
        mummo = temp.GetComponent<AI>();

        for (int i = 0; i < pointsParent.childCount; i++)
        {
            points.Add(pointsParent.GetChild(i).transform);
        }
    }
    private void Update()
    {
    }
    public IEnumerator Patrol()
    {
        if (isPatrolling)
        {
            int i = Random.Range(0, points.Count);

            if (mummo.IsMovementNecessary(points[i]))
            {
                mummo.anims.WalkAnim(true);

                yield return new WaitUntil(mummo.CloseEnough);

                mummo.anims.WalkAnim(false);
            }

            mummo.anims.ActionAnim();

            if (Random.Range(0, 5) == 2)
                mummo.mummoDialog.Whoops(); yield return new WaitUntil(mummo.anims.EndAnimResumeTask);

            isPatrolling = false;

        }

        mummo.isListening = true;
    }

    public void StartPatrol()
    {
        isPatrolling = true;
        StartCoroutine(Patrol());
        
    }
}
