using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Dungeon/Character Data")]
public class CharacterData : ScriptableObject
{
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
