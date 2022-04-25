<<<<<<< HEAD
using System.Collections.Generic;
using UnityEngine;

public class Turtle
{
    private int position;
    private int state; //0 - good, 1 - go down, 2 - go up, 3 - down
    private GameObject turtleObject;
    private static List<Turtle> turtles = new List<Turtle>();
    private static List<int> goDownList = new List<int>();
    private static List<int> goUpList = new List<int>();

    public int GetPosition()
    {
        return position;
    }

    public void SetPosition(int position)
    {
        this.position = position;
    }

    public int GetState()
    {
        return state;
    }

    public void SetState(int state)
    {
        this.state = state;
    }

    public GameObject GetTurtleObject()
    {
        return turtleObject;
    }

    public void SetTurtleObject(GameObject turtleObject)
    {
        this.turtleObject = turtleObject;
    }

    public static Turtle GetTurtles(int index)
    {
        return turtles[index];
    }

    public static void AddTurtles(Turtle turtle)
    {
        turtles.Add(turtle);
    }

    public static int GetGoDownList(int index)
    {
        return goDownList[index];
    }

    public static int GetGoDownListSize()
    {
        return (goDownList.Count);
    }

    public static void AddGoDownList(int position)
    {
        goDownList.Add(position);
    }

    public static void RemoveGoDownListAt(int index)
    {
        goDownList.RemoveAt(index);
    }

    public static int GetGoUpList(int index)
    {
        return goUpList[index];
    }

    public static int GetGoUpListSize()
    {
        return (goUpList.Count);
    }

    public static void AddGoUpList(int position)
    {
        goUpList.Add(position);
    }

    public static void RemoveGoUpListAt(int index)
    {
        goUpList.RemoveAt(index);
    }

    public static int SearchIndexByPosition(int position)
    {
        for (int i = 0; i < 40; i++)
        {
            if (turtles[i].position == position) return i;
        }

        return -1;
    }

    public static void ClearLists()
    {
        turtles.Clear();
        goDownList.Clear();
        goUpList.Clear();
    }
=======
using System.Collections.Generic;
using UnityEngine;

public class Turtle
{
    private int position;
    private int state; //0 - good, 1 - go down, 2 - go up, 3 - down
    private GameObject turtleObject;
    private static List<Turtle> turtles = new List<Turtle>();
    private static List<int> goDownList = new List<int>();
    private static List<int> goUpList = new List<int>();

    public int GetPosition()
    {
        return position;
    }

    public void SetPosition(int position)
    {
        this.position = position;
    }

    public int GetState()
    {
        return state;
    }

    public void SetState(int state)
    {
        this.state = state;
    }

    public GameObject GetTurtleObject()
    {
        return turtleObject;
    }

    public void SetTurtleObject(GameObject turtleObject)
    {
        this.turtleObject = turtleObject;
    }

    public static Turtle GetTurtles(int index)
    {
        return turtles[index];
    }

    public static void AddTurtles(Turtle turtle)
    {
        turtles.Add(turtle);
    }

    public static int GetGoDownList(int index)
    {
        return goDownList[index];
    }

    public static int GetGoDownListSize()
    {
        return (goDownList.Count);
    }

    public static void AddGoDownList(int position)
    {
        goDownList.Add(position);
    }

    public static void RemoveGoDownListAt(int index)
    {
        goDownList.RemoveAt(index);
    }

    public static int GetGoUpList(int index)
    {
        return goUpList[index];
    }

    public static int GetGoUpListSize()
    {
        return (goUpList.Count);
    }

    public static void AddGoUpList(int position)
    {
        goUpList.Add(position);
    }

    public static void RemoveGoUpListAt(int index)
    {
        goUpList.RemoveAt(index);
    }

    public static int SearchIndexByPosition(int position)
    {
        for (int i = 0; i < 40; i++)
        {
            if (turtles[i].position == position) return i;
        }

        return -1;
    }

    public static void ClearLists()
    {
        turtles.Clear();
        goDownList.Clear();
        goUpList.Clear();
    }
>>>>>>> ca61e779ba700dbc0df8a95b287f6ea4b74a5e89
}