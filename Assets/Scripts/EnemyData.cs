using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "Dungeon/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public GameObject enemyModel; 
    public Vector3 enemyLocation;

    public int gold;

    // Combat info
    public float chanceHead;
    public float chanceArms;
    public float chanceBody;
    public float chanceLegs;

    public float multHead;
    public float multArms;
    public float multBody;
    public float multLegs;

    public int maxHealth;
    public int curHealth;
    public int attackDamage;
    public int pierceAttackDamage;
    public int defence;
}