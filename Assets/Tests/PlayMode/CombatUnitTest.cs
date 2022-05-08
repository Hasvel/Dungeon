using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CombatUnitTest
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator KnightCombatUnitTestOne()
    {
        PlayerData playerData = ScriptableObject.CreateInstance<PlayerData>();

        // examples of possible stats
        playerData.defence = 5;
        playerData.maxHealth = 30;
        playerData.curHealth = 30;

        // examples of possible enemy stats
        EnemyData enemyDataOne = ScriptableObject.CreateInstance<EnemyData>();
        enemyDataOne.attackDamage = 10;
        enemyDataOne.pierceAttackDamage = 5;

        int damage = enemyDataOne.attackDamage - playerData.defence;
        if (damage <= 0)
            damage = 0;
        damage += enemyDataOne.pierceAttackDamage;
        Debug.Log($"pDamage: {enemyDataOne.pierceAttackDamage}");
        playerData.curHealth -= damage;

        Debug.Log($"damage: {damage}, curHealth: {playerData.curHealth}");

        yield return new WaitForSeconds(5);
        Assert.AreEqual(playerData.curHealth, 20);
    }

    [UnityTest]
    public IEnumerator KnightCombatUnitTestTwo()
    {
        PlayerData playerData = ScriptableObject.CreateInstance<PlayerData>();

        // examples of possible stats
        playerData.defence = 5;
        playerData.maxHealth = 30;
        playerData.curHealth = 30;

        // examples of possible enemy stats
        EnemyData enemyDataTwo = ScriptableObject.CreateInstance<EnemyData>();
        enemyDataTwo.attackDamage = 5;
        enemyDataTwo.pierceAttackDamage = 0;

        int damage = enemyDataTwo.attackDamage - playerData.defence;
        if (damage <= 0)
            damage = 0;
        damage += enemyDataTwo.pierceAttackDamage;
        playerData.curHealth -= damage;

        yield return new WaitForSeconds(5);
        Assert.AreEqual(playerData.curHealth, 30);
    }

    [UnityTest]
    public IEnumerator KnightCombatUnitTestThree()
    {
        PlayerData playerData = ScriptableObject.CreateInstance<PlayerData>();

        // examples of possible stats
        playerData.defence = 5;
        playerData.maxHealth = 30;
        playerData.curHealth = 30;

        EnemyData enemyDataThree = ScriptableObject.CreateInstance<EnemyData>();
        enemyDataThree.attackDamage = 0;
        enemyDataThree.pierceAttackDamage = 10;

        int damage = enemyDataThree.attackDamage - playerData.defence;
        if (damage <= 0)
            damage = 0;
        damage += enemyDataThree.pierceAttackDamage;
        playerData.curHealth -= damage;

        yield return new WaitForSeconds(5);
        Assert.AreEqual(playerData.curHealth, 20);
    }

    [UnityTest]
    public IEnumerator ArcherCombatUnitTestThree()
    {
        PlayerData playerData = ScriptableObject.CreateInstance<PlayerData>();

        // examples of possible stats
        playerData.defence = 3;
        playerData.maxHealth = 25;
        playerData.curHealth = 25;

        EnemyData enemyDataThree = ScriptableObject.CreateInstance<EnemyData>();
        enemyDataThree.attackDamage = 4;
        enemyDataThree.pierceAttackDamage = 5;

        int damage = enemyDataThree.attackDamage - playerData.defence;
        if (damage <= 0)
            damage = 0;
        damage += enemyDataThree.pierceAttackDamage;
        playerData.curHealth -= damage;

        yield return new WaitForSeconds(5);
        Assert.AreEqual(playerData.curHealth, 19);
    }

    [UnityTest]
    public IEnumerator KingCombatUnitTestThree()
    {
        PlayerData playerData = ScriptableObject.CreateInstance<PlayerData>();

        // examples of possible stats
        playerData.defence = 2;
        playerData.maxHealth = 20;
        playerData.curHealth = 20;

        EnemyData enemyDataThree = ScriptableObject.CreateInstance<EnemyData>();
        enemyDataThree.attackDamage = 0;
        enemyDataThree.pierceAttackDamage = 10;

        int damage = enemyDataThree.attackDamage - playerData.defence;
        if (damage <= 0)
            damage = 0;
        damage += enemyDataThree.pierceAttackDamage;
        playerData.curHealth -= damage;

        yield return new WaitForSeconds(5);
        Assert.AreEqual(playerData.curHealth, 10);
    }
}
