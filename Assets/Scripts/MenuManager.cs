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
    public Text className;
    public Slider difficultySlider;

    private int difficulty = 0;

    public void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        difficultySlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

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
                classBackground.color = new Color32(71, 190, 177, 255);
                className.text = "Archer";
                break;
            case Characters.KNIGHT:
                classBackground.color = new Color32(190, 71, 79, 255);
                className.text = "Knight";
                break;
            case Characters.KING:
                classBackground.color = new Color32(71, 136, 190, 255);
                className.text = "King";
                break;
        }
    }

    public void ValueChangeCheck()
    {
        difficulty = (int)difficultySlider.value;
        Debug.Log(difficulty);
    }
}
