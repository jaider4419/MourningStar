using UnityEngine;

public class LighthouseView : MonoBehaviour
{
    [Header("View Settings")]
    [SerializeField] private float lookSpeed = 2.0f;
    [SerializeField] private float maxLookAngle = 45.0f;
    [SerializeField] private float keyboardLookSpeed = 30.0f;

    [Header("Control Options")]
    [SerializeField] private bool useMouse = true;
    [SerializeField] private bool useKeyboard = true;

    // These store the initial rotation set in the Editor
    private float initialTilt;
    private float initialYaw;
    private float currentYaw;

    void Start()
    {
        // Store the initial rotation values from whatever you set in Scene view
        initialTilt = transform.localEulerAngles.x;
        initialYaw = transform.localEulerAngles.y;

        // Initialize current yaw (left/right rotation)
        currentYaw = 0f;

        // Apply the initial rotation
        UpdateRotation();
    }

    void Update()
    {
        // Mouse look (only when right mouse button is held)
        if (useMouse && Input.GetMouseButton(1)) // Right mouse button
        {
            currentYaw += Input.GetAxis("Mouse X") * lookSpeed;
            currentYaw = Mathf.Clamp(currentYaw, -maxLookAngle, maxLookAngle);
        }

        // Keyboard look
        if (useKeyboard)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                currentYaw -= keyboardLookSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                currentYaw += keyboardLookSpeed * Time.deltaTime;
            }
            currentYaw = Mathf.Clamp(currentYaw, -maxLookAngle, maxLookAngle);
        }

        // Apply the rotation
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        // Apply the initial tilt (set in Editor) plus any yaw rotation
        transform.localEulerAngles = new Vector3(
            initialTilt,
            initialYaw + currentYaw,
            0
        );
    }

    // Call this to reset view to initial position
    public void ResetView()
    {
        currentYaw = 0f;
        UpdateRotation();
    }
}