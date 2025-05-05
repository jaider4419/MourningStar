using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string SAVE_PATH = Application.persistentDataPath + "/save.json";

    public static void SaveGame(GameData data)
    {
        try
        {
            string jsonData = JsonUtility.ToJson(data, true);
            File.WriteAllText(SAVE_PATH, jsonData);
            Debug.Log("Game saved to: " + SAVE_PATH);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Save failed: " + e.Message);
        }
    }

    public static GameData LoadGame()
    {
        if (File.Exists(SAVE_PATH))
        {
            try
            {
                string jsonData = File.ReadAllText(SAVE_PATH);
                return JsonUtility.FromJson<GameData>(jsonData);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Load failed: " + e.Message);
                return null;
            }
        }
        return null;
    }

    public static void DeleteSave()
    {
        if (File.Exists(SAVE_PATH))
        {
            File.Delete(SAVE_PATH);
            Debug.Log("Save file deleted");
        }
    }

    public static bool SaveExists()
    {
        return File.Exists(SAVE_PATH);
    }
}