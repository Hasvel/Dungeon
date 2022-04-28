using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MarketManager : MonoBehaviour
{
    public PlayerData player;

    public int shieldCost = 7;
    public int kniveCost = 7;
    public int scrollCost = 10;
    public int potionCost = 4;
    public int gloveCost = 10;

    public TMP_Text goldText;

    public void Start()
    {
        goldText.text = player.gold.ToString();
        DontDestroyOnLoad(player);
    }

    public void OnShieldBuy()
    {
        if(player.gold >= shieldCost)
        {
            player.gold -= shieldCost;
            player.defence += 2;

            goldText.text = player.gold.ToString();
        }
    }

    public void OnKniveBuy()
    {
        if (player.gold >= kniveCost)
        {
            player.gold -= kniveCost;
            player.attackDamage += 2;

            goldText.text = player.gold.ToString();
        }
    }

    public void OnScrollBuy()
    {
        if (player.gold >= scrollCost)
        {
            player.gold -= scrollCost;
            player.maxHealth += 10;

            goldText.text = player.gold.ToString();
        }
    }

    public void OnPotionBuy()
    {
        if (player.gold >= potionCost)
        {
            player.gold -= potionCost;
            player.curHealth = player.maxHealth;

            goldText.text = player.gold.ToString();
        }
    }

    public void OnGloveBuy()
    {
        if (player.gold >= gloveCost)
        {
            player.gold -= gloveCost;
            player.addAttackChance += 0.1f;

            goldText.text = player.gold.ToString();
        }
    }

    public void OnMoveNextButtonPress()
    {
        player.currentLevel++;
        gameObject.GetComponent<SaveSystem>().SavePlayerData();

        SceneManager.LoadScene("Doors");
    }

    public void OnExitButtonPress()
    {
        SceneManager.LoadScene("MainMenu");
    }
}