using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovment : MonoBehaviour
{
    [SerializeField]
    int index;

    private Parabola p;
    private Vector3 startPos;
    private Vector3 endPos;
    private float preTime;
    private bool onMove = false;
    private Quaternion rotateEnd;

    private void Start()
    {
        Debug.Log(Enemy.enemies[index].enemyObject.transform.position);
    }

    private Vector2 CheckDistanceToPlayer(int p)
    {
        Vector2 distance = new Vector2(
            (float)(Player.playerObject.transform.position.x - Enemy.enemies[p].enemyObject.transform.position.x),
            (float)(Player.playerObject.transform.position.z - Enemy.enemies[p].enemyObject.transform.position.z));

        return distance;
    }

    void GoUP()
    {

            preTime = 0;

            onMove = true;

            if (Enemy.enemies[index].direction == 0)

                Enemy.enemies[index].position -= 35;


            else if (Enemy.enemies[index].direction == 1)

                Enemy.enemies[index].position -= 1;


            else if (Enemy.enemies[index].direction == 2)

                Enemy.enemies[index].position += 35;


            else if (Enemy.enemies[index].direction == 3)

                Enemy.enemies[index].position += 1;

            startPos = this.transform.position;

            if (Player.position == Enemy.enemies[index].position)
            {
                

            }
    }

    private void Move()
    {
        if(Mathf.Abs(CheckDistanceToPlayer(index).x) > Mathf.Abs(CheckDistanceToPlayer(index).y))
        {
            if (CheckDistanceToPlayer(index).x > 0)
            {

            }
            else if (CheckDistanceToPlayer(index).x <= 0)
            {

            }
        } else
        {
            if (CheckDistanceToPlayer(index).y > 0)
            {

            }
            else if (CheckDistanceToPlayer(index).y <= 0)
            {
                
            }
        }

        
    }

    private void FixedUpdate()
    {
        
    }

}
