using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GloryTableManager : MonoBehaviour
{
    private List<GameResult> gameResults;

    // Start is called before the first frame update
    void Start()
    {
        GameObject template = transform.GetChild(0).gameObject;
        gameResults = GetComponent<SaveSystem>().GetGloryTable();
        if (gameResults == null)
        {
            Destroy(template);
            return;
        }

        GameObject newField;

        int N = gameResults.Count;

        for(int i = 0; i < N; i++)
        {
            newField = Instantiate(template, transform);
            newField.transform.GetChild(0).GetComponent<TMP_Text>().text = gameResults[i].playerName;
            newField.transform.GetChild(1).GetComponent<TMP_Text>().text = gameResults[i].className;
            newField.transform.GetChild(2).GetComponent<TMP_Text>().text = gameResults[i].level.ToString();
        }

        Destroy(template);
    }
}
