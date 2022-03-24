using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box
{
    public int position;
    public int state; // 0 - good, 1 - go down, 2 - go up, 3 - down, 4 - turtle, 5 - enemie
    public GameObject boxObject;

    public static List<Box> boxes = new List<Box>();
    public static List<int> availableBoxesPosition = new List<int>();
    public static List<int> destroyedBoxesPosition = new List<int>();

    public static int destroyAmmount = 140;

}

public class BoxManage : MonoBehaviour
{
    float time = 0;
    float requireTime = 3f;
    float difficulty = 0.3f;

    List<int> goDownList = new List<int>();
    List<int> goUpList = new List<int>();

    void CreateBoxesList()
    {
        int countPosition = 0;

        foreach (Transform i in transform)
        {
            Box temp = new Box()
            {
                position = countPosition,

                state = 3,

                boxObject = i.gameObject
            };

            if(i.gameObject.activeSelf)
            {
                Box.availableBoxesPosition.Add(countPosition);

                temp.state = 0;
            }

            Box.boxes.Add(temp);

            countPosition++;
        }

        int randMin = 0;
        int randMax = 3;

        for (int i = 0; i < Box.destroyAmmount; i++)
        {
            int rand = Random.Range(randMin, randMax);

            Box.destroyedBoxesPosition.Add(Box.availableBoxesPosition[rand]);

            Box.boxes[Box.destroyedBoxesPosition[i]].state = 3;

            Box.boxes[Box.destroyedBoxesPosition[i]].boxObject.transform.position =
                new Vector3(Box.boxes[Box.destroyedBoxesPosition[i]].boxObject.transform.position.x, -.8f, Box.boxes[Box.destroyedBoxesPosition[i]].boxObject.transform.position.z);
            
            Box.boxes[Box.destroyedBoxesPosition[i]].boxObject.GetComponent<FloatEffect>().enabled = false;
            
            randMin += 3;
            randMax += 3;

        }
    }

    void SetDownUp()
    {

        if(time >= requireTime)
        {
            int temp;

                do
                {
                    temp = Random.Range(0, 420);
                } while (Box.boxes[Box.availableBoxesPosition[temp]].state != 0);

                goDownList.Add(Box.availableBoxesPosition[temp]);

                Box.destroyedBoxesPosition.Add(Box.availableBoxesPosition[temp]);

                Box.boxes[Box.availableBoxesPosition[temp]].state = 1;

                Box.boxes[Box.availableBoxesPosition[temp]].boxObject.GetComponent<FloatEffect>().enabled = false;

                do
                {
                    temp = Random.Range(0, 420);
                } while (Box.boxes[Box.availableBoxesPosition[temp]].state != 3);

                goUpList.Add(Box.availableBoxesPosition[temp]);

                Box.destroyedBoxesPosition.Remove(Box.availableBoxesPosition[temp]);

                Box.boxes[Box.availableBoxesPosition[temp]].state = 2;

            

            requireTime += difficulty;
        }

        for (int i = 0; i < goDownList.Count; i++)
        {
            GoDown(Box.boxes[goDownList[i]], i);
        }

        for (int i = 0; i < goUpList.Count; i++)
        {
            GoUp(Box.boxes[goUpList[i]], i);
        }
    }

    void GoDown(Box box, int index)
    {
        if (box.state == 1)
        {
            box.boxObject.transform.position =
            Vector3.Lerp(box.boxObject.transform.position,
            new Vector3(box.boxObject.transform.position.x, -.9f, box.boxObject.transform.position.z),
            0.3f * Time.fixedDeltaTime);

            if (box.boxObject.transform.position.y <= -.8f)
            {

                goDownList.RemoveAt(index);

                

                box.state = 3;
            }
        }
    }

    void GoUp(Box box, int index)
    {
        if (box.state == 2)
        {
            box.boxObject.transform.position =
            Vector3.Lerp(box.boxObject.transform.position,
            new Vector3(box.boxObject.transform.position.x, -0.2f, box.boxObject.transform.position.z),
            0.3f * Time.fixedDeltaTime);

            if (box.boxObject.transform.position.y >= -0.3f)
            {

                goUpList.RemoveAt(index);

                box.state = 0;

                box.boxObject.GetComponent<FloatEffect>().SetStartY(box.boxObject.transform.position.y);
                box.boxObject.GetComponent<FloatEffect>().enabled = true;

            }

        }
    }

    void SetDifficulty()
    {
        if (time >= 20 && time < 40) difficulty = 0.27f;

        else if (time >= 40 && time < 60) difficulty = 0.24f;

        else if(time >= 60 && time < 80) difficulty = 0.21f;

        else if (time >= 80 && time < 100) difficulty = 0.18f;

        else if (time >= 100 && time < 120) difficulty = 0.15f;

        else if (time >= 120 && time < 140) difficulty = 0.12f;

        else if (time >= 140) difficulty = 0.09f; 

    }

    void Awake()
    {
        Application.targetFrameRate = 120;

        CreateBoxesList();
    }

    private void Update()
    {

        SetDifficulty();

    }

    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;

        SetDownUp();

    }
}
