using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem: MonoBehaviour
{
    public PlayerData playerData;
    string savePath;

    void Awake()
    {
        // Update the path once the persistent path exists.
        savePath = Application.persistentDataPath + "/saveData.json";
    }

    public void SavePlayerData()
    {
        Debug.Log($"{savePath}");
        string saveFile = JsonUtility.ToJson(playerData);
        File.WriteAllText(savePath, saveFile);
    }

    public bool ReadPlayerData()
    {
        if (File.Exists(savePath))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(savePath);

            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.
            playerData = JsonUtility.FromJson<PlayerData>(fileContents);

            return true;
        }
        else return false;
    }    
}
