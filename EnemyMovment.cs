using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    [SerializeField]
    private int index;

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

    private static float timeToMove = 2f;

    float countTime = 0f;

    public bool GetIsFalling()
    {
        return isFalling;
    }

    public static float GetTimeToMove()
    {
        return timeToMove;
    }

    public static void SetTimeToMove(float newTime)
    {
        timeToMove = newTime;
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
            (float)(PlayerManage.GetPlayerObject().transform.position.x - Enemy.GetEnemies(p).GetEnemyObject().transform.position.x),
            (float)(PlayerManage.GetPlayerObject().transform.position.z - Enemy.GetEnemies(p).GetEnemyObject().transform.position.z));

        return distance;
    }

    private void GoUP()
    {
        isJump = true;

        isFalling = false;

        duration = 0.6f;

        preTime = 0;

        onMove = true;

        if (Enemy.GetEnemies(index).GetDirection() == 0)

            Enemy.GetEnemies(index).AddToPosition(-35);

        else if (Enemy.GetEnemies(index).GetDirection() == 1)

            Enemy.GetEnemies(index).AddToPosition(-1);

        else if (Enemy.GetEnemies(index).GetDirection() == 2)

            Enemy.GetEnemies(index).AddToPosition(35);

        else if (Enemy.GetEnemies(index).GetDirection() == 3)

        Enemy.GetEnemies(index).AddToPosition(1);

        startPos = this.transform.position;

        endPos = Box.GetBoxes(Enemy.GetEnemies(index).GetPosition()).GetBoxObject().transform.position
            + new Vector3(0, 0.6f, 0);

    }

    private void CheckIfMoveAviable()
    {
        if (onMove && preTime == 0) preTime = Time.time;

        if (!onMove)
        {
            isJump = false;
            SetJump();
        }
        else if (isJump) Jump();

    }

    private void SetJump()
    {
        preTime = 0;

        JumpOnBox();
    }

    private void JumpOnBox()
    {
        if (!isEnd)
            this.transform.position = Vector3.Lerp(this.transform.position, Box.GetBoxes(Enemy.GetEnemies(index).GetPosition()).GetBoxObject().transform.position
            + new Vector3(0, 0.6f, 0.05f), Time.deltaTime * 10f);

    }

    private void Jump()
    {
        if ((((Time.time - preTime) / duration) <= 1) && onMove)
        {
            p.Move(this.transform, startPos, endPos, (Time.time - preTime) / duration, index);
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

            float dx = PlayerManage.GetPlayerObject().transform.position.x - this.transform.position.x;
            float dz = PlayerManage.GetPlayerObject().transform.position.z - this.transform.position.z;

            if (dx <= 0 && dz <= 0)
            {
                //up
                if (Mathf.Abs(dx) >= Mathf.Abs(dz) && Box.GetBoxes(Enemy.GetEnemies(index).GetPosition() + 35).GetState() == 0
                    && !Enemy.IsEnemyOnPosition(Enemy.GetEnemies(index).GetPosition() + 35))
                {

                    Enemy.GetEnemies(index).SetDirection(2);
                    rotateEnd = Quaternion.Euler(0, 270, 0);
                    GoUP();
                }
                //left
                else if (Mathf.Abs(dx) < Mathf.Abs(dz) && Box.GetBoxes(Enemy.GetEnemies(index).GetPosition() - 1).GetState() == 0
                    && !Enemy.IsEnemyOnPosition(Enemy.GetEnemies(index).GetPosition() - 1))
                {
                    Enemy.GetEnemies(index).SetDirection(1);
                    rotateEnd = Quaternion.Euler(0, 180, 0);
                    GoUP();
                }
                else if (Box.GetBoxes(Enemy.GetEnemies(index).GetPosition() + 35).GetState() == 0
                    && !Enemy.IsEnemyOnPosition(Enemy.GetEnemies(index).GetPosition() + 35))
                {
                    Enemy.GetEnemies(index).SetDirection(2);
                    rotateEnd = Quaternion.Euler(0, 270, 0);
                    GoUP();
                }
                else if (Box.GetBoxes(Enemy.GetEnemies(index).GetPosition() - 1).GetState() == 0
                    && !Enemy.IsEnemyOnPosition(Enemy.GetEnemies(index).GetPosition() - 1))
                {
                    Enemy.GetEnemies(index).SetDirection(1);
                    rotateEnd = Quaternion.Euler(0, 180, 0);
                    GoUP();
                }
                else
                {
                    //only turn up
                    if (Mathf.Abs(dx) >= Mathf.Abs(dz))
                    {
                        Enemy.GetEnemies(index).SetDirection(2);
                        rotateEnd = Quaternion.Euler(0, 270, 0);
                    }
                    //only turn left
                    else if (Mathf.Abs(dx) < Mathf.Abs(dz))
                    {
                        Enemy.GetEnemies(index).SetDirection(1);
                        rotateEnd = Quaternion.Euler(0, 180, 0);
                    }
                }
            }
            else if (dx <= 0 && dz >= 0)
            {
                //up
                if (Mathf.Abs(dx) >= Mathf.Abs(dz) && Box.GetBoxes(Enemy.GetEnemies(index).GetPosition() + 35).GetState() == 0
                    && !Enemy.IsEnemyOnPosition(Enemy.GetEnemies(index).GetPosition() + 35))
                {

                    Enemy.GetEnemies(index).SetDirection(2);
                    rotateEnd = Quaternion.Euler(0, 270, 0);
                    GoUP();
                }
                //right
                else if (Mathf.Abs(dx) < Mathf.Abs(dz) && Box.GetBoxes(Enemy.GetEnemies(index).GetPosition() + 1).GetState() == 0
                    && !Enemy.IsEnemyOnPosition(Enemy.GetEnemies(index).GetPosition() + 1))
                {
                    Enemy.GetEnemies(index).SetDirection(3);
                    rotateEnd = Quaternion.Euler(0, 0, 0);
                    GoUP();
                }
                else if (Box.GetBoxes(Enemy.GetEnemies(index).GetPosition() + 35).GetState() == 0
                    && !Enemy.IsEnemyOnPosition(Enemy.GetEnemies(index).GetPosition() + 35))
                {
                    Enemy.GetEnemies(index).SetDirection(2);
                    rotateEnd = Quaternion.Euler(0, 270, 0);
                    GoUP();
                }
                else if (Box.GetBoxes(Enemy.GetEnemies(index).GetPosition() + 1).GetState() == 0
                    && !Enemy.IsEnemyOnPosition(Enemy.GetEnemies(index).GetPosition() + 1))
                {
                    Enemy.GetEnemies(index).SetDirection(3);
                    rotateEnd = Quaternion.Euler(0, 0, 0);
                    GoUP();
                }

                else
                {
                    //only turn up
                    if (Mathf.Abs(dx) >= Mathf.Abs(dz))
                    {
                        Enemy.GetEnemies(index).SetDirection(2);
                        rotateEnd = Quaternion.Euler(0, 270, 0);
                    }
                    //only turn right
                    else if (Mathf.Abs(dx) < Mathf.Abs(dz))
                    {
                        Enemy.GetEnemies(index).SetDirection(3);
                        rotateEnd = Quaternion.Euler(0, 0, 0);
                    }
                }
            }
            else if (dx >= 0 && dz <= 0)
            {

                //bottom
                if (Mathf.Abs(dx) >= Mathf.Abs(dz) && Box.GetBoxes(Enemy.GetEnemies(index).GetPosition() - 35).GetState() == 0
                    && !Enemy.IsEnemyOnPosition(Enemy.GetEnemies(index).GetPosition() - 35))
                {

                    Enemy.GetEnemies(index).SetDirection(0);
                    rotateEnd = Quaternion.Euler(0, 90, 0);
                    GoUP();
                }
                //left
                else if (Mathf.Abs(dx) < Mathf.Abs(dz) && Box.GetBoxes(Enemy.GetEnemies(index).GetPosition() - 1).GetState() == 0
                    && !Enemy.IsEnemyOnPosition(Enemy.GetEnemies(index).GetPosition() - 1))
                {
                    Enemy.GetEnemies(index).SetDirection(1);
                    rotateEnd = Quaternion.Euler(0, 180, 0);
                    GoUP();
                }
                else if (Box.GetBoxes(Enemy.GetEnemies(index).GetPosition() - 35).GetState() == 0
                    && !Enemy.IsEnemyOnPosition(Enemy.GetEnemies(index).GetPosition() - 35))
                {
                    Enemy.GetEnemies(index).SetDirection(0);
                    rotateEnd = Quaternion.Euler(0, 90, 0);
                    GoUP();
                }
                else if (Box.GetBoxes(Enemy.GetEnemies(index).GetPosition() - 1).GetState() == 0
                    && !Enemy.IsEnemyOnPosition(Enemy.GetEnemies(index).GetPosition() - 1))
                {
                    Enemy.GetEnemies(index).SetDirection(1);
                    rotateEnd = Quaternion.Euler(0, 180, 0);
                    GoUP();
                }

                else
                {
                    //only turn bottom
                    if (Mathf.Abs(dx) >= Mathf.Abs(dz))
                    {
                        Enemy.GetEnemies(index).SetDirection(0);
                        rotateEnd = Quaternion.Euler(0, 90, 0);
                    }
                    //only turn left
                    else if (Mathf.Abs(dx) < Mathf.Abs(dz))
                    {
                        Enemy.GetEnemies(index).SetDirection(1);
                        rotateEnd = Quaternion.Euler(0, 180, 0);
                    }
                }
            }
            else if (dx >= 0 && dz >= 0)
            {

                //bottom
                if (Mathf.Abs(dx) >= Mathf.Abs(dz) && Box.GetBoxes(Enemy.GetEnemies(index).GetPosition() - 35).GetState() == 0
                    && !Enemy.IsEnemyOnPosition(Enemy.GetEnemies(index).GetPosition() - 35))
                {

                    Enemy.GetEnemies(index).SetDirection(0);
                    rotateEnd = Quaternion.Euler(0, 90, 0);
                    GoUP();
                }
                //right
                else if (Mathf.Abs(dx) < Mathf.Abs(dz) && Box.GetBoxes(Enemy.GetEnemies(index).GetPosition() + 1).GetState() == 0
                    && !Enemy.IsEnemyOnPosition(Enemy.GetEnemies(index).GetPosition() + 1))
                {
                    Enemy.GetEnemies(index).SetDirection(3);
                    rotateEnd = Quaternion.Euler(0, 0, 0);
                    GoUP();
                }
                else if (Box.GetBoxes(Enemy.GetEnemies(index).GetPosition() - 35).GetState() == 0
                    && !Enemy.IsEnemyOnPosition(Enemy.GetEnemies(index).GetPosition() - 35))
                {
                    Enemy.GetEnemies(index).SetDirection(0);
                    rotateEnd = Quaternion.Euler(0, 90, 0);
                    GoUP();
                }
                else if (Box.GetBoxes(Enemy.GetEnemies(index).GetPosition() + 1).GetState() == 0
                    && !Enemy.IsEnemyOnPosition(Enemy.GetEnemies(index).GetPosition() + 1))
                {
                    Enemy.GetEnemies(index).SetDirection(3);
                    rotateEnd = Quaternion.Euler(0, 0, 0);
                    GoUP();
                }
                else
                {
                    //only turn bottom
                    if (Mathf.Abs(dx) >= Mathf.Abs(dz))
                    {
                        Enemy.GetEnemies(index).SetDirection(0);
                        rotateEnd = Quaternion.Euler(0, 90, 0);
                    }
                    //only turn right
                    else if (Mathf.Abs(dx) < Mathf.Abs(dz))
                    {
                        Enemy.GetEnemies(index).SetDirection(3);
                        rotateEnd = Quaternion.Euler(0, 0, 0);
                    }
                }
            }

            if (Enemy.GetEnemies(index).GetPosition() == PlayerManage.GetPlayerPosition())
            {
                PlayerManage.SetIsEnd(true);
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
