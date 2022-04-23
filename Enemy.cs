using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    private int position;
    private int direction;
    private GameObject enemyObject;
    private static List<Enemy> enemies = new List<Enemy>();

    public int GetPosition()
    {
        return position;
    }

    public void SetPosition(int position)
    {
        this.position = position;
    }

    public void AddToPosition(int index)
    {
        this.position += index;
    }

    public int GetDirection()
    {
        return direction;
    }

    public void SetDirection(int direction)
    {
        this.direction = direction;
    }

    public GameObject GetEnemyObject()
    {
        return enemyObject;
    }

    public void SetEnemyObject(GameObject enemyObject)
    {
        this.enemyObject = enemyObject;
    }

    public static Enemy GetEnemies(int index)
    {
        return enemies[index];
    }

    public static void AddEnemies(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public static bool IsEnemyOnPosition(int position)
    {
        bool isFound = false;

        for (int i = 0; i < enemies.Count; i++)
        {
            if (position == enemies[i].position) isFound = true;
        }

        return isFound;
    }

    public static bool CheckIfEnemyOnPosition(int position)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].position == position) return true;
        }
        return false;
    }

    public static void ClearList()
    {
        enemies.Clear();
    }
}

