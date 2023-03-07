using UnityEngine;

public class SmoothFollow : MonoBehaviour {
    // The target transform to follow
    public Transform targetTransform;

    // The time it takes to smooth the movement
    public float smoothTime = 0.3f;

    // The current velocity of the movement
    private Vector3 velocity;

    // The current rotation of the movement
    private Quaternion rotation;

    void Update() {
        // Calculate the smoothed position of the camera
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref velocity, smoothTime);

        // Update the camera position
        transform.position = smoothedPosition;

        // Calculate the smoothed rotation of the camera
        Quaternion smoothedRotation = Quaternion.Slerp(transform.rotation, targetTransform.rotation, smoothTime * Time.deltaTime);

        // Update the camera rotation
        transform.rotation = smoothedRotation;
    }
}
