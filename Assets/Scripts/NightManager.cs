using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NightManager : MonoBehaviour
{
    public static NightManager Instance;

    [Header("UI Elements")]
    public Text energyText;
    public Text timerText;
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Game Settings")]
    public float maxEnergy = 100f;
    public float energyDrainRate = 2f; // per second when light is on
    public float gameDuration = 120f; // 2 minutes

    private float currentEnergy;
    private float gameTimer;
    private bool gameOver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentEnergy = maxEnergy;
        gameTimer = gameDuration;
        UpdateUI();
    }

    private void Update()
    {
        if (gameOver) return;

        gameTimer -= Time.deltaTime;
        if (gameTimer <= 0)
        {
            WinGame();
            return;
        }

        UpdateUI();
    }

    public void DrainEnergy()
    {
        if (gameOver) return;

        currentEnergy -= energyDrainRate * Time.deltaTime;
        if (currentEnergy <= 0)
        {
            currentEnergy = 0;
            LoseGame();
        }
    }

    private void UpdateUI()
    {
        energyText.text = $"Energy: {Mathf.CeilToInt(currentEnergy)}%";

        int minutes = Mathf.FloorToInt(gameTimer / 60);
        int seconds = Mathf.FloorToInt(gameTimer % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void WinGame()
    {
        gameOver = true;
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void LoseGame()
    {
        gameOver = true;
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}