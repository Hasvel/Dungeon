using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public enum Characters
    {
        ARCHER = 0,
        KNIGHT = 1,
        KING = 2
    }

    public Image classBackground;
    public Text className;
    public Slider difficultySlider;
    public TMP_InputField playerNameField;

    private int difficulty = 0;
    private string playerName = "";
    private Characters currentClass = Characters.ARCHER;

    public PlayerData playerData;
    public CharacterData[] characters;

    public void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        difficultySlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        playerNameField.onValueChanged.AddListener(delegate { NameChangeCheck(); });

        DontDestroyOnLoad(playerData);
    }

    public void StartNewGame()
    {
        if(playerName != "")
        {
            playerData.playerName = playerName;
            playerData.currentLevel = 1;
            playerData.difficulty = difficulty;
            playerData.gold = 0;

            foreach(CharacterData character in characters)
            {
                if(character.className.ToLower() == currentClass.ToString().ToLower())
                {
                    playerData.className = character.className;
                    playerData.characterModel = character.characterModel;
                    playerData.characterLocation = character.characterLocation;

                    playerData.chanceHead = character.chanceHead;
                    playerData.chanceArms = character.chanceArms;
                    playerData.chanceBody = character.chanceBody;
                    playerData.chanceLegs = character.chanceLegs;

                    playerData.maxHealth = character.maxHealth;
                    playerData.curHealth = character.maxHealth;
                    playerData.attackDamage = character.attackDamage;
                    playerData.addAttackChance = character.addAttackChance;
                    playerData.pierceAttackDamage = character.pierceAttackDamage;
                    playerData.defence = character.defence;

                    Debug.Log($"Menu End - Player Name: {playerData.playerName} {playerName}");
                    SceneManager.LoadScene("Doors");
                }
            }
        }
    }

    public void ContinueGame()
    {
        if(gameObject.GetComponent<SaveSystem>().ReadPlayerData())
            SceneManager.LoadScene("Doors");
    }

    public void ExitGame()
    {
        Debug.Log("ExitPressed");
        Application.Quit();
    }

    public void SelectClass(int classIndex)
    {
        Characters character = (Characters)classIndex;
        switch (character)
        {
            case Characters.ARCHER:
                classBackground.color = new Color32(71, 190, 177, 255);
                className.text = "Archer";
                currentClass = Characters.ARCHER;
                break;
            case Characters.KNIGHT:
                classBackground.color = new Color32(190, 71, 79, 255);
                className.text = "Knight";
                currentClass = Characters.KNIGHT;
                break;
            case Characters.KING:
                classBackground.color = new Color32(71, 136, 190, 255);
                className.text = "King";
                currentClass = Characters.KING;
                break;
        }
    }

    public void ValueChangeCheck()
    {
        difficulty = (int)difficultySlider.value;
        Debug.Log(difficulty);
    }

    public void NameChangeCheck()
    {
        playerName = playerNameField.text;
        Debug.Log(playerName);
    }
}
