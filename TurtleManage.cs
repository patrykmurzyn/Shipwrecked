using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle
{
    public int position;
    public int state; //0 - good, 1 - go down, 2 - go up, 3 - down
    public GameObject turtleObject;
    public static List<Turtle> turtles = new List<Turtle>();
    public static List<int> goDownList = new List<int>();
    public static List<int> goUpList = new List<int>();

    public static int GetIndex(int position)
    {
        for (int i = 0; i < 40; i++)
        {
            if (turtles[i].position == position) return i;
        }

        return -1;
    }
}


public class TurtleManage : MonoBehaviour
{

    void CreateTurtleList()
    {
        foreach (Transform i in transform)
        {
            Turtle temp = new Turtle
            {
                turtleObject = i.gameObject,

                state = 0,
            };

            Turtle.turtles.Add(temp);
        }
    }
     
    void SetTurtles()
    {
        for(int i = 0; i < 40; i++)
        {
            int temp;

            do
            {
                temp = Random.Range(0, 420);

            } while (Box.boxes[Box.availableBoxesPosition[temp]].state != 3);

            Box.boxes[Box.availableBoxesPosition[temp]].state = 4;

            Box.boxes[Box.availableBoxesPosition[temp]].boxObject.transform.position
                = new Vector3(Box.boxes[Box.availableBoxesPosition[temp]].boxObject.transform.position.x, -.8f, Box.boxes[Box.availableBoxesPosition[temp]].boxObject.transform.position.z);

            Turtle.turtles[i].turtleObject.transform.position
                = new Vector3(Box.boxes[Box.availableBoxesPosition[temp]].boxObject.transform.position.x, -.2f, Box.boxes[Box.availableBoxesPosition[temp]].boxObject.transform.position.z);

            Turtle.turtles[i].position = Box.availableBoxesPosition[temp];

            Turtle.turtles[i].turtleObject.GetComponent<FloatEffect>().SetStartY(-.2f);


        }
    }

    void GoDown()
    {
        for (int i = 0; i < Turtle.goDownList.Count; i++)
        {

            Turtle.turtles[Turtle.goDownList[i]].turtleObject.transform.position =
                Vector3.Lerp(Turtle.turtles[Turtle.goDownList[i]].turtleObject.transform.position,
                new Vector3(Turtle.turtles[Turtle.goDownList[i]].turtleObject.transform.position.x, -2f, Turtle.turtles[Turtle.goDownList[i]].turtleObject.transform.position.z),
                0.3f * Time.deltaTime);

            if(Turtle.turtles[Turtle.goDownList[i]].turtleObject.transform.position.y < -.8f)
            {

                Turtle.goUpList.Add(Turtle.goDownList[i]);

                int temp;

                do
                {
                    temp = Random.Range(0, 420);
                
                } while (Box.boxes[Box.availableBoxesPosition[temp]].state != 3);

                Turtle.turtles[Turtle.goDownList[i]].turtleObject.transform.position = new Vector3(Box.boxes[Box.availableBoxesPosition[temp]].boxObject.transform.position.x, -1f, Box.boxes[Box.availableBoxesPosition[temp]].boxObject.transform.position.z);
                
                Box.boxes[Box.availableBoxesPosition[temp]].state = 4;

                Box.boxes[Turtle.turtles[Turtle.goDownList[i]].position].state = 3;

                Turtle.turtles[Turtle.goDownList[i]].position = Box.availableBoxesPosition[temp];

                Turtle.goDownList.RemoveAt(i);
            }
        }
    }

    void GoUp()
    {
        for (int i = 0; i < Turtle.goUpList.Count; i++)
        {
            Turtle.turtles[Turtle.goUpList[i]].turtleObject.transform.position =
                Vector3.Lerp(Turtle.turtles[Turtle.goUpList[i]].turtleObject.transform.position,
                new Vector3(Turtle.turtles[Turtle.goUpList[i]].turtleObject.transform.position.x, 1f, Turtle.turtles[Turtle.goUpList[i]].turtleObject.transform.position.z),
                0.3f * Time.deltaTime);

            if(Turtle.turtles[Turtle.goUpList[i]].turtleObject.transform.position.y >= -.2f)
            {
                Turtle.turtles[Turtle.goUpList[i]].turtleObject.GetComponent<FloatEffect>().enabled = true;

                Turtle.goUpList.RemoveAt(i);
            }
        }

    }

    private void Start()
    {
        CreateTurtleList();

        SetTurtles();
    }

    private void FixedUpdate()
    {
        GoDown();

        GoUp();
    }
}
