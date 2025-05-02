using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float rotationSmoothTime = 0.1f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    [Header("Character Controller Settings")]
    public float slopeLimit = 45f;
    public float stepOffset = 0.3f;
    public float skinWidth = 0.08f;
    public float minMoveDistance = 0.001f;
    public float centerY = 1f;
    public float radius = 0.5f;
    public float height = 2f;

    [Header("References")]
    public CharacterController controller;
    public Transform cameraTransform;
    public Animator animator;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private float turnSmoothVelocity;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        // Initialize character controller settings
        controller.slopeLimit = slopeLimit;
        controller.stepOffset = stepOffset;
        controller.skinWidth = skinWidth;
        controller.minMoveDistance = minMoveDistance;
        controller.center = new Vector3(0, centerY, 0);
        controller.radius = radius;
        controller.height = height;
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Movement input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Handle animations
        bool isMoving = direction.magnitude >= 0.1f;
        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        // Movement logic
        if (direction.magnitude >= 0.1f)
        {
            // Calculate target angle based on camera direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Calculate movement direction
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetTrigger("Jump");
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}