using UnityEngine;

public class Fish : MonoBehaviour
{
    public Vector3 spawnPosition;
    public Vector3 targetPosition;
    public float swimSpeed = 2f;
    public bool destroyAtDestination = true;
    public float scaredSpeedMultiplier = 3f;
    public float scaredDuration = 2f;

    [Header("Swim Behavior")]
    public float swimJitterAmount = 0.5f;
    public float swimJitterFrequency = 2f;
    public float tailWagSpeed = 5f;
    public float tailWagAmount = 15f;
    public float smoothTurnSpeed = 2f;

    private bool isScared = false;
    private float scaredTimer = 0f;
    private Vector3 scaredDirection;
    private float originalSpeed;
    private float currentJitter;
    private float nextJitterTime;
    private float wagOffset;
    private Vector3 currentDirection;

    void Awake()
    {
        originalSpeed = swimSpeed;
        LockRotation();
        currentDirection = (targetPosition - transform.position).normalized;
    }

    void Update()
    {
        if (isScared)
        {
            ScaredMovement();
            return;
        }

        NormalMovement();
        TailWagAnimation();
    }

    void NormalMovement()
    {
        // Smooth directional changes
        Vector3 targetDir = (targetPosition - transform.position).normalized;
        currentDirection = Vector3.Slerp(currentDirection, targetDir, smoothTurnSpeed * Time.deltaTime);

        // Jittery forward movement
        if (Time.time > nextJitterTime)
        {
            currentJitter = Random.Range(-swimJitterAmount, swimJitterAmount);
            nextJitterTime = Time.time + (1f / swimJitterFrequency);
        }

        // Apply movement
        Vector3 moveDirection = currentDirection + (transform.right * currentJitter);
        transform.position += moveDirection * swimSpeed * Time.deltaTime;

        // Face movement direction with Y-lock
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(moveDirection);
            Vector3 euler = targetRot.eulerAngles;
            euler.y = 265.782f;
            transform.rotation = Quaternion.Euler(euler.x, euler.y, wagOffset);
        }

        if (Vector3.Distance(transform.position, targetPosition) < 0.5f && destroyAtDestination)
        {
            Destroy(gameObject);
        }
    }

    void ScaredMovement()
    {
        scaredTimer -= Time.deltaTime;
        if (scaredTimer <= 0)
        {
            Destroy(gameObject);
            return;
        }

        // Erratic scared movement
        Vector3 scaredMove = scaredDirection +
                           (transform.right * Mathf.Sin(Time.time * 20f) * 0.3f +
                           (transform.up * Mathf.Cos(Time.time * 15f) * 0.2f;

        transform.position += scaredMove * swimSpeed * Time.deltaTime;

        // Frantic rotation
        float scaredWag = Mathf.Sin(Time.time * 25f) * 30f;
        transform.rotation = Quaternion.Euler(0, 265.782f, scaredWag);
    }

    void TailWagAnimation()
    {
        wagOffset = Mathf.Sin(Time.time * tailWagSpeed) * tailWagAmount;
        transform.Rotate(0, 0, wagOffset - transform.rotation.eulerAngles.z);
    }

    public void ScareFish(Vector3 lightPosition)
    {
        if (isScared) return;

        scaredDirection = (transform.position - lightPosition).normalized;
        scaredDirection.y = 0;
        scaredDirection.Normalize();

        isScared = true;
        scaredTimer = scaredDuration;
        swimSpeed = originalSpeed * scaredSpeedMultiplier;
    }
}