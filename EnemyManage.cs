using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
    public int position;
    public int direction;
    public GameObject enemyObject;
    public static List<Enemy> enemies = new List<Enemy>();

    public bool IsEnemyOnPosition(int position)
    {
        bool isFound = false;

        for(int i = 0; i < enemies.Count; i++)
        {
            if (position == enemies[i].position) isFound = true;
        }

        return isFound;
    }

    public int GetDirection()
    {
        return direction;
    }

    public static bool CheckIfEnemyOnPosition(int position)
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i].position == position) return true;
        }
        return false;
    }
}

public class EnemyManage : MonoBehaviour
{
    void CreateEnemyList()
    {
        foreach (Transform i in transform)
        {
            Enemy temp = new Enemy
            {
                enemyObject = i.gameObject,
                
            };

            Enemy.enemies.Add(temp);
        }
    }

    void SetEnemies()
    {
        for(int i = 0; i < 4; i++)
        {

            int temp;

            do
            {
                temp = Random.Range(0, 420);

            } while (Box.boxes[temp].state != 0 ||
            Vector3.Distance(PlayerManage.playerObject.transform.position, Box.boxes[temp].boxObject.transform.position) < 12);

            Enemy.enemies[i].enemyObject.transform.position = Box.boxes[temp].boxObject.transform.position
            + new Vector3(0, 0.6f, 0.05f);

            Enemy.enemies[i].position = Box.boxes[temp].position;

            Enemy.enemies[i].direction = Random.Range(0, 4);

            Box.boxes[temp].state = 5;

            if (Enemy.enemies[i].direction == 0)
            {
                Enemy.enemies[i].enemyObject.transform.eulerAngles = new Vector3(0, 90, 0);
            }
            else if (Enemy.enemies[i].direction == 1)
            {
                Enemy.enemies[i].enemyObject.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (Enemy.enemies[i].direction == 2)
            {
                Enemy.enemies[i].enemyObject.transform.eulerAngles = new Vector3(0, 270, 0);
            }
            else if (Enemy.enemies[i].direction == 3)
            {
                Enemy.enemies[i].enemyObject.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    private void Awake()
    {
        CreateEnemyList();

        SetEnemies();
    }
}
