using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndSceneManager : MonoBehaviour
{
    public PlayerData playerData;

    public TMP_Text titleText;
    public TMP_Text playerUIName;
    public TMP_Text classUIName;
    public TMP_Text levelUI;

    public Image winImage;

    void Start()
    {
        if(playerData.curHealth <= 0)
        {
            titleText.text = "YOU DIED";
            winImage.enabled = false;
        }
        else
        {
            titleText.text = "YOU SURVIVED";
            winImage.enabled = true;
        }

        playerUIName.text = playerData.playerName;
        classUIName.text = playerData.className;
        levelUI.text = playerData.currentLevel.ToString();
    }

    public void OnMenuButtonPressed()
    {
        gameObject.GetComponent<SaveSystem>().AddToGloryTable(new GameResult(playerData.playerName, 
            playerData.className, playerData.currentLevel.ToString()));
        gameObject.GetComponent<SaveSystem>().SaveGloryTable();
        SceneManager.LoadScene("MainMenu");
    }
}
