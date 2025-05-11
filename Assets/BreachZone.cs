using UnityEngine;

public class BreachZone : MonoBehaviour
{
    // REQUIRED INSPECTOR REFERENCES
    public FishSpawner spawner;  // Drag your FishSpawner object here
    public AudioClip breachSound; // Drag your sound file here

    // SETTINGS
    public int maxBreaches = 5;

    // PRIVATE
    private AudioSource audioSource;
    private int currentBreaches = 0;

    void Start()
    {
        // Auto-create AudioSource if missing
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Exit if not a fish
        if (!other.CompareTag("Fish")) return;

        // Increment counter
        currentBreaches++;
        Debug.Log($"Breach #{currentBreaches} detected!");

        // Play sound (forced)
        if (breachSound != null)
        {
            AudioSource.PlayClipAtPoint(breachSound, transform.position);
        }
        else
        {
            Debug.LogError("Missing breach sound clip!");
        }

        // Destroy the fish
        Destroy(other.gameObject);

        // Check lose condition
        if (currentBreaches >= maxBreaches && spawner != null)
        {
            Debug.Log("MAX BREACHES! Triggering lose...");
            spawner.TriggerLoseCondition();
        }
        else if (spawner == null)
        {
            Debug.LogError("FishSpawner reference missing!");
        }
    }
}