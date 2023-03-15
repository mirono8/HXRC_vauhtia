using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cable : Triggerable
{
    private LineRenderer lineRenderer;
    private Vector3 parentStartLoc;

    Vector3 myPos = new Vector3();
    Vector3 nextPos = new Vector3();

    public GameObject startHere;

    float[] lineLength;

    // Transforms to act as start and end markers for the journey.
    public Transform plug;

    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    public float journeyLengthPerVertex;

    private float currentLength;

    public int segments = 0;

    private Vector3 deviation;


    public GameObject outlet;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        



    }

 

    // Move to the target end position.
    void Update()
    {
        if (!triggered)
        {
            Debug.Log("we at the clam");
            Line();
        }

        // Line();*/
    }
    private void Line()  //   korjaa öäöööäö
    {
        
        
            Debug.Log("drawing a line");


            // Distance moved equals elapsed time times speed..
            /*  float distCovered = (Time.time - startTime) * speed;

              // Fraction of journey completed equals current distance divided by total distance.
              float fractionOfJourney = distCovered / journeyLength;

              // Set our position as a fraction of the distance between the markers.
              for (int i = 1; i < lineRenderer.positionCount; i++)
              {

                  if (journeyLengthPerVertex > lineLength[i - 1])
                  {

                      var t = transform;
                      t.localPosition = Vector3.Lerp(lineRenderer.GetPosition(i - 1), lineRenderer.GetPosition(i), fractionOfJourney);
                      lineRenderer.SetPosition(i, t.localPosition);
                  }
              }*/

            lineLength = new float[segments];
            //lineLength = new float[];

            // parentOffset = gameObject.GetComponentsInParent<Transform>()[1].localPosition;


            parentStartLoc = startHere.transform.position;


            /* for (int i = 0; i < segments; i++)
             {
                 lineRenderer.positionCount++;

             }
            */
            journeyLength = Vector3.Distance(startHere.transform.position, outlet.transform.position); //nää oli local, testing
            // journeyLengthPerVertex = journeyLength / segments;

            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                deviation = new Vector3(Random.Range(0.01f, 0.05f), Random.Range(0.01f, 0.05f));

                lineRenderer.SetPosition(0, parentStartLoc);
                myPos = lineRenderer.GetPosition(i);
                Debug.Log(myPos + " mypos at " + i);
                nextPos = Vector3.MoveTowards(myPos, outlet.transform.position + deviation, journeyLengthPerVertex);

                /* GameObject instancedObj;
                 Debug.Log(cableSegmentPrefab.transform.rotation.z + " prefab Z");
                 instancedObj = Instantiate(cableSegmentPrefab, myPos, Quaternion.identity*cableSegmentPrefab.transform.localRotation);
                 */

                if (i < segments)
                {
                    if (journeyLength > currentLength)
                    {
                        if (lineRenderer.positionCount < segments && (Vector3.Distance(myPos, outlet.transform.position) > 0.08f))
                        {
                            lineRenderer.positionCount++;

                            lineRenderer.SetPosition(i + 1, nextPos);

                            /* Vector3 line = Vector3.MoveTowards(myPos, nextPos, 0);
                             Debug.DrawLine(myPos, nextPos, Color.red, 200);
                             var iy = instancedObj.GetComponentsInChildren<Transform>()[1].localScale;
                             var ix = instancedObj.GetComponentsInChildren<Transform>()[1].localRotation;
                             Debug.Log(ix.z + "Z axis");*/
                            lineLength[i] = Vector3.Distance(myPos, nextPos);
                            /* instancedObj.GetComponentsInChildren<Transform>()[1].localPosition += iy / 2;
                             iy.y = lineLength[i] / 2;

                             ix.x = myPos.z;
                             instancedObj.GetComponentsInChildren<Transform>()[1].localScale = iy;

                             instancedObj.GetComponentsInChildren<Transform>()[1].localRotation = ix;
                            */

                            currentLength += lineLength[i];
                            Debug.Log(currentLength + " current length");
                        }
                        else
                        {
                            lineRenderer.SetPosition(i, outlet.transform.position);
                            Debug.Log("stopped printing cable");
                            break;
                        }
                    }
                else
                {
                    lineRenderer.SetPosition(i, outlet.transform.position);
                    Debug.Log("stopped printing cable");
                    break;
                }
            }
            }
        

        triggered = true;

    }
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
