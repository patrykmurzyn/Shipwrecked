using System.Collections.Generic;
using UnityEngine;

public class Box
{
    private int position;
    private int state; // 0 - good, 1 - go down, 2 - go up, 3 - down, 4 - turtle, 5 - enemie
    private GameObject boxObject;

    private static List<Box> boxes = new List<Box>();
    private static List<int> availableBoxesPosition = new List<int>();
    private static List<int> destroyedBoxesPosition = new List<int>();
    private static List<int> goDownList = new List<int>();
    private static List<int> goUpList = new List<int>();

    private static int destroyAmmount = 140;

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

    public GameObject GetBoxObject()
    {
        return boxObject;
    }

    public void SetBoxObject(GameObject boxObject)
    {
        this.boxObject = boxObject;
    }

    public static int GetDestroyAmmount()
    {
        return destroyAmmount;
    }

    public static void AddBoxes(Box box)
    {
        boxes.Add(box);
    }

    public static Box GetBoxes(int index)
    {
        return boxes[index];
    }

    public static void AddAvailableBoxesPosition(int position)
    {
        availableBoxesPosition.Add(position);
    }

    public static int GetAvailableBoxesPosition(int index)
    {
        return availableBoxesPosition[index];
    }

    public static void AddDestroyedBoxesPosition(int position)
    {
        destroyedBoxesPosition.Add(position);
    }

    public static void RemoveDestroyedBoxesPosition(int position)
    {
        destroyedBoxesPosition.Remove(position);
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

    public static int GetDestroyedBoxesPosition(int index)
    {
        return destroyedBoxesPosition[index];
    }

}