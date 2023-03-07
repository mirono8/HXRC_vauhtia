using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.EventSystems;

public class CustomVRUIMenuController : MonoBehaviour {
    public GameObject raycastObject;
    private LineRenderer lineRenderer;
    private RaycastHit hit;
    private GameObject lastHitObject;
    private bool isClicking = false;

    private void Start() {
        lineRenderer = raycastObject.GetComponent<LineRenderer>();
    }
    private void Update() {
        // Cast a ray from the raycastObject in the forward direction
        if (Physics.Raycast(raycastObject.transform.position, raycastObject.transform.forward, out hit, 10f)) {
            // Check if the hit object has an EventTrigger component
            if (hit.collider.gameObject.GetComponent<EventTrigger>()) {
                // Render the raycast                
                Debug.DrawRay(raycastObject.transform.position, raycastObject.transform.forward * hit.distance, Color.white);

                // Call the Hover method on the hit object
                if (hit.collider.gameObject != lastHitObject) {
                    lastHitObject = hit.collider.gameObject;
                    ExecuteEvents.Execute(lastHitObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
                }

                // Check if the Fire1 button is pressed and call the Click method on the hit object
                if (Input.GetButtonDown("XRI_Right_TriggerButton") ||
                    Input.GetButtonDown("XRI_Left_TriggerButton") ||
                    Input.GetButtonDown("XRI_Right_GripButton") ||
                    Input.GetButtonDown("XRI_Left_GripButton")) {
                    if (!isClicking) {
                        isClicking = true;
                        ExecuteEvents.Execute(lastHitObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                    }
                }
                else {
                    isClicking = false;
                }
                // Set the lineRenderer to show the ray from the raycastObject to the hit point
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, raycastObject.transform.position);
                lineRenderer.SetPosition(1, hit.point);
            }
            else {
                // Call the Exit method on the last hit object if there is one
                if (lastHitObject != null) {
                    ExecuteEvents.Execute(lastHitObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                    lastHitObject = null;
                }
                isClicking = false;
            }

        }
        else {
            // Call the Exit method on the last hit object if there is one
            if (lastHitObject != null) {
                ExecuteEvents.Execute(lastHitObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
                lastHitObject = null;
            }
            isClicking = false;

            // Set the lineRenderer to not show the ray if it doesn't hit anything
            lineRenderer.enabled = false;
        }
    }
}
