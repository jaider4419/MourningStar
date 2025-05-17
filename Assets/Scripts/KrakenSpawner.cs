using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class KrakenSpawner : MonoBehaviour
{
    public GameObject krakenPrefab;
    public float spawnRate = 10f;
    public int maxKrakens = 1;
    public float spawnPointCooldown = 20f;
    [SerializeField] private float nightDuration = 20f;
    public string nextSceneName = "RollingCredits";

    [System.Serializable]
    public class SpawnPoint
    {
        public Transform transform;
        public Vector3 lookDirection;
    }

    public SpawnPoint[] spawnPoints;

    private List<GameObject> spawnedKrakens = new List<GameObject>();
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
        InvokeRepeating(nameof(SpawnKraken), 0f, spawnRate);
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

    void SpawnKraken()
    {
        if (krakenPrefab == null || spawnedKrakens.Count >= maxKrakens) return;
        CleanUpKrakenList();
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
        GameObject newKraken = Instantiate(krakenPrefab, chosenPoint.transform.position, Quaternion.Euler(chosenPoint.lookDirection));
        spawnedKrakens.Add(newKraken);
        spawnPointCooldowns[chosenPoint.transform] = spawnPointCooldown;
    }

    public void RemoveKraken(GameObject kraken)
    {
        if (spawnedKrakens.Contains(kraken))
        {
            spawnedKrakens.Remove(kraken);
        }
    }

    void CleanUpKrakenList()
    {
        spawnedKrakens.RemoveAll(kraken => kraken == null);
    }

    public void TriggerLoseCondition()
    {
        if (nightEnded) return;
        nightEnded = true;
        CancelInvoke(nameof(SpawnKraken));
        foreach (var kraken in spawnedKrakens.ToArray())
        {
            if (kraken != null) Destroy(kraken);
        }
        spawnedKrakens.Clear();
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextSceneName);
    }

    void EndNight()
    {
        nightEnded = true;
        CancelInvoke(nameof(SpawnKraken));
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextSceneName);
    }
}