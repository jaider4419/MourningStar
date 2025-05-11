using UnityEngine;

public class Fish : MonoBehaviour
{
    public Vector3 spawnPosition;
    public Vector3 targetPosition;
    public float swimSpeed = 2f;
    public bool destroyAtDestination = true;

    private bool isScared = false;
    private float scaredTimer = 0f;
    private FishSpawner spawner;

    public void SetSpawner(FishSpawner spawnerRef)
    {
        spawner = spawnerRef;
    }

    void Awake()
    {
        if (gameObject.layer != LayerMask.NameToLayer("Fish"))
        {
            Debug.LogWarning("Fish object is not on the Fish layer", this);
        }

        if (GetComponent<Collider>() == null)
        {
            Debug.LogWarning("Fish is missing a Collider component", this);
        }
    }

    void Start()
    {
        transform.position = spawnPosition;
    }

    void Update()
    {
        if (isScared)
        {
            scaredTimer -= Time.deltaTime;
            if (scaredTimer <= 0f)
            {
                if (spawner != null) spawner.RemoveFish(gameObject);
                Destroy(gameObject);
            }
            return;
        }

        if (HasReachedDestination())
        {
            if (destroyAtDestination)
            {
                if (spawner != null) spawner.RemoveFish(gameObject);
                Destroy(gameObject);
            }
            return;
        }

        MoveTowardTarget();
    }

    void MoveTowardTarget()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            swimSpeed * Time.deltaTime
        );
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

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.RemoveFish(gameObject);
        }
    }
}