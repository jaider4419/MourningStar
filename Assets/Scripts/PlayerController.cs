using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 0f;
    public Animator animator;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        // Movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement vector
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // Update isMoving parameter based on movement speed
        bool isMoving = movement.magnitude > 0;

        // Update animation parameters
        animator.SetBool("IsMoving", isMoving);
        animator.SetFloat("Horizontal", horizontalInput);
        animator.SetFloat("Vertical", verticalInput);

        // Move the character
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}