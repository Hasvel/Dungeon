using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathManager : MonoBehaviour
{
    public PlayerData playerData;

    public void Start()
    {
        Debug.Log($"Doors PN - {playerData.playerName}");
        DontDestroyOnLoad(playerData);
    }
    public void StartBattleScene()
    {
        if(playerData.currentLevel % 3 == 0)
        {
            SceneManager.LoadScene("TraderScene");
        }
        else
        {
            SceneManager.LoadScene("BattleScene");
        }
    }
}
