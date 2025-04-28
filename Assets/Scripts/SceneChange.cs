using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string SceneName;




    public void loadNextScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneName);
    }



}
