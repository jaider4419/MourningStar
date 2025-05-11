using UnityEngine;
using UnityEngine.SceneManagement;

public class SmartBGMManager : MonoBehaviour
{
    public static SmartBGMManager Instance { get; private set; }

    [Header("BGM Settings")]
    public AudioClip bgmClip;
    [Tooltip("Scenes where BGM should be playing")]
    public string[] activeScenes = new string[] { "Lhfloor", "Lhfloor2", "Morning" };
    [Tooltip("Scenes where BGM should pause")]
    public string[] pauseScenes = new string[] { "Night", "Cutscene" };

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = bgmClip;
            audioSource.loop = true;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (System.Array.Exists(activeScenes, s => s == scene.name))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            audioSource.UnPause();
        }

        else if (System.Array.Exists(pauseScenes, s => s == scene.name))
        {
            audioSource.Pause();
        }

        else
        {
            audioSource.Stop();
        }
    }
}