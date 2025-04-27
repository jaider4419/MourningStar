using UnityEngine;

public class Fish : MonoBehaviour
{
    [Header("Movement Path")]
    public Vector3 spawnPosition;
    public Vector3 targetPosition;
    public float swimSpeed = 2f;

    [Header("Behavior")]
    public bool destroyAtDestination = true;
    public float scaredSpeedMultiplier = 3f;
    public float scaredDuration = 2f;
    public float scaredRotationSpeed = 5f;

    private bool isScared = false;
    private float scaredTimer = 0f;
    private Vector3 scaredDirection;
    private float originalSpeed;

    void Awake()
    {
        if (gameObject.layer != LayerMask.NameToLayer("Fish"))
        {
            Debug.LogWarning($"Fish {name} is not on Fish layer!", this);
        }

        if (GetComponent<Collider>() == null)
        {
            Debug.LogError($"Fish {name} has no collider!", this);
        }

        originalSpeed = swimSpeed;
    }

    void Start()
    {
        transform.position = spawnPosition;
        FaceTarget();
    }

    void Update()
    {
        if (isScared)
        {
            ScaredBehavior();
            return;
        }

        if (HasReachedDestination())
        {
            if (destroyAtDestination)
            {
                Destroy(gameObject);
            }
            return;
        }

        MoveTowardTarget();
    }

    void FaceTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void MoveTowardTarget()
    {
        float step = swimSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    bool HasReachedDestination()
    {
        return Vector3.Distance(transform.position, targetPosition) < 0.5f;
    }

    public void ScareFish(Vector3 lightPosition)
    {
        if (isScared) return;

        scaredDirection = (transform.position - lightPosition).normalized;
        if (scaredDirection == Vector3.zero)
        {
            scaredDirection = transform.forward;
        }
        scaredDirection.y = 0;
        scaredDirection.Normalize();

        isScared = true;
        scaredTimer = scaredDuration;
        swimSpeed = originalSpeed * scaredSpeedMultiplier;

        // Set new escape target
        targetPosition = transform.position + scaredDirection * 10f;
        FaceTarget();

        Debug.Log($"{name} is scared! Swimming to {targetPosition}");
    }

    void ScaredBehavior()
    {
        scaredTimer -= Time.deltaTime;

        if (scaredTimer <= 0)
        {
            Destroy(gameObject);
            return;
        }

        if (scaredDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(scaredDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                scaredRotationSpeed * Time.deltaTime
            );
        }

        transform.position += scaredDirection * swimSpeed * Time.deltaTime;
    }
}