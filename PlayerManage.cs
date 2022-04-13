using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private static bool isEnd = false;

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

    public static void SetIsEnd(bool option)
    {
        isEnd = option;
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

    public void GoUP()
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

    public void GoLeft()
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

    public void GoRight()
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

        if (Turtle.GetIndex(playerPosition) != -1)
        {
            JumpOnTurtle();

        }
        else
        {
            JumpOnBox();
        }
    }

    public void JumpOnTurtle()
    {
        if(!isEnd)
            transform.position = Vector3.Lerp(transform.position, Turtle.turtles[Turtle.GetIndex(playerPosition)].turtleObject.transform.position
            + new Vector3(0, 0.6f, 0.05f), Time.deltaTime * 10f);
    }

    public void JumpOnBox()
    {
        if (!isEnd)
            transform.position = Vector3.Lerp(transform.position, Box.boxes[playerPosition].boxObject.transform.position
            + new Vector3(0, 0.6f, 0.05f), Time.deltaTime * 10f);

    }

    public void Jump()
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

    private void CheckIfEnd()
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
        Application.targetFrameRate = 61;

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
