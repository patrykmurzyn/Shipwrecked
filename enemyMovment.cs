using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaEnemy
{
    readonly float heigh;


    public ParabolaEnemy(float heigh)
    {
        this.heigh = heigh;
    }

    public void Move(Transform target, Vector3 a, Vector3 b, float time, int index)
    {

        float lenght = Mathf.Abs(a.x - b.x);
        float distance;


        if (Enemy.enemies[index].GetDirection() == 0 || Enemy.enemies[index].GetDirection() == 2)
        {
            lenght = Mathf.Abs(a.x - b.x);
            distance = Mathf.Abs(target.transform.position.x - b.x);
        }
        else
        {
            lenght = Mathf.Abs(a.z - b.z);
            distance = Mathf.Abs(target.transform.position.z - b.z);
        }

        if (!target.GetComponent<enemyMovment>().GetIsFalling() && distance < 0.7f * lenght)
        {
            target.GetComponent<enemyMovment>().SetDuration(0.0001f);
            target.GetComponent<enemyMovment>().SetIsFalling(true);
        }

        float target_X = a.x + (b.x - a.x) * time;
        float target_Y = a.y + ((b.y - a.y)) * time + 0.6f * (1 - (Mathf.Abs(0.5f - time) / 0.5f) * (Mathf.Abs(0.5f - time) / 0.5f));
        float target_Z = a.z + (b.z - a.z) * time;

        target.position = Vector3.Lerp(target.position, new Vector3(target_X, target_Y, target_Z), 1f);
    }
}


public class enemyMovment : MonoBehaviour
{
    [SerializeField]
    int index;

    private ParabolaEnemy p;
    private Vector3 startPos;
    private Vector3 endPos;
    private float preTime;
    private bool onMove = false;
    private Quaternion rotateEnd;
    private bool isEnd = false;

    [SerializeField]
    private AudioClip jumpSound;

    [SerializeField]
    private float height;

    [SerializeField]
    private float duration;

    private bool isFalling = false;
    private bool isJump = false;

    private float timeToMove = 2f;

    float countTime = 0f;

    public bool GetIsFalling()
    {
        return isFalling;
    }

    public void SetDuration(float duration)
    {
        this.duration = duration;
    }

    public void SetIsFalling(bool isFalling)
    {
        this.isFalling = isFalling;
    }
    private void Awake()
    {
        p = new ParabolaEnemy(height);
    }

    private Vector2 CheckDistanceToPlayer(int p)
    {
        Vector2 distance = new Vector2(
            (float)(PlayerManage.playerObject.transform.position.x - Enemy.enemies[p].enemyObject.transform.position.x),
            (float)(PlayerManage.playerObject.transform.position.z - Enemy.enemies[p].enemyObject.transform.position.z));

        return distance;
    }

