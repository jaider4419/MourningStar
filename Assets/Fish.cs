using UnityEngine;

public class Fish : MonoBehaviour
{
    public Vector3 spawnPosition;
    public Vector3 targetPosition;
    public float swimSpeed = 2f;
    public bool destroyAtDestination = true;

    private bool isScared = false;
    private float scaredTimer = 0f;

    void Awake()
    {
        if (gameObject.layer != LayerMask.NameToLayer("Fish"))
        {
        }

        if (GetComponent<Collider>() == null)
        {
        }
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
            scaredTimer -= Time.deltaTime;
            if (scaredTimer <= 0f)
            {
                Destroy(gameObject);
            }
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

        isScared = true;
        scaredTimer = 0.5f;
    }
}