using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Dungeon/Player Data")]
public class PlayerData : ScriptableObject
{
    public string playerName;
    public int currentLevel;
    public int gold;

    public CharacterData character;

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

    // testing
}