    public void GoUP()
    {
        isJump = true;

        isFalling = false;

        duration = 0.6f;

        if (!isEnd)
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

            endPos = Box.boxes[Enemy.enemies[index].position].boxObject.transform.position
                + new Vector3(0, 0.6f, 0);

        }
    }

    public void CheckIfMoveAviable()
    {
        if (onMove && preTime == 0) preTime = Time.time;

        if (!onMove)
        {
            isJump = false;
            SetJump();
        }
        else if (isJump) Jump();

    }

    public void SetJump()
    {
        preTime = 0;

        JumpOnBox();
    }

    public void JumpOnBox()
    {
        if (!isEnd)
            this.transform.position = Vector3.Lerp(this.transform.position, Box.boxes[Enemy.enemies[index].position].boxObject.transform.position
            + new Vector3(0, 0.6f, 0.05f), Time.deltaTime * 10f);

    }

    public void Jump()
    {
        if ((((Time.time - preTime) / duration) <= 1) && onMove)
        {
            //Debug.Log("transform: " + this.transform + ", startPos: " + startPos + ", endPos: " + endPos + ", time: " + (Time.time - preTime) / duration);
            p.Move(this.transform, startPos, endPos, (Time.time - preTime) /duration, index);
        }
        else
        {
            onMove = false;
            preTime = 0;

        }
    }

    private void CalculatePath()
    {
        countTime += Time.deltaTime;

        if (countTime > timeToMove)
        {
            float dx = PlayerManage.playerObject.transform.position.x - this.transform.position.x;
            float dz = PlayerManage.playerObject.transform.position.z - this.transform.position.z;

            Debug.Log("Abs(dx): " + Mathf.Abs(dx) + ", Abs(dz): " + Mathf.Abs(dz));

            if (dx <= 0 && dz <= 0)
            {
                //up
                if (Mathf.Abs(dx) >= Mathf.Abs(dz) && Box.boxes[Enemy.enemies[index].position + 35].state == 0 
                    && !Enemy.enemies[0].IsEnemyOnPosition(Enemy.enemies[index].position + 35))
                {

                    Enemy.enemies[index].direction = 2;
                    rotateEnd = Quaternion.Euler(0, 270, 0);
                    GoUP();
                }
                //left
                else if (Mathf.Abs(dx) < Mathf.Abs(dz) && Box.boxes[Enemy.enemies[index].position - 1].state == 0
                    && !Enemy.enemies[0].IsEnemyOnPosition(Enemy.enemies[index].position - 1))
                {
                    Enemy.enemies[index].direction = 1;
                    rotateEnd = Quaternion.Euler(0, 180, 0);
                    GoUP();
                }
                else if(Box.boxes[Enemy.enemies[index].position + 35].state == 0
                    && !Enemy.enemies[0].IsEnemyOnPosition(Enemy.enemies[index].position + 35))
                {
                    Enemy.enemies[index].direction = 2;
                    rotateEnd = Quaternion.Euler(0, 270, 0);
                    GoUP();
                }
                else if(Box.boxes[Enemy.enemies[index].position - 1].state == 0
                    && !Enemy.enemies[0].IsEnemyOnPosition(Enemy.enemies[index].position - 1))
                {
                    Enemy.enemies[index].direction = 1;
                    rotateEnd = Quaternion.Euler(0, 180, 0);
                    GoUP();
                }
                else
                {
                    //only turn up
                    if (Mathf.Abs(dx) >= Mathf.Abs(dz))
                    {
                        Enemy.enemies[index].direction = 2;
                        rotateEnd = Quaternion.Euler(0, 270, 0);
                    }
                    //only turn left
                    else if (Mathf.Abs(dx) < Mathf.Abs(dz))
                    {
                        Enemy.enemies[index].direction = 1;
                        rotateEnd = Quaternion.Euler(0, 180, 0);
                    }
                }
            } 
            else if(dx <= 0 && dz >= 0)
            {
                //up
                if (Mathf.Abs(dx) >= Mathf.Abs(dz) && Box.boxes[Enemy.enemies[index].position + 35].state == 0
                    && !Enemy.enemies[0].IsEnemyOnPosition(Enemy.enemies[index].position + 35))
                {

                    Enemy.enemies[index].direction = 2;
                    rotateEnd = Quaternion.Euler(0, 270, 0);
                    GoUP();
                }
                //right
                else if (Mathf.Abs(dx) < Mathf.Abs(dz) && Box.boxes[Enemy.enemies[index].position + 1].state == 0
                    && !Enemy.enemies[0].IsEnemyOnPosition(Enemy.enemies[index].position + 1))
                {
                    Enemy.enemies[index].direction = 3;
                    rotateEnd = Quaternion.Euler(0, 0, 0);
                    GoUP();
                }
                else if(Box.boxes[Enemy.enemies[index].position + 35].state == 0
                    && !Enemy.enemies[0].IsEnemyOnPosition(Enemy.enemies[index].position + 35))
                {
                    Enemy.enemies[index].direction = 2;
                    rotateEnd = Quaternion.Euler(0, 270, 0);
                    GoUP();
                }
                else if(Box.boxes[Enemy.enemies[index].position + 1].state == 0
                    && !Enemy.enemies[0].IsEnemyOnPosition(Enemy.enemies[index].position + 1))
                {
                    Enemy.enemies[index].direction = 3;
                    rotateEnd = Quaternion.Euler(0, 0, 0);
                    GoUP();
                }

                else
                {
                    //only turn up
                    if (Mathf.Abs(dx) >= Mathf.Abs(dz))
                    {
                        Enemy.enemies[index].direction = 2;
                        rotateEnd = Quaternion.Euler(0, 270, 0);
                    }
                    //only turn right
                    else if (Mathf.Abs(dx) < Mathf.Abs(dz))
                    {
                        Enemy.enemies[index].direction = 3;
                        rotateEnd = Quaternion.Euler(0, 0, 0);
                    }
                }
            }
            else if(dx >= 0 && dz <= 0)
            {
                
                //bottom
                if (Mathf.Abs(dx) >= Mathf.Abs(dz) && Box.boxes[Enemy.enemies[index].position - 35].state == 0
                    && !Enemy.enemies[0].IsEnemyOnPosition(Enemy.enemies[index].position - 35))
                {

                    Enemy.enemies[index].direction = 0;
                    rotateEnd = Quaternion.Euler(0, 90, 0);
                    GoUP();
                }
                //left
                else if (Mathf.Abs(dx) < Mathf.Abs(dz) && Box.boxes[Enemy.enemies[index].position - 1].state == 0
                    && !Enemy.enemies[0].IsEnemyOnPosition(Enemy.enemies[index].position - 1))
                {
                    Enemy.enemies[index].direction = 1;
                    rotateEnd = Quaternion.Euler(0, 180, 0);
                    GoUP();
                }
                else if(Box.boxes[Enemy.enemies[index].position - 35].state == 0
                    && !Enemy.enemies[0].IsEnemyOnPosition(Enemy.enemies[index].position - 35))
                {
                    Enemy.enemies[index].direction = 0;
                    rotateEnd = Quaternion.Euler(0, 90, 0);
                    GoUP();
                }
                else if(Box.boxes[Enemy.enemies[index].position - 1].state == 0
                    && !Enemy.enemies[0].IsEnemyOnPosition(Enemy.enemies[index].position - 1))
                {
                    Enemy.enemies[index].direction = 1;
                    rotateEnd = Quaternion.Euler(0, 180, 0);
                    GoUP();
                }

                else
                {
                    //only turn bottom
                    if (Mathf.Abs(dx) >= Mathf.Abs(dz))
                    {
                        Enemy.enemies[index].direction = 0;
                        rotateEnd = Quaternion.Euler(0, 90, 0);
                    }
                    //only turn left
                    else if (Mathf.Abs(dx) < Mathf.Abs(dz))
                    {
                        Enemy.enemies[index].direction = 1;
                        rotateEnd = Quaternion.Euler(0, 180, 0);
                    }
                }
            }
            else if(dx >= 0 && dz >= 0)
            {

                //bottom
                if (Mathf.Abs(dx) >= Mathf.Abs(dz) && Box.boxes[Enemy.enemies[index].position - 35].state == 0
                    && !Enemy.enemies[0].IsEnemyOnPosition(Enemy.enemies[index].position - 35))
                {

                    Enemy.enemies[index].direction = 0;
                    rotateEnd = Quaternion.Euler(0, 90, 0);
                    GoUP();
                }
                //right
                else if (Mathf.Abs(dx) < Mathf.Abs(dz) && Box.boxes[Enemy.enemies[index].position + 1].state == 0
                    && !Enemy.enemies[0].IsEnemyOnPosition(Enemy.enemies[index].position + 1))
                {
                    Enemy.enemies[index].direction = 3;
                    rotateEnd = Quaternion.Euler(0, 0, 0);
                    GoUP();
                }
                else if(Box.boxes[Enemy.enemies[index].position - 35].state == 0
                    && !Enemy.enemies[0].IsEnemyOnPosition(Enemy.enemies[index].position - 35))
                {
                    Enemy.enemies[index].direction = 0;
                    rotateEnd = Quaternion.Euler(0, 90, 0);
                    GoUP();
                }
                else if(Box.boxes[Enemy.enemies[index].position + 1].state == 0
                    && !Enemy.enemies[0].IsEnemyOnPosition(Enemy.enemies[index].position + 1))
                {
                    Enemy.enemies[index].direction = 3;
                    rotateEnd = Quaternion.Euler(0, 0, 0);
                    GoUP();
                }
                else
                {
                    //only turn bottom
                    if (Mathf.Abs(dx) >= Mathf.Abs(dz))
                    {
                        Enemy.enemies[index].direction = 0;
                        rotateEnd = Quaternion.Euler(0, 90, 0);
                    }
                    //only turn right
                    else if (Mathf.Abs(dx) < Mathf.Abs(dz))
                    {
                        Enemy.enemies[index].direction = 3;
                        rotateEnd = Quaternion.Euler(0, 0, 0);
                    }
                }
            }

            countTime = 0f;
        }

    }

    private void Update()
    {
        CalculatePath();

        CheckIfMoveAviable();

        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotateEnd, 5f * Time.deltaTime);
    }

}
