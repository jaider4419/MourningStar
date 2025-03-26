using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorSceneChange : MonoBehaviour
{
    public string SceneName;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            SceneManager.LoadScene(SceneName);
            Debug.Log("Loading New Scene");
        }
    }
}
