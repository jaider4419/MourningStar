using UnityEngine;
using System.Collections.Generic;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    public float spawnRate = 3f;
    public int maxFish = 5;
    public float spawnPointCooldown = 5f;
    [SerializeField] private float nightDuration = 120f;
    public GameObject winPanel;
    public GameObject losePanel;

    [System.Serializable]
    public class SpawnPoint
    {
        public Transform transform;
        public Vector3 lookDirection;
    }

    public SpawnPoint[] spawnPoints;

    private List<GameObject> spawnedFish = new List<GameObject>();
    private Dictionary<Transform, float> spawnPointCooldowns = new Dictionary<Transform, float>();
    private float nightTimer;
    private bool nightEnded = false;

    void Start()
    {
        nightTimer = nightDuration;

        foreach (var point in spawnPoints)
        {
            spawnPointCooldowns[point.transform] = 0f;
        }

        InvokeRepeating(nameof(SpawnFish), 0f, spawnRate);

        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
    }

    void Update()
    {
        if (nightEnded) return;

        nightTimer -= Time.deltaTime;
        if (nightTimer <= 0f) EndNight();

        foreach (var point in spawnPoints)
        {
            if (spawnPointCooldowns[point.transform] > 0)
            {
                spawnPointCooldowns[point.transform] -= Time.deltaTime;
            }
        }
    }

    void SpawnFish()
    {
        if (fishPrefab == null || spawnedFish.Count >= maxFish) return;

        CleanUpFishList();

        List<SpawnPoint> availablePoints = new List<SpawnPoint>();
        foreach (var point in spawnPoints)
        {
            if (spawnPointCooldowns[point.transform] <= 0)
            {
                availablePoints.Add(point);
            }
        }

        if (availablePoints.Count == 0) return;

        SpawnPoint chosenPoint = availablePoints[Random.Range(0, availablePoints.Count)];
        GameObject newFish = Instantiate(
            fishPrefab,
            chosenPoint.transform.position,
            Quaternion.Euler(chosenPoint.lookDirection)
        );

        Fish fishComponent = newFish.GetComponent<Fish>();
        if (fishComponent != null)
        {
            fishComponent.spawnPosition = chosenPoint.transform.position;
            fishComponent.SetSpawner(this); 
        }

        spawnedFish.Add(newFish);
        spawnPointCooldowns[chosenPoint.transform] = spawnPointCooldown;
    }

    public void RemoveFish(GameObject fish)
    {
        if (spawnedFish.Contains(fish))
        {
            spawnedFish.Remove(fish);
        }
    }

    void CleanUpFishList()
    {
        spawnedFish.RemoveAll(fish => fish == null);
    }

    public void TriggerLoseCondition()
    {
        if (nightEnded) return;

        nightEnded = true;
        CancelInvoke(nameof(SpawnFish));

        foreach (var fish in spawnedFish.ToArray())
        {
            if (fish != null) Destroy(fish);
        }
        spawnedFish.Clear();

        Time.timeScale = 0f;
        if (losePanel != null) losePanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    void EndNight()
    {
        nightEnded = true;
        CancelInvoke(nameof(SpawnFish));
        Time.timeScale = 0f;
        if (winPanel != null) winPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}