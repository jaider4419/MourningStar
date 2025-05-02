using UnityEngine;
using UnityEngine.UI;

public class ObjectInspector : MonoBehaviour
{
    [Header("Inspection Settings")]
    public float inspectionDistance = 2f;
    public float rotationSpeed = 10f;
    public LayerMask inspectableLayer;

    [Header("UI References")]
    public GameObject inspectionPanel; // Parent object for all inspection UI
    public Text dialogueText; // Where your dialogue appears
    public Button exitButton; // The X button to close

    [Header("Input Settings")]
    public KeyCode exitKey = KeyCode.Escape;
    public KeyCode rotateKey = KeyCode.Mouse0;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;
    private bool isInspecting = false;
    private Transform currentObject;

    void Start()
    {
        // Setup button listener
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(StopInspecting);
        }

        // Hide UI initially
        if (inspectionPanel != null)
        {
            inspectionPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (isInspecting)
        {
            HandleRotationInput();

            // Optional: Still allow keyboard exit
            if (Input.GetKeyDown(exitKey))
            {
                StopInspecting();
            }
        }
        else if (Time.timeScale > 0)
        {
            HandleInspectionStart();
        }
    }

    void HandleRotationInput()
    {
        if (Input.GetKey(rotateKey))
        {
            float xRot = Input.GetAxis("Mouse X") * rotationSpeed;
            float yRot = Input.GetAxis("Mouse Y") * rotationSpeed;

            currentObject.Rotate(Camera.main.transform.up, -xRot, Space.World);
            currentObject.Rotate(Camera.main.transform.right, yRot, Space.World);
        }
    }

    void HandleInspectionStart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, inspectableLayer))
            {
                StartInspecting(hit.transform);

                // Set dialogue text based on object (customize this part)
                if (dialogueText != null)
                {
                    dialogueText.text = GetDialogueForObject(hit.transform);
                }
            }
        }
    }

    string GetDialogueForObject(Transform obj)
    {
        // Customize this method to return different text for different objects
        return "This is a " + obj.name + ". How interesting!";
    }

    public void StartInspecting(Transform obj)
    {
        currentObject = obj;
        isInspecting = true;
        Time.timeScale = 0f;

        // Save original transform
        originalPosition = obj.position;
        originalRotation = obj.rotation;
        originalParent = obj.parent;

        // Parent to camera
        obj.SetParent(Camera.main.transform);
        obj.localPosition = new Vector3(0, 0, inspectionDistance);
        obj.localRotation = Quaternion.Euler(15f, -20f, 0);

        // Disable physics
        if (obj.TryGetComponent(out Rigidbody rb)) rb.isKinematic = true;
        if (obj.TryGetComponent(out Collider col)) col.enabled = false;

        // Show UI
        if (inspectionPanel != null)
        {
            inspectionPanel.SetActive(true);
        }
    }

    public void StopInspecting()
    {
        Time.timeScale = 1f;

        // Restore object
        if (currentObject != null)
        {
            currentObject.SetParent(originalParent);
            currentObject.position = originalPosition;
            currentObject.rotation = originalRotation;

            if (currentObject.TryGetComponent(out Rigidbody rb)) rb.isKinematic = false;
            if (currentObject.TryGetComponent(out Collider col)) col.enabled = true;
        }

        // Hide UI
        if (inspectionPanel != null)
        {
            inspectionPanel.SetActive(false);
        }

        currentObject = null;
        isInspecting = false;
    }
}