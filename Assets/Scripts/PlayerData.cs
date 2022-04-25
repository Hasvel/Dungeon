using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Dungeon/Player Data")]
public class PlayerData : ScriptableObject
{
    public string playerName;
    public int currentLevel;
    public int difficulty;
    public int gold;

    // Start class info
    public string className;
    public GameObject characterModel;
    public Vector3 characterLocation;

    // Combat info
    public float chanceHead;
    public float chanceArms;
    public float chanceBody;
    public float chanceLegs;

    public int maxHealth;
    public int curHealth;
    public int attackDamage;
    public float addAttackChance;
    public int pierceAttackDamage;
    public int defence;
}
