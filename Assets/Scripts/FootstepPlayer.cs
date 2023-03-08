using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource aSource;

    private float speedModifier = 1.5f;
    private float speed;
    private float time;

    public AI mummo;
    public AudioClipsScriptable footsteps;
    private void OnEnable()
    {
        time = 0f;
    }

    private void Update()
    {
        if (mummo.moveTowardsThis != null)
        {
            if (!mummo.IsMovementNecessary(mummo.moveTowardsThis))
                return;

            time += Time.deltaTime * mummo.movementSpd * speedModifier;

            if (time >= 1f)
            {
                time = time % 1f;

                if (footsteps == null)
                    return;

                var step = footsteps.PickRandom();
                aSource.PlayOneShot(step);
                Debug.Log("mummo step");
            }
        }
    }
}
