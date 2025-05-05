using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string newGameScene = "Level1";
    [SerializeField] private Button loadGameButton;

    void Start() => loadGameButton.interactable = SaveSystem.SaveExists();

    public void NewGame()
    {
        SaveLoadManager.Instance.NewGame();
        SceneManager.LoadScene(newGameScene);
    }

    public void LoadGame() => SaveLoadManager.Instance.LoadGame();

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}