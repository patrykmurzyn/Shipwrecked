using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private float minimumDistance = .2f;
    [SerializeField]
    private float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)]
    private float directionTreshHold = .9f;


    private InputManager inputmanager;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    private void Awake()
    {
        inputmanager = InputManager.Instance;
    }

    private void OnEnable()
    {
        inputmanager.OnStartTouch += SwipeStart;
        inputmanager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputmanager.OnStartTouch -= SwipeStart;
        inputmanager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector2.Distance(startPosition, endPosition) >= minimumDistance
            && (endTime - startTime) <= maximumTime)
        {
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2d = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2d);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionTreshHold)
        {
            Player.GetComponent<PlayerManage>().GoUP();
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionTreshHold)
        {
            Player.GetComponent<PlayerManage>().GoLeft();
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionTreshHold)
        {
            Player.GetComponent<PlayerManage>().GoRight();
        }
    }

}