using UnityEngine;

public class KrakenSpotlight : MonoBehaviour
{
    [SerializeField] private LayerMask krakenLayer;
    [SerializeField] private float detectionRadius = 15f;
    [SerializeField] private float hitCooldown = 0.5f;

    private Light spotlight;
    private float lastHitTime;
    private bool isActive = false;

    void Awake()
    {
        spotlight = GetComponent<Light>();
        lastHitTime = -hitCooldown; 
    }

    void Update()
    {
        if (isActive && Time.time - lastHitTime >= hitCooldown)
        {
            CheckForTentacles();
        }
    }

    public void SetLightActive(bool active)
    {
        isActive = active;
        spotlight.enabled = active;
    }

    void CheckForTentacles()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, krakenLayer);

        foreach (Collider hit in hits)
        {
            if (IsTentacleVisible(hit.transform))
            {
                hit.GetComponent<KrakenTentacle>()?.NotifyHit();
                lastHitTime = Time.time;
                break;
            }
        }
    }

    bool IsTentacleVisible(Transform tentacle)
    {
        RaycastHit hit;
        Vector3 direction = tentacle.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out hit, detectionRadius))
        {
            return hit.collider.CompareTag("KrakenTentacle");
        }
        return false;
    }
}