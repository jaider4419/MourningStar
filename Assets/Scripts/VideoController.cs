using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string nextSceneName;

    void Start()
    {
        videoPlayer.loopPointReached += EndReached;
        videoPlayer.Play();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene(nextSceneName);  
        }
    }

    void EndReached(VideoPlayer vp)
    {
        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}