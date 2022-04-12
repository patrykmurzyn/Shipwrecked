using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class Parabola
{
    readonly float heigh;

    public Parabola(float heigh)
    {
        this.heigh = heigh;
    }

    public void Move(Transform target, Vector3 a, Vector3 b, float time)
    {
        float target_X = a.x + (b.x - a.x) * time;
        float target_Y = a.y + ((b.y - a.y)) * time + heigh * (1 - (Mathf.Abs(0.5f - time) / 0.5f) * (Mathf.Abs(0.5f - time) / 0.5f));
        float target_Z = a.z + (b.z - a.z) * time;

        target.position = Vector3.Lerp(target.position, new Vector3(target_X, target_Y, target_Z), 1f);
    }
}
*/

public class Parabola
{
    readonly float heigh;
    

    public Parabola(float heigh)
    {
        this.heigh = heigh;
    }

    public void Move(Transform target, Vector3 a, Vector3 b, float time)
    {

        float lenght = Mathf.Abs(a.x - b.x);
        float distance;


        if(target.GetComponent<PlayerManage>().GetDirection() == 0 || target.GetComponent<PlayerManage>().GetDirection() == 2)
        {
            lenght = Mathf.Abs(a.x - b.x);
            distance = Mathf.Abs(target.transform.position.x - b.x);
        } else
        {
            lenght = Mathf.Abs(a.z - b.z);
            distance = Mathf.Abs(target.transform.position.z - b.z);
        }

        if(!target.GetComponent<PlayerManage>().GetIsFalling() && distance < 0.7f * lenght)
        {
            target.GetComponent<PlayerManage>().SetDuration(0.2f);
            target.GetComponent<PlayerManage>().SetIsFalling(true);
        }

        float target_X = a.x + (b.x - a.x) * time;
        float target_Y = a.y + ((b.y - a.y)) * time + heigh * (1 - (Mathf.Abs(0.5f - time) / 0.5f) * (Mathf.Abs(0.5f - time) / 0.5f));
        float target_Z = a.z + (b.z - a.z) * time;

        target.position = Vector3.Lerp(target.position, new Vector3(target_X, target_Y, target_Z), 1f);
    }
}

public class PlayerManage : MonoBehaviour
{
    public static int playerPosition;
    public static int playerDirection;
    public static GameObject playerObject;

    private PlayerAction playerAction;

    private Parabola p;
    private Vector3 startPos;
    private Vector3 endPos;
    private float preTime;
    private static bool onMove = false;
    private Quaternion rotateEnd;
    private bool isEnd = false;

    [SerializeField]
    private AudioClip jumpSound;
    [SerializeField]
    private AudioClip JumpWater;
    new AudioSource audio;
    private bool isEndSound = false;

    [SerializeField]
    private float height;

    [SerializeField]
    private float duration;

    private bool isJump = false;
    private bool isFalling = false;

    public void SetDuration(float duration)
    {
        this.duration = duration;
    }

    public float GetPreTime()
    {
        return this.preTime;
    }

    public int GetDirection()
    {
        return playerDirection;
    }

    public bool GetIsFalling()
    {
        return isFalling;
    }

    public void SetIsFalling(bool isFalling)
    {
        this.isFalling = isFalling;
    }

    void Awake()
    {
        playerObject = this.gameObject;

        playerAction = new PlayerAction();
    }

    void OnEnable()
    {
        playerAction.Enable();
    }

    void OnDisable()
    {
        playerAction.Disable();
    }

    void GoUP()
    {
        isJump = true;

        isFalling = false;

        duration = 0.6f;


        if (!isEnd)
        {

            preTime = 0;

            audio.PlayOneShot(jumpSound);

            onMove = true;

            if (playerDirection == 0)

                playerPosition -= 35;


            else if (playerDirection == 1)

                playerPosition -= 1;


            else if (playerDirection == 2)

                playerPosition += 35;


            else if (playerDirection == 3)

                playerPosition += 1;

            startPos = this.transform.position;

            if (Turtle.GetIndex(playerPosition) != -1)
            {
                endPos = Turtle.turtles[Turtle.GetIndex(playerPosition)].turtleObject.transform.position
                + new Vector3(0, 0.6f, 0);

                Turtle.goDownList.Add(Turtle.GetIndex(playerPosition));

                Turtle.turtles[Turtle.GetIndex(playerPosition)].turtleObject.GetComponent<FloatEffect>().enabled = false;

            }
            else
            {
                endPos = Box.boxes[playerPosition].boxObject.transform.position
                + new Vector3(0, 0.6f, 0);
            }

            if (Box.boxes[playerPosition].state == 3)
            {
                endPos = Box.boxes[playerPosition].boxObject.transform.position
                    + new Vector3(0, -1f, 0);

                isEnd = true;
            }
            
        }
    }

