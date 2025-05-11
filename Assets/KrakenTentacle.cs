using UnityEngine;

public class KrakenTentacle : MonoBehaviour
{
    [SerializeField] private KrakenController krakenController;
    [SerializeField] private int tentacleIndex;

    public void NotifyHit()
    {
        krakenController?.LightHitTentacle(this.transform);
    }

    void OnValidate()
    {
        if (krakenController == null)
            krakenController = GetComponentInParent<KrakenController>();
    }
}