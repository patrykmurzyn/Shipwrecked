using UnityEngine;
using TMPro;

[DefaultExecutionOrder(-2)]
public class PlayerManage : MonoBehaviour
{
    private static int playerPosition;
    private static int playerDirection;
    private static int playerPoints;
    private static int playerPointsSecure;
    private static GameObject playerObject;
    [SerializeField]
    private GameObject[] skins;

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
    [SerializeField]
    private AudioClip coinCollect;
    new AudioSource audio;
    private bool isEndSound = false;

    [SerializeField]
    private float height;

    [SerializeField]
    private float duration;

    private bool isJump = false;
    private bool isFalling = false;

    [SerializeField]
    private GameObject theEndTitle;
    [SerializeField]
    private GameObject playAgainButton;
    [SerializeField]
    public GameObject homeButton;
    [SerializeField]
    public GameObject scoreText;
    [SerializeField]
    public GameObject scoreValue;
    [SerializeField]
    public GameObject smallScore;
    [SerializeField]
    public GameObject backgroundSound;
    [SerializeField]
    public GameObject touchPad;
    [SerializeField]
    public GameObject swipeManager;

    private bool isCoinsAdded = false;


    public static int GetPlayerPosition()
    {
        return playerPosition;
    }

    public static int GetPlayerDirection()
    {
        return playerDirection;
    }
    public static int GetPlayerPoints()
    {
        if (playerPoints + 79 == playerPointsSecure)
        {
            return playerPoints;
        }
        return playerPointsSecure - 79;
    }

    public static void AddPlayerPoint()
    {
        if (playerPoints + 79 == playerPointsSecure)
        {
            playerPoints++;
            playerPointsSecure++;
        } else
        {
            playerPoints = playerPointsSecure - 79;
        }

    }

    public static GameObject GetPlayerObject()
    {
        return playerObject;
    }

    public void SetDuration(float duration)
    {
        this.duration = duration;
    }

    public static void SetIsEnd(bool option)
    {
        isEnd = option;
    }

    public bool GetIsFalling()
    {
        return isFalling;
    }

    public void SetIsFalling(bool isFalling)
    {
        this.isFalling = isFalling;
    }

    public static float CheckDistanceToPlayer(int position)
    {
        return Vector3.Distance(playerObject.transform.position, Box.GetBoxes(position).GetBoxObject().transform.position);
    }

    private void Awake()
    {
        playerObject = this.gameObject;

        playerAction = new PlayerAction();
    }

    private void OnEnable()
    {
        playerAction.Enable();
    }

    private void OnDisable()
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

            if (Turtle.SearchIndexByPosition(playerPosition) != -1)
            {
                endPos = Turtle.GetTurtles(Turtle.SearchIndexByPosition(playerPosition)).GetTurtleObject().transform.position
                + new Vector3(0, 0.6f, 0);

                Turtle.AddGoDownList(Turtle.SearchIndexByPosition(playerPosition));

                Turtle.GetTurtles(Turtle.SearchIndexByPosition(playerPosition)).SetState(1);

                Turtle.GetTurtles(Turtle.SearchIndexByPosition(playerPosition)).GetTurtleObject().GetComponent<FloatEffect>().enabled = false;

            }
            else
            {
                endPos = Box.GetBoxes(playerPosition).GetBoxObject().transform.position
                + new Vector3(0, 0.6f, 0);
            }

            if (Box.GetBoxes(playerPosition).GetState() == 3)
            {
                endPos = Box.GetBoxes(playerPosition).GetBoxObject().transform.position
                    + new Vector3(0, -1f, 0);

                isEnd = true;
            }

            if (Enemy.IsEnemyOnPosition(playerPosition))
            {
                isEnd = true;
            }

            if(CoinManage.AddPointIfPlayerIsOnCoin())
            {
                audio.PlayOneShot(coinCollect);
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

    private void CheckInputs()
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
        } while (Box.GetBoxes(temp).GetState() != 0);

        playerPosition = Box.GetBoxes(temp).GetPosition();
        playerDirection = Random.Range(0, 4);

        this.transform.position = Box.GetBoxes(temp).GetBoxObject().transform.position
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

        if (Turtle.SearchIndexByPosition(playerPosition) != -1)
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
        if (!isEnd)
            transform.position = Vector3.Lerp(transform.position, Turtle.GetTurtles(Turtle.SearchIndexByPosition(playerPosition)).GetTurtleObject().transform.position
            + new Vector3(0, 0.6f, 0.05f), Time.deltaTime * 10f);
    }

    private void JumpOnBox()
    {
        if (!isEnd)
            transform.position = Vector3.Lerp(transform.position, Box.GetBoxes(playerPosition).GetBoxObject().transform.position
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

    private void CheckIfEnd()
    {
        if (this.transform.position.y <= -.3f && !isEndSound)
        {
            isEnd = true;

            audio.PlayOneShot(JumpWater);

            isEndSound = true;
        }

        if (Box.GetBoxes(playerPosition).GetState() != 4 &&
            Box.GetBoxes(playerPosition).GetState() != 3 &&
            Box.GetBoxes(playerPosition).GetBoxObject().transform.position.y < -.7f)
        {
            endPos = Box.GetBoxes(playerPosition).GetBoxObject().transform.position
                    + new Vector3(0, -1f, 0);

            isEnd = true;
        }

        if (isEnd && !onMove)
        {
            transform.position = Vector3.Lerp(transform.position, Box.GetBoxes(playerPosition).GetBoxObject().transform.position
            + new Vector3(0, -2f, 0.05f), Time.deltaTime * 1f);

            scoreValue.GetComponent<TextMeshProUGUI>().text = playerPoints.ToString();

            smallScore.SetActive(false);
            theEndTitle.SetActive(true);
            playAgainButton.SetActive(true);
            homeButton.SetActive(true);
            scoreText.SetActive(true);
            scoreValue.SetActive(true);
            touchPad.SetActive(false);
        }

        if(isEnd && !isCoinsAdded)
        {
            isCoinsAdded = true;

            GameOver.SaveCoins();
        }

    }

    public static void Clear()
    {

        isEnd = false;
    }

    private void CheckSkin()
    {
        skins[EncryptedPlayerPrefs.GetInt("selectedCharacter")].SetActive(true);
    }

    private void Start()
    {

        SetStartPosition();

        rotateEnd = transform.rotation;

        p = new Parabola(height);

        audio = GetComponent<AudioSource>();

        audio.volume = PlayerPrefs.GetFloat("effectsSetting");

        playerPoints = 0;
        playerPointsSecure = 79;

        CheckSkin();

        backgroundSound.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("musicSetting");

        Application.targetFrameRate = PlayerPrefs.GetInt("refreshRateSetting");

        if(PlayerPrefs.GetInt("touchPad") == 1)
        {
            swipeManager.SetActive(false);
            touchPad.SetActive(true);
        } else
        {
            swipeManager.SetActive(true);
            touchPad.SetActive(false);
        }

        
    }

    private void Update()
    {
        CheckInputs();

        CheckIfEnd();

        CheckIfMoveAviable();

        transform.rotation = Quaternion.Lerp(transform.rotation, rotateEnd, 5f * Time.deltaTime);

    }
}