    void GoLeft()
    {
        if (!isEnd)
        {
            if (playerDirection == 0)
            {
                playerDirection = 3;
                rotateEnd = Quaternion.Euler(0, 0, 0);
            }

            else if (playerDirection == 1)
            {
                playerDirection = 0;
                rotateEnd = Quaternion.Euler(0, 90, 0);
            }

            else if (playerDirection == 2)
            {
                playerDirection = 1;
                rotateEnd = Quaternion.Euler(0, 180, 0);
            }

            else if (playerDirection == 3)
            {
                playerDirection = 2;
                rotateEnd = Quaternion.Euler(0, 270, 0);
            }
        }
    }

    void GoRight()
    {
        if (!isEnd)
        {
            if (playerDirection == 0)
            {
                playerDirection = 1;
                rotateEnd = Quaternion.Euler(0, 180, 0);
            }

            else if (playerDirection == 1)
            {
                playerDirection = 2;
                rotateEnd = Quaternion.Euler(0, 270, 0);
            }

            else if (playerDirection == 2)
            {
                playerDirection = 3;
                rotateEnd = Quaternion.Euler(0, 0, 0);
            }

            else if (playerDirection == 3)
            {
                playerDirection = 0;
                rotateEnd = Quaternion.Euler(0, 90, 0);
            }
        }
    }

    void CheckInputs()
    {
        if (playerAction.Player.Up.triggered)
        {
            GoUP();
        }

        if (playerAction.Player.Left.triggered)
        {
            GoLeft();
        }

        if (playerAction.Player.Right.triggered)
        {
            GoRight();
        }
    }

    private void SetStartPosition()
    {
        int temp;

        do
        {
            temp = Random.Range(0, 420);
        } while (Box.boxes[temp].state != 0);

        playerPosition = Box.boxes[temp].position;
        playerDirection = Random.Range(0, 4);

        this.transform.position = Box.boxes[temp].boxObject.transform.position
            + new Vector3(0, 0.6f, 0.05f);

        if (playerDirection == 0)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else if (playerDirection == 1)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (playerDirection == 2)
        {
            transform.eulerAngles = new Vector3(0, 270, 0);
        }
        else if (playerDirection == 3)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
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

        if (Turtle.GetIndex(playerPosition) != -1)
        {
            JumpOnTurtle();

        }
        else
        {
            JumpOnBox();
        }
    }

    private void JumpOnTurtle()
    {
        if(!isEnd)
            transform.position = Vector3.Lerp(transform.position, Turtle.turtles[Turtle.GetIndex(playerPosition)].turtleObject.transform.position
            + new Vector3(0, 0.6f, 0.05f), Time.deltaTime * 10f);
    }

    private void JumpOnBox()
    {
        if (!isEnd)
            transform.position = Vector3.Lerp(transform.position, Box.boxes[playerPosition].boxObject.transform.position
            + new Vector3(0, 0.6f, 0.05f), Time.deltaTime * 10f);

    }

    private void Jump()
    {
        if ((((Time.time - preTime) / duration) <= 1) && onMove)
        {

            p.Move(this.transform, startPos, endPos, (Time.time - preTime) / duration);
        }
        else
        {
            onMove = false;
            preTime = 0;

        }
    }

    void CheckIfEnd()
    {
        if (this.transform.position.y <= -.3f && !isEndSound)
        {
            isEnd = true;

            audio.PlayOneShot(JumpWater);

            isEndSound = true;
        }

        if (Box.boxes[playerPosition].state != 4 &&
            Box.boxes[playerPosition].state != 3 &&
            Box.boxes[playerPosition].boxObject.transform.position.y < -.7f)
        {
            endPos = Box.boxes[playerPosition].boxObject.transform.position
                    + new Vector3(0, -1f, 0);

            isEnd = true;
        }

        if(isEnd && !onMove)
        {
            transform.position = Vector3.Lerp(transform.position, Box.boxes[playerPosition].boxObject.transform.position
            + new Vector3(0, -2f, 0.05f), Time.deltaTime * 1f);
        }
    }

    void Start()
    {
        Application.targetFrameRate = 60;

        SetStartPosition();

        rotateEnd = transform.rotation;

        p = new Parabola(height);

        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckInputs();

        CheckIfEnd();

        CheckIfMoveAviable();

        transform.rotation = Quaternion.Lerp(transform.rotation, rotateEnd, 5f * Time.deltaTime);

    }
}
