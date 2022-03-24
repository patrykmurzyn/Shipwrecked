using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public static int position;
    public static int direction;
    public static GameObject playerObject;

}

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

public class PlayerManage : MonoBehaviour
{
    private PlayerAction playerAction;

    private Parabola p;
    private Vector3 startPos;
    private Vector3 endPos;
    private float preTime;
    private bool onMove = false;
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

    public void SetIsEndTrue()
    {
        isEnd = true;
    }

    void Awake()
    {
        Player.playerObject = this.gameObject;

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

        if (!isEnd)
        {

            preTime = 0;

            audio.PlayOneShot(jumpSound);

            onMove = true;

            if (Player.direction == 0)

                Player.position -= 35;


            else if (Player.direction == 1)

                Player.position -= 1;


            else if (Player.direction == 2)

                Player.position += 35;


            else if (Player.direction == 3)

                Player.position += 1;

            startPos = this.transform.position;

            if (Turtle.GetIndex(Player.position) != -1)
            {
                endPos = Turtle.turtles[Turtle.GetIndex(Player.position)].turtleObject.transform.position
                + new Vector3(0, 0.6f, 0);

                Turtle.goDownList.Add(Turtle.GetIndex(Player.position));

                Turtle.turtles[Turtle.GetIndex(Player.position)].turtleObject.GetComponent<FloatEffect>().enabled = false;

            }
            else
            {
                endPos = Box.boxes[Player.position].boxObject.transform.position
                + new Vector3(0, 0.6f, 0);
            }

            if (Box.boxes[Player.position].state == 3)
            {
                endPos = Box.boxes[Player.position].boxObject.transform.position
                    + new Vector3(0, -1f, 0);

                isEnd = true;
            }

        }
    }

    void GoLeft()
    {
        if (!isEnd)
        {
            if (Player.direction == 0)
            {
                Player.direction = 3;
                rotateEnd = Quaternion.Euler(0, 0, 0);
            }

            else if (Player.direction == 1)
            {
                Player.direction = 0;
                rotateEnd = Quaternion.Euler(0, 90, 0);
            }

            else if (Player.direction == 2)
            {
                Player.direction = 1;
                rotateEnd = Quaternion.Euler(0, 180, 0);
            }

            else if (Player.direction == 3)
            {
                Player.direction = 2;
                rotateEnd = Quaternion.Euler(0, 270, 0);
            }
        }
    }

    void GoRight()
    {
        if (!isEnd)
        {
            if (Player.direction == 0)
            {
                Player.direction = 1;
                rotateEnd = Quaternion.Euler(0, 180, 0);
            }

            else if (Player.direction == 1)
            {
                Player.direction = 2;
                rotateEnd = Quaternion.Euler(0, 270, 0);
            }

            else if (Player.direction == 2)
            {
                Player.direction = 3;
                rotateEnd = Quaternion.Euler(0, 0, 0);
            }

            else if (Player.direction == 3)
            {
                Player.direction = 0;
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

        Player.position = Box.boxes[temp].position;
        Player.direction = Random.Range(0, 4);

        this.transform.position = Box.boxes[temp].boxObject.transform.position
            + new Vector3(0, 0.6f, 0.05f);

        if (Player.direction == 0)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else if (Player.direction == 1)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (Player.direction == 2)
        {
            transform.eulerAngles = new Vector3(0, 270, 0);
        }
        else if (Player.direction == 3)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void CheckIfMoveAviable()
    {
        if (onMove && preTime == 0) preTime = Time.time;

        if (!onMove) SetJump();

        Jump();

    }

    private void SetJump()
    {
        preTime = 0;

        if (Turtle.GetIndex(Player.position) != -1)
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
            transform.position = Vector3.Lerp(transform.position, Turtle.turtles[Turtle.GetIndex(Player.position)].turtleObject.transform.position
            + new Vector3(0, 0.6f, 0.05f), Time.fixedDeltaTime * 10f);
    }

    private void JumpOnBox()
    {
        if (!isEnd)
            transform.position = Vector3.Lerp(transform.position, Box.boxes[Player.position].boxObject.transform.position
            + new Vector3(0, 0.6f, 0.05f), Time.fixedDeltaTime * 10f);

    }

    private void Jump()
    {
        if ((((Time.time - preTime) / duration) <= 1) && onMove)
        {
            p.Move(this.transform, startPos, endPos, (Time.fixedTime - preTime) / duration);
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

        if (Box.boxes[Player.position].state != 4 &&
            Box.boxes[Player.position].state != 3 &&
            Box.boxes[Player.position].boxObject.transform.position.y < -.7f)
        {
            endPos = Box.boxes[Player.position].boxObject.transform.position
                    + new Vector3(0, -1f, 0);

            isEnd = true;
        }

        if(isEnd && !onMove)
        {
            transform.position = Vector3.Lerp(transform.position, Box.boxes[Player.position].boxObject.transform.position
            + new Vector3(0, -2f, 0.05f), Time.fixedDeltaTime * 1f);
        }
    }

    void Start()
    {
        SetStartPosition();

        rotateEnd = transform.rotation;

        p = new Parabola(height);

        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CheckInputs();

        CheckIfEnd();

    }


    void FixedUpdate()
    {
        CheckIfMoveAviable();

        transform.rotation = Quaternion.Lerp(transform.rotation, rotateEnd, 5f * Time.fixedDeltaTime);
    }
}
