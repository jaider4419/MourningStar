using UnityEngine;

public class ObjectInspector : MonoBehaviour
{
    [Header("Inspection Settings")]
    public float inspectionDistance = 2f;
    public float rotationSpeed = 10f;
    public LayerMask inspectableLayer;

    [Header("Input Settings")]
    public KeyCode exitKey = KeyCode.Escape;
    public KeyCode rotateKey = KeyCode.Mouse0;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;
    private bool isInspecting = false;
    private Transform currentObject;

    void Update()
    {
        if (isInspecting)
        {
            // Rotation isn't affected by timeScale when using direct input
            if (Input.GetKey(rotateKey))
            {
                float xRot = Input.GetAxis("Mouse X") * rotationSpeed;
                float yRot = Input.GetAxis("Mouse Y") * rotationSpeed;

                currentObject.Rotate(Camera.main.transform.up, -xRot, Space.World);
                currentObject.Rotate(Camera.main.transform.right, yRot, Space.World);
            }

            if (Input.GetKeyDown(exitKey))
            {
                StopInspecting();
            }
        }
        else if (Time.timeScale > 0) // Only allow clicks when game isn't frozen
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, inspectableLayer))
                {
                    StartInspecting(hit.transform);
                }
            }
        }
    }

   public void StartInspecting(Transform obj)
    {
        currentObject = obj;
        isInspecting = true;

        // Freeze the entire game
        Time.timeScale = 0f;

        // Save original transform
        originalPosition = obj.position;
        originalRotation = obj.rotation;
        originalParent = obj.parent;

        // Parent to camera with slight angle
        obj.SetParent(Camera.main.transform);
        obj.localPosition = new Vector3(0, 0, inspectionDistance);
        obj.localRotation = Quaternion.Euler(15f, -20f, 0);

        // Disable physics
        if (obj.TryGetComponent(out Rigidbody rb)) rb.isKinematic = true;
        if (obj.TryGetComponent(out Collider col)) col.enabled = false;
    }

    public void StopInspecting()
    {
        // Unfreeze the game
        Time.timeScale = 1f;

        // Restore object
        currentObject.SetParent(originalParent);
        currentObject.position = originalPosition;
        currentObject.rotation = originalRotation;

        if (currentObject.TryGetComponent(out Rigidbody rb)) rb.isKinematic = false;
        if (currentObject.TryGetComponent(out Collider col)) col.enabled = true;

        currentObject = null;
        isInspecting = false;
    }
}