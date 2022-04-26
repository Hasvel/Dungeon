using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem: MonoBehaviour
{
    public PlayerData playerData;
    string saveFile;

    void Awake()
    {
        // Update the path once the persistent path exists.
        saveFile = Application.persistentDataPath + "/saveData.json";
    }

    public void SavePlayerData()
    {
        string saveFile = JsonUtility.ToJson(playerData);
        File.WriteAllText(saveFile, saveFile);
    }

    public bool ReadPlayerData()
    {
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);

            // Deserialize the JSON data 
            //  into a pattern matching the GameData class.
            playerData = JsonUtility.FromJson<PlayerData>(fileContents);

            return true;
        }
        else return false;
    }    
}
