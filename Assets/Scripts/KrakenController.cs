using UnityEngine;
using System.Collections;

public class KrakenController : MonoBehaviour
{
    [Header("Tentacle Settings")]
    [SerializeField] private Transform[] tentacleBones; 
    [SerializeField] private float riseHeight = 3f; 
    [SerializeField] private float minRiseTime = 2f;
    [SerializeField] private float maxRiseTime = 5f;
    [SerializeField] private float riseSpeed = 0.5f;
    [SerializeField] private float retreatDelay = 0.3f; 

    [Header("Health")]
    [SerializeField] private int hitsPerTentacleToKill = 15;
    [SerializeField] private GameObject winPanel;

    private Vector3[] originalPositions;
    private bool[] isTentacleRaised;
    private int[] tentacleHitCounts;
    private bool isRetreating = false;

    void Awake()
    {
        originalPositions = new Vector3[tentacleBones.Length];
        isTentacleRaised = new bool[tentacleBones.Length];
        tentacleHitCounts = new int[tentacleBones.Length];

        for (int i = 0; i < tentacleBones.Length; i++)
        {
            originalPositions[i] = tentacleBones[i].localPosition;
        }
    }

    void Start()
    {
        for (int i = 0; i < tentacleBones.Length; i++)
        {
            StartCoroutine(RandomTentacleMovement(i));
        }
    }

    IEnumerator RandomTentacleMovement(int tentacleIndex)
    {
        while (true)
        {
            if (!isRetreating)
            {
                float waitTime = Random.Range(minRiseTime, maxRiseTime);
                yield return new WaitForSeconds(waitTime);

                yield return StartCoroutine(MoveTentacle(tentacleIndex, riseHeight, riseSpeed));
                isTentacleRaised[tentacleIndex] = true;

                yield return new WaitForSeconds(Random.Range(1f, 3f));

                if (isTentacleRaised[tentacleIndex])
                {
                    yield return StartCoroutine(MoveTentacle(tentacleIndex, 0f, riseSpeed));
                    isTentacleRaised[tentacleIndex] = false;
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator MoveTentacle(int index, float targetHeight, float speed)
    {
        Vector3 startPos = tentacleBones[index].localPosition;
        Vector3 endPos = originalPositions[index] + Vector3.up * targetHeight;
        float progress = 0f;

        while (progress < 1f)
        {
            progress += Time.deltaTime * speed;
            tentacleBones[index].localPosition = Vector3.Lerp(startPos, endPos, progress);
            yield return null;
        }
    }

    public void OnTentacleHitByLight(int tentacleIndex)
    {
        if (!isTentacleRaised[tentacleIndex]) return;

        StartCoroutine(RetreatTentacle(tentacleIndex));
        RegisterHit(tentacleIndex);
    }

    IEnumerator RetreatTentacle(int tentacleIndex)
    {
        isRetreating = true;
        isTentacleRaised[tentacleIndex] = false;

        yield return new WaitForSeconds(retreatDelay);

        yield return StartCoroutine(MoveTentacle(tentacleIndex, 0f, riseSpeed * 2f));
        isRetreating = false;
    }

    void RegisterHit(int tentacleIndex)
    {
        tentacleHitCounts[tentacleIndex]++;
        CheckForDeath();
    }

    void CheckForDeath()
    {
        foreach (int count in tentacleHitCounts)
        {
            if (count < hitsPerTentacleToKill) return;
        }

        if (winPanel != null) winPanel.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void LightHitTentacle(Transform hitTentacle)
    {
        for (int i = 0; i < tentacleBones.Length; i++)
        {
            if (tentacleBones[i] == hitTentacle)
            {
                OnTentacleHitByLight(i);
                break;
            }
        }
    }
}