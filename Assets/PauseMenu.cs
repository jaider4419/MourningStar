using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the pause menu UI panel
    public string mainMenuSceneName = "MainMenu"; // Name of the main menu scene
    public AudioSource bgm;
    private bool isPaused = false;
    private string nextSceneName = "FirstScene";



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked; 
        bgm.Play();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; 
        bgm.Pause();
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }


    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();

    }

    public void Restart() // Not in use currently.
    {
        Debug.Log("scene reloads");
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(nextSceneName);
    }
}