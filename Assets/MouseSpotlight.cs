using UnityEngine;
using TMPro;
using System.Collections;

public class StaticSpotlight : MonoBehaviour
{
    [Header("Light Settings")]
    public Light spotlight;
    public LayerMask waterLayer;
    [SerializeField] private LayerMask fishLayer;
    public float heightAboveWater = 5f;
    public float lightScaleMultiplier = 10f;

    [Header("Energy System")]
    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float currentEnergy = 100f;
    [SerializeField] private float energyDrainInterval = 10f;
    [SerializeField] private float energyDrainAmount = 20f;
    public TMP_Text energyText;

    [Header("Fish Detection")]
    public float fishDetectionRadius = 5f;
    public bool debugMode = true;

    private Camera mainCamera;
    private Transform lightContainer;
    private Transform lightRotator;
    private Vector3? lockedPosition = null;
    private bool isLightActive = false;
    private Coroutine drainCoroutine;

    void Start()
    {
        mainCamera = Camera.main;

        lightContainer = new GameObject("LightContainer").transform;
        lightContainer.parent = mainCamera.transform;
        lightContainer.localScale = Vector3.one * lightScaleMultiplier;

        lightRotator = new GameObject("LightRotator").transform;
        lightRotator.parent = lightContainer;
        lightRotator.localRotation = Quaternion.Euler(90, 0, 0);

        if (spotlight != null)
        {
            spotlight.transform.parent = lightRotator;
            spotlight.transform.localPosition = Vector3.zero;
            spotlight.transform.localRotation = Quaternion.identity;
            spotlight.enabled = false;
        }

        if (fishLayer == 0)
        {
            fishLayer = LayerMask.GetMask("Fish");
            Debug.Log("Auto-assigned Fish layer to spotlight");
        }

        currentEnergy = maxEnergy;
        UpdateEnergyUI();
    }

    void Update()
    {
        if (spotlight == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            TryActivateLight();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            DeactivateLight();
        }
    }

    void TryActivateLight()
    {
        if (currentEnergy <= 0) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, waterLayer))
        {
            lockedPosition = hit.point + Vector3.up * heightAboveWater;
            lightContainer.position = lockedPosition.Value;
            spotlight.enabled = true;
            isLightActive = true;

            if (drainCoroutine != null) StopCoroutine(drainCoroutine);
            drainCoroutine = StartCoroutine(EnergyDrainCoroutine());

            ScareFishInArea(lockedPosition.Value);
        }
    }

    void DeactivateLight()
    {
        lockedPosition = null;
        spotlight.enabled = false;
        isLightActive = false;

        if (drainCoroutine != null)
        {
            StopCoroutine(drainCoroutine);
            drainCoroutine = null;
        }
    }

    IEnumerator EnergyDrainCoroutine()
    {
        while (isLightActive && currentEnergy > 0)
        {
            yield return new WaitForSeconds(energyDrainInterval);

            if (!isLightActive) yield break; 

            currentEnergy = Mathf.Max(0, currentEnergy - energyDrainAmount);
            UpdateEnergyUI();

            Debug.Log($"Energy drained! Current: {currentEnergy}%");

            if (currentEnergy <= 0)
            {
                DeactivateLight();
                yield break;
            }
        }
    }

    void UpdateEnergyUI()
    {
        if (energyText != null)
        {
            energyText.text = $"ENERGY: {Mathf.RoundToInt(currentEnergy)}%";
        }
    }

    void ScareFishInArea(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, fishDetectionRadius, fishLayer);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<Fish>(out Fish fish))
            { 
                fish.ScareFish(position);
                if (debugMode) Debug.Log($"Scared fish: {hitCollider.name}");
            }
        }

        if (debugMode)
        {
            Debug.Log($"Detected {hitColliders.Length} fish");
            foreach (var collider in hitColliders)
            {
                Debug.DrawLine(position, collider.transform.position, Color.red, 1f);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (lockedPosition.HasValue)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(lockedPosition.Value, fishDetectionRadius);
        }
    }
}