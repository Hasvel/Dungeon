using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class BattleSystemManager : MonoBehaviour
{
    public enum BattleState 
    { 
        START, 
        PLAYERTURN, 
        ENEMYTURN, 
        WIN, 
        LOST 
    }

    public enum BodyRegion
    {
        HEAD = 0,
        ARMS = 1,
        BODY = 2,
        LEGS = 3
    }

    public PlayerData player;
    private GameObject playerObj;
    private EnemyData enemy;
    private GameObject enemyObj;
    public EnemySpawner enemySpawner;

    private int enemyHealth;

    private BattleState battleState;
    private BodyRegion curBodyRegion;
    private bool hasClicked = true;

    // UI
    public Text announcementText;
    public Text playerNameText;
    public Text classNameText;
    public Text levelText;

    public Slider playerHealthBar;
    public Slider enemyHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        // randomize enemy
        enemy = enemySpawner.spawnEnemy();

        battleState = BattleState.START;
        curBodyRegion = BodyRegion.HEAD;

        enemyHealth = enemy.curHealth;

        // UI
        announcementText.enabled = false;
        playerNameText.text = player.playerName;
        classNameText.text = player.className;
        levelText.text = player.currentLevel.ToString();

        playerHealthBar.maxValue = player.maxHealth;
        enemyHealthBar.maxValue = enemy.maxHealth;
        playerHealthBar.value = player.curHealth;
        enemyHealthBar.value = enemyHealth;

        StartCoroutine(BeginBattle());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator BeginBattle()
    {
        // spawn characters on the platforms
        playerObj = Instantiate(player.characterModel.gameObject, player.characterLocation, player.characterModel.transform.rotation);
        enemyObj = Instantiate(enemy.enemyModel.gameObject, enemy.enemyLocation, enemy.enemyModel.transform.rotation);

        // set the characters stats in HUD displays
        //playerStatusHUD.SetStatusHUD(playerStatus);
        //enemyStatusHUD.SetStatusHUD(enemyStatus);

        yield return new WaitForSeconds(2);

        // player turn!
        battleState = BattleState.PLAYERTURN;

        // let player select his action now!    
        yield return StartCoroutine(PlayerTurn());
    }

    public void OnAttackButtonPress()
    {
        // don't allow player to click on 'attack'
        // button if it's not his turn!
        if (battleState != BattleState.PLAYERTURN)
            return;

        // allow only a single action per turn
        if (!hasClicked)
        {
            StartCoroutine(PlayerAttack());

            // block user from repeatedly 
            // pressing attack button  
            hasClicked = true;
        }
    }

    public void OnRunBattonPress()
    {
        if (battleState != BattleState.PLAYERTURN)
            return;

        if (!hasClicked)
        {
            float runRolledChance = Random.Range(0.0f, 1.0f);
            if(runRolledChance <= 0.5)
            {
                SceneManager.LoadScene("Doors");
            }
            else
            {
                StartCoroutine(EnemyTurn());
            }

            hasClicked = true;
        }
    }

    public void OnMoveNextButtonPress()
    {
        if(battleState == BattleState.WIN)
        {
            // saving
            player.currentLevel++;
            gameObject.GetComponent<SaveSystem>().SavePlayerData();

            SceneManager.LoadScene("Doors");
        }
        else if (battleState == BattleState.LOST)
        {
            // glory table
            SceneManager.LoadScene("MainMenu");
        }
    }

    IEnumerator PlayerTurn()
    {
        StartCoroutine(ShowMessage("Player Turn", 3));
        yield return new WaitForSeconds(3);
        hasClicked = false;
    }

    IEnumerator EnemyTurn()
    {
        StartCoroutine(ShowMessage("Enemy Turn", 3));
        yield return new WaitForSeconds(3);

        // Enemy rolls dice
        float attackRolledChance = Random.Range(0.0f, 1.0f) - player.difficulty/200;

        // Determing body part to attack
        float neededChance = 0.2f;
        int bodyIndex = Random.Range(0, 4);
        switch (bodyIndex)
        {
            case 0:
                neededChance = player.chanceHead;
                break;
            case 1:
                neededChance = player.chanceArms;
                break;
            case 2:
                neededChance = player.chanceBody;
                break;
            case 3:
                neededChance = player.chanceLegs;
                break;
        }

        // Determing is attack succesful
        if (attackRolledChance <= neededChance)
        {
            int defenceDamage = enemy.attackDamage - player.defence;
            if (defenceDamage < 0)
                defenceDamage = 0;

            int resultDamage = enemy.pierceAttackDamage + defenceDamage;

            player.curHealth -= resultDamage;
            playerHealthBar.value = player.curHealth;
            StartCoroutine(ShowMessage("Enemy Hits!", 3));
        }
        else
        {
            StartCoroutine(ShowMessage("Enemy Misses", 3));
        }
        Debug.Log($"Enemy Turn! Rolled - {attackRolledChance}, Needed - {neededChance}");
        Debug.Log($"Player Health: {player.curHealth}");
        yield return new WaitForSeconds(3);

        // End of fight condition
        if (player.curHealth <= 0)
        {
            // if the player health drops to 0 
            // we have lost the battle...
            battleState = BattleState.LOST;
            yield return StartCoroutine(EndBattle());
        }
        else
        {
            // if the player health is still
            // above 0 when the turn finishes
            // it's our turn again!
            battleState = BattleState.PLAYERTURN;
            yield return StartCoroutine(PlayerTurn());
        }
    }

    IEnumerator PlayerAttack()
    {
        // Player rolls dice
        float attackRolledChance = Random.Range(0.0f, 1.0f) - player.addAttackChance;

        // Determing current chance of current body part
        float neededChance = 0.2f;
        float damageMultiplier = 1.0f;
        switch(curBodyRegion)
        {
            case BodyRegion.HEAD:
                neededChance = enemy.chanceHead;
                damageMultiplier = enemy.multHead;
                break;
            case BodyRegion.ARMS:
                neededChance = enemy.chanceArms;
                damageMultiplier = enemy.multArms;
                break;
            case BodyRegion.BODY:
                neededChance = enemy.chanceBody;
                damageMultiplier = enemy.multBody;
                break;
            case BodyRegion.LEGS:
                neededChance = enemy.chanceLegs;
                damageMultiplier = enemy.multLegs;
                break;
        }

        // Determing if is attack succesful
        if (attackRolledChance <= neededChance)
        {
            int defenceDamage = player.attackDamage - enemy.defence;
            if(defenceDamage < 0)
                defenceDamage = 0;

            float resultDamage = (player.pierceAttackDamage + defenceDamage) * damageMultiplier;
            enemyHealth -= (int)Math.Round(resultDamage);
            enemyHealthBar.value = enemyHealth;
            StartCoroutine(ShowMessage("Hit!", 3));
        }
        else
        {
            StartCoroutine(ShowMessage("Miss", 3));
        }
        Debug.Log($"Player Turn! Rolled - {attackRolledChance}, Needed - {neededChance}");
        Debug.Log($"Enemy Health: {enemyHealth}");
        yield return new WaitForSeconds(3);

        // End of fight condition
        if (enemyHealth <= 0)
        {
            // if the enemy health drops to 0 
            // we won!
            battleState = BattleState.WIN;
            yield return StartCoroutine(EndBattle());
        }
        else
        {
            // if the enemy health is still
            // above 0 when the turn finishes
            // it's enemy's turn!
            battleState = BattleState.ENEMYTURN;
            yield return StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator EndBattle()
    {
        if (battleState == BattleState.WIN)
        {
            Destroy(enemyObj);
            StartCoroutine(ShowMessage("VICTORY", 10));
            yield return new WaitForSeconds(1);
            Debug.Log("WINNER");
        }
        else if (battleState == BattleState.LOST)
        {
            Destroy(playerObj);
            StartCoroutine(ShowMessage("YOU'RE DEAD", 10));
            yield return new WaitForSeconds(1);
            Debug.Log("LOST");
        }
    }

    public void SetCurBodyRegion(int BRNum)
    {
        curBodyRegion = (BodyRegion)BRNum;
    }

    IEnumerator ShowMessage(string message, float delay)
    {
        announcementText.text = message;
        announcementText.enabled = true;
        yield return new WaitForSeconds(delay);
        announcementText.enabled = false;
    }
}