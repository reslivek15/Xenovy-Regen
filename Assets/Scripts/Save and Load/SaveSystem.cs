using System.IO;
using UnityEngine;
using UnityEngine.Splines.ExtrusionShapes;
public static class SaveSystem
{
    private static string saveFilePath = Application.persistentDataPath + "/playerSave.json";

    // Save the data to a JSON file
    public static void SavePlayer(PlayerSaveData data)
    {
        string json = JsonUtility.ToJson(data, true); // Convert to JSON string
        File.WriteAllText(saveFilePath, json);        // Write to file
        Debug.Log("Player saved to: " + saveFilePath);
    }

    //Load the data from the JSON file
    public static PlayerSaveData LoadPlayer()
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.LogWarning("No save file found at: " + saveFilePath);
            return null;
        }

        string json = File.ReadAllText(saveFilePath);     // Read the file
        PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(json); // Convert JSON to object
        return data;
    }
}
