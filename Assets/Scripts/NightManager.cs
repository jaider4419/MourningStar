using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NightManager : MonoBehaviour
{
    public static NightManager Instance;

    public TMP_Text energyText;
    private Text timerText;
    public GameObject winPanel;
    public GameObject losePanel;

    public float maxEnergy = 100f;
    public float chunkSize = 20f; 
    public float chunkTime = 10f;
    public float gameDuration = 120f; 

    private float currentEnergy;
    private float gameTimer;
    private float drainTimer;
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
        UpdateEnergyUI(); 
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

        int minutes = Mathf.FloorToInt(gameTimer / 60);
        int seconds = Mathf.FloorToInt(gameTimer % 60);
    }

    public void DrainEnergyOverTime()
    {
        drainTimer += Time.deltaTime;

        if (drainTimer >= chunkTime)
        {
            currentEnergy -= chunkSize;
            drainTimer = 0f; 

            UpdateEnergyUI();

            if (currentEnergy <= 0)
            {
                currentEnergy = 0;
                LoseGame();
            }
        }
    }

    private void UpdateEnergyUI()
    {
        int displayEnergy = Mathf.CeilToInt(currentEnergy / chunkSize) * (int)chunkSize;
        energyText.text = $"Power: {displayEnergy}%";
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