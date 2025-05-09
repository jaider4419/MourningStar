using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaticSpotlight : MonoBehaviour
{
    public Light spotlight;
    public LayerMask waterLayer;
    [SerializeField] private LayerMask fishLayer;
    public float heightAboveWater = 5f;
    public float lightScaleMultiplier = 10f;
    public Image[] batteryBars;
    public TMP_Text energyText;
    public float fishDetectionRadius = 10f;
    public float batteryDrainInterval = 3f; // New configurable variable

    private Camera mainCamera;
    private Transform lightContainer;
    private Transform lightRotator;
    private Vector3? lockedPosition = null;
    private bool isLightActive = false;
    private float continuousLightTime = 0f;
    private int currentBatteryIndex = 5;

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

        if (fishLayer == 0) fishLayer = LayerMask.GetMask("Fish");
        currentBatteryIndex = batteryBars.Length - 1;
        UpdateEnergyUI();
    }

    void Update()
    {
        if (spotlight == null) return;

        if (Input.GetMouseButtonDown(0)) TryActivateLight();
        else if (Input.GetMouseButtonUp(0)) DeactivateLight();

        if (isLightActive)
        {
            continuousLightTime += Time.deltaTime;
            if (continuousLightTime >= batteryDrainInterval) 
            {
                DrainBattery();
                continuousLightTime = 0f;
            }
        }
    }

    void DrainBattery()
    {
        if (currentBatteryIndex < 0) return;
        batteryBars[currentBatteryIndex].gameObject.SetActive(false);
        currentBatteryIndex--;
        UpdateEnergyUI();

        if (currentBatteryIndex < 0)
        {
            DeactivateLight();
            FishSpawner fishSpawner = FindObjectOfType<FishSpawner>();
            if (fishSpawner != null)
            {
                fishSpawner.TriggerLoseCondition();
            }
        }
    }

    void UpdateEnergyUI()
    {
        if (energyText != null)
        {
            float energyPercent = ((float)(currentBatteryIndex + 1) / batteryBars.Length) * 100f;
            energyText.text = $"{Mathf.RoundToInt(energyPercent)}%";
        }
    }

    void TryActivateLight()
    {
        if (currentBatteryIndex < 0) return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, waterLayer))
        {
            lockedPosition = hit.point + Vector3.up * heightAboveWater;
            lightContainer.position = lockedPosition.Value;
            spotlight.enabled = true;
            isLightActive = true;
            ScareFishInArea(lockedPosition.Value);
        }
    }

    void DeactivateLight()
    {
        spotlight.enabled = false;
        isLightActive = false;
    }

    void ScareFishInArea(Vector3 position)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, fishDetectionRadius, fishLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<Fish>(out Fish fish))
            {
                fish.ScareFish(position);
            }
        }
    }
}