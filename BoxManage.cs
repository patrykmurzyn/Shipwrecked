<<<<<<< HEAD
using UnityEngine;

public class BoxManage : MonoBehaviour
{
    private float time = 0;
    private float requireTime = 3f;
    private float difficulty = 0.3f;
    
    public float GetDifficulty()
    {
        return difficulty;
    }

    private void CreateBoxesList()
    {
        int countPosition = 0;

        foreach (Transform i in transform)
        {
            Box temp = new Box();

            temp.SetPosition(countPosition);

            temp.SetState(3);

            temp.SetBoxObject(i.gameObject);
            

            if(i.gameObject.activeSelf)
            {
                Box.AddAvailableBoxesPosition(countPosition);

                temp.SetState(0);
            }

            Box.AddBoxes(temp);

            countPosition++;
        }
        
        int randMin = 0;
        int randMax = 3;

        for (int i = 0; i < Box.GetDestroyAmmount(); i++)
        {
            int rand = Random.Range(randMin, randMax);

            Box.AddDestroyedBoxesPosition(Box.GetAvailableBoxesPosition(rand));

            Box.GetBoxes(Box.GetDestroyedBoxesPosition(i)).SetState(3);

            Box.GetBoxes(Box.GetDestroyedBoxesPosition(i)).GetBoxObject().transform.position =
                new Vector3(Box.GetBoxes(Box.GetDestroyedBoxesPosition(i)).GetBoxObject().transform.position.x, 
                -.8f, 
                Box.GetBoxes(Box.GetDestroyedBoxesPosition(i)).GetBoxObject().transform.position.z);
            
            Box.GetBoxes(Box.GetDestroyedBoxesPosition(i)).GetBoxObject().GetComponent<FloatEffect>().enabled = false;
            
            randMin += 3;
            randMax += 3;

        }
        
    }

    private void SetDownUp()
    {

        if(time >= requireTime)
        {
            int temp;

                do
                {
                    temp = Random.Range(0, 420);
                } while (Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).GetState() != 0
                    || Enemy.IsEnemyOnPosition(Box.GetAvailableBoxesPosition(temp)));

            Box.AddGoDownList(Box.GetAvailableBoxesPosition(temp));

                Box.AddDestroyedBoxesPosition(Box.GetAvailableBoxesPosition(temp));

                Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).SetState(1);

                Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).GetBoxObject().GetComponent<FloatEffect>().enabled = false;

                do
                {
                    temp = Random.Range(0, 420);
                } while (Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).GetState() != 3);

                Box.AddGoUpList(Box.GetAvailableBoxesPosition(temp));

                Box.RemoveDestroyedBoxesPosition(Box.GetAvailableBoxesPosition(temp));

                Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).SetState(2);

            

            requireTime += difficulty;
        }

        for (int i = 0; i < Box.GetGoDownListSize(); i++)
        {
            GoDown(Box.GetBoxes(Box.GetGoDownList(i)), i);
        }

        for (int i = 0; i < Box.GetGoUpListSize(); i++)
        {
            GoUp(Box.GetBoxes(Box.GetGoUpList(i)), i);
        }
    }

    private void GoDown(Box box, int index)
    {
        if (box.GetState() == 1)
        {
            box.GetBoxObject().transform.position =
                Vector3.Lerp(box.GetBoxObject().transform.position,
                    new Vector3(box.GetBoxObject().transform.position.x, 
                    -.9f,
                    box.GetBoxObject().transform.position.z),
                0.3f * Time.deltaTime);

            if (box.GetBoxObject().transform.position.y <= -.8f)
            {

                Box.RemoveGoDownListAt(index);

                box.SetState(3);
            }
        }
    }

    private void GoUp(Box box, int index)
    {
        if (box.GetState() == 2)
        {
            box.GetBoxObject().transform.position =
                Vector3.Lerp(box.GetBoxObject().transform.position,
                    new Vector3(box.GetBoxObject().transform.position.x,
                    -0.2f,
                    box.GetBoxObject().transform.position.z),
                0.3f * Time.deltaTime);

            if (box.GetBoxObject().transform.position.y >= -0.3f)
            {

                Box.RemoveGoUpListAt(index);

                box.SetState(0);

                box.GetBoxObject().GetComponent<FloatEffect>().SetStartY(box.GetBoxObject().transform.position.y);
                box.GetBoxObject().GetComponent<FloatEffect>().enabled = true;

            }

        }
    }

    private void SetDifficulty()
    {
        if (time >= 20 && time < 40)
        {
            difficulty = 0.27f;
        }
        else if (time >= 40 && time < 60)
        {
            difficulty = 0.24f;
        }
        else if (time >= 60 && time < 80)
        {
            difficulty = 0.21f;
        }
        else if (time >= 80 && time < 100)
        {
            difficulty = 0.18f;
        }
        else if (time >= 100 && time < 120)
        {
            difficulty = 0.15f;
        }
        else if (time >= 120 && time < 140)
        {
            difficulty = 0.12f;
        }
        else if (time >= 140)
        {
            difficulty = 0.09f;
        }

        EnemyMovment.SetTimeToMove(6 * difficulty);
    }

    private void Awake()
    {
        Application.targetFrameRate = 120;

        CreateBoxesList();
    }

    private void Update()
    {
        time += Time.deltaTime;

        SetDownUp();

        SetDifficulty();

    }
}
=======
using UnityEngine;

public class BoxManage : MonoBehaviour
{
    private float time = 0;
    private float requireTime = 3f;
    private float difficulty = 0.3f;
    
    public float GetDifficulty()
    {
        return difficulty;
    }

    private void CreateBoxesList()
    {
        int countPosition = 0;

        foreach (Transform i in transform)
        {
            Box temp = new Box();

            temp.SetPosition(countPosition);

            temp.SetState(3);

            temp.SetBoxObject(i.gameObject);
            

            if(i.gameObject.activeSelf)
            {
                Box.AddAvailableBoxesPosition(countPosition);

                temp.SetState(0);
            }

            Box.AddBoxes(temp);

            countPosition++;
        }
        
        int randMin = 0;
        int randMax = 3;

        for (int i = 0; i < Box.GetDestroyAmmount(); i++)
        {
            int rand = Random.Range(randMin, randMax);

            Box.AddDestroyedBoxesPosition(Box.GetAvailableBoxesPosition(rand));

            Box.GetBoxes(Box.GetDestroyedBoxesPosition(i)).SetState(3);

            Box.GetBoxes(Box.GetDestroyedBoxesPosition(i)).GetBoxObject().transform.position =
                new Vector3(Box.GetBoxes(Box.GetDestroyedBoxesPosition(i)).GetBoxObject().transform.position.x, 
                -.8f, 
                Box.GetBoxes(Box.GetDestroyedBoxesPosition(i)).GetBoxObject().transform.position.z);
            
            Box.GetBoxes(Box.GetDestroyedBoxesPosition(i)).GetBoxObject().GetComponent<FloatEffect>().enabled = false;
            
            randMin += 3;
            randMax += 3;

        }
        
    }

    private void SetDownUp()
    {

        if(time >= requireTime)
        {
            int temp;

                do
                {
                    temp = Random.Range(0, 420);
                } while (Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).GetState() != 0
                    || Enemy.IsEnemyOnPosition(Box.GetAvailableBoxesPosition(temp)));

            Box.AddGoDownList(Box.GetAvailableBoxesPosition(temp));

                Box.AddDestroyedBoxesPosition(Box.GetAvailableBoxesPosition(temp));

                Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).SetState(1);

                Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).GetBoxObject().GetComponent<FloatEffect>().enabled = false;

                do
                {
                    temp = Random.Range(0, 420);
                } while (Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).GetState() != 3);

                Box.AddGoUpList(Box.GetAvailableBoxesPosition(temp));

                Box.RemoveDestroyedBoxesPosition(Box.GetAvailableBoxesPosition(temp));

                Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).SetState(2);

            

            requireTime += difficulty;
        }

        for (int i = 0; i < Box.GetGoDownListSize(); i++)
        {
            GoDown(Box.GetBoxes(Box.GetGoDownList(i)), i);
        }

        for (int i = 0; i < Box.GetGoUpListSize(); i++)
        {
            GoUp(Box.GetBoxes(Box.GetGoUpList(i)), i);
        }
    }

    private void GoDown(Box box, int index)
    {
        if (box.GetState() == 1)
        {
            box.GetBoxObject().transform.position =
                Vector3.Lerp(box.GetBoxObject().transform.position,
                    new Vector3(box.GetBoxObject().transform.position.x, 
                    -.9f,
                    box.GetBoxObject().transform.position.z),
                0.3f * Time.deltaTime);

            if (box.GetBoxObject().transform.position.y <= -.8f)
            {

                Box.RemoveGoDownListAt(index);

                box.SetState(3);
            }
        }
    }

    private void GoUp(Box box, int index)
    {
        if (box.GetState() == 2)
        {
            box.GetBoxObject().transform.position =
                Vector3.Lerp(box.GetBoxObject().transform.position,
                    new Vector3(box.GetBoxObject().transform.position.x,
                    -0.2f,
                    box.GetBoxObject().transform.position.z),
                0.3f * Time.deltaTime);

            if (box.GetBoxObject().transform.position.y >= -0.3f)
            {

                Box.RemoveGoUpListAt(index);

                box.SetState(0);

                box.GetBoxObject().GetComponent<FloatEffect>().SetStartY(box.GetBoxObject().transform.position.y);
                box.GetBoxObject().GetComponent<FloatEffect>().enabled = true;

            }

        }
    }

    private void SetDifficulty()
    {
        if (time >= 20 && time < 40)
        {
            difficulty = 0.27f;
        }
        else if (time >= 40 && time < 60)
        {
            difficulty = 0.24f;
        }
        else if (time >= 60 && time < 80)
        {
            difficulty = 0.21f;
        }
        else if (time >= 80 && time < 100)
        {
            difficulty = 0.18f;
        }
        else if (time >= 100 && time < 120)
        {
            difficulty = 0.15f;
        }
        else if (time >= 120 && time < 140)
        {
            difficulty = 0.12f;
        }
        else if (time >= 140)
        {
            difficulty = 0.09f;
        }

        EnemyMovment.SetTimeToMove(6 * difficulty);
    }

    private void Awake()
    {
        Application.targetFrameRate = 120;

        CreateBoxesList();
    }

    private void Update()
    {
        time += Time.deltaTime;

        SetDownUp();

        SetDifficulty();

    }
}
>>>>>>> ca61e779ba700dbc0df8a95b287f6ea4b74a5e89
