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
    public Transform[] spawnPoints;

    private List<GameObject> spawnedFish = new List<GameObject>();
    private Dictionary<Transform, float> spawnPointCooldowns = new Dictionary<Transform, float>();
    private float nightTimer;
    private bool nightEnded = false;

    void Start()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            return;
        }

        nightTimer = nightDuration;

        foreach (var spawnPoint in spawnPoints)
        {
            spawnPointCooldowns[spawnPoint] = 0f;
        }

        InvokeRepeating(nameof(SpawnFish), 0f, spawnRate);

        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }
    }

    void Update()
    {
        if (nightEnded) return;

        nightTimer -= Time.deltaTime;

        if (nightTimer <= 0f)
        {
            EndNight();
        }

        List<Transform> keys = new List<Transform>(spawnPointCooldowns.Keys);
        foreach (var spawnPoint in keys)
        {
            if (spawnPointCooldowns[spawnPoint] > 0)
            {
                spawnPointCooldowns[spawnPoint] -= Time.deltaTime;
            }
        }
    }

    public void TriggerLoseCondition()
    {
        if (nightEnded) return;

        nightEnded = true;
        CancelInvoke(nameof(SpawnFish));

        Time.timeScale = 0f;

        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void SpawnFish()
    {
        if (fishPrefab == null)
        {
            return;
        }

        CleanUpFishList();

        if (spawnedFish.Count >= maxFish) return;

        List<Transform> availableSpawnPoints = new List<Transform>();
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPointCooldowns[spawnPoint] <= 0)
            {
                availableSpawnPoints.Add(spawnPoint);
            }
        }

        if (availableSpawnPoints.Count == 0) return;

        Transform randomSpawnPoint = availableSpawnPoints[Random.Range(0, availableSpawnPoints.Count)];
        GameObject newFish = Instantiate(fishPrefab, randomSpawnPoint.position, randomSpawnPoint.rotation);

        Fish fishComponent = newFish.GetComponent<Fish>();
        if (fishComponent != null)
        {
            fishComponent.spawnPosition = randomSpawnPoint.position;
        }

        spawnedFish.Add(newFish);
        spawnPointCooldowns[randomSpawnPoint] = spawnPointCooldown;
    }

    void CleanUpFishList()
    {
        spawnedFish.RemoveAll(fish => fish == null);
    }

    void EndNight()
    {
        nightEnded = true;
        CancelInvoke(nameof(SpawnFish));

        Time.timeScale = 0f;

        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}