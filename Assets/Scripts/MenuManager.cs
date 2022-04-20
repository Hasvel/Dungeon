using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public enum Characters
    {
        ARCHER = 0,
        KNIGHT = 1,
        KING = 2
    }

    public Image classBackground;

    public void StartNewGame()
    {
        SceneManager.LoadScene(2);
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
                classBackground.color = 
                break;
            case Characters.KNIGHT:
                break;
            case Characters.KING:
                break;
        }
    }
}
