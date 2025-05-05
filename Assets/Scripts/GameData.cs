using UnityEngine;

[System.Serializable]
public class GameData
{
    public string lastSavedTime;
    public int currentSceneIndex;
    public Vector3 playerPosition;

    public GameData()
    {
        lastSavedTime = System.DateTime.Now.ToString();
        currentSceneIndex = 1;
        playerPosition = Vector3.zero;
    }
}