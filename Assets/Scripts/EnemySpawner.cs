using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public EnemyData[] enemies;

    public EnemyData spawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemies.Length);
        return enemies[enemyIndex];
    }
}
