using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Debug.Log("ExitPressed");
        Application.Quit();
    }
}
