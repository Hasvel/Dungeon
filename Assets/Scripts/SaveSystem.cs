using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem: MonoBehaviour
{
    public PlayerData playerData;
    [SerializeField] private List<GameResult> gameResults;
    string savePath;
    string saveGlory;

    [Serializable]
    private class SaveWrapper
    {
        public List<GameResult> gameResultsWrapped;
    }

    void Awake()
    {
        // Update the path once the persistent path exists.
        savePath = Application.persistentDataPath + "/saveData.json";
        saveGlory = Application.persistentDataPath + "/saveGlory.json";

        gameResults = new List<GameResult>();
    }

    public void SavePlayerData()
    {
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
            UnitStats savedPlayerData = JsonUtility.FromJson<UnitStats>(fileContents);

            playerData.playerName = savedPlayerData.playerName;
            playerData.currentLevel = savedPlayerData.currentLevel;
            playerData.difficulty = savedPlayerData.difficulty;
            playerData.gold = savedPlayerData.gold;

            playerData.className = savedPlayerData.className;
            playerData.characterModel = savedPlayerData.characterModel;
            playerData.characterLocation = savedPlayerData.characterLocation;

            playerData.chanceHead = savedPlayerData.chanceHead;
            playerData.chanceArms = savedPlayerData.chanceArms;
            playerData.chanceBody = savedPlayerData.chanceBody;
            playerData.chanceLegs = savedPlayerData.chanceLegs;

            playerData.maxHealth = savedPlayerData.maxHealth;
            playerData.curHealth = savedPlayerData.curHealth;
            playerData.attackDamage = savedPlayerData.attackDamage;
            playerData.addAttackChance = savedPlayerData.addAttackChance;
            playerData.pierceAttackDamage = savedPlayerData.pierceAttackDamage;
            playerData.defence = savedPlayerData.defence;

            return true;
        }
        else return false;
    }   
    
    public void AddToGloryTable(GameResult gameResult)
    {
        ReadGloryTabe();
        gameResults.Add(gameResult);
        SaveGloryTable();
    }
    public void SaveGloryTable()
    {
        SaveWrapper saveWrapper = new SaveWrapper();
        saveWrapper.gameResultsWrapped = gameResults;
        string saveFile = JsonUtility.ToJson(saveWrapper);

        File.WriteAllText(saveGlory, saveFile);
    }

    private bool ReadGloryTabe()
    {
        if (File.Exists(saveGlory))
        {
            string fileContents = File.ReadAllText(saveGlory);
            SaveWrapper saveWrapper = JsonUtility.FromJson<SaveWrapper>(fileContents);
            gameResults = saveWrapper.gameResultsWrapped;
            return true;
        }
        else
            return false;
    }

    public List<GameResult> GetGloryTable()
    {
        if (ReadGloryTabe())
        {
            return new List<GameResult>(gameResults);
        }
        else
            return null;
    }
}
