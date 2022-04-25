using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathManager : MonoBehaviour
{
    public void StartBattleScene()
    {
        SceneManager.LoadScene("BattleScene");
    }
}
