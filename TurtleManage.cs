using UnityEngine;

public class TurtleManage : MonoBehaviour
{

    private void CreateTurtleList()
    {
        foreach (Transform i in transform)
        {
            Turtle temp = new Turtle();

            temp.SetTurtleObject(i.gameObject);

            temp.SetState(0);

            Turtle.AddTurtles(temp);
        }
    }
     
    private void SetTurtles()
    {
        for(int i = 0; i < 40; i++)
        {
            int temp;

            do
            {
                temp = Random.Range(0, 420);

            } while (Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).GetState() != 3);

            Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).SetState(4);

            Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).GetBoxObject().transform.position
                = new Vector3(Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).GetBoxObject().transform.position.x,
                -.8f,
                Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).GetBoxObject().transform.position.z);

            Turtle.GetTurtles(i).GetTurtleObject().transform.position
                = new Vector3(Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).GetBoxObject().transform.position.x,
                -.2f,
                Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).GetBoxObject().transform.position.z);

            Turtle.GetTurtles(i).SetPosition(Box.GetAvailableBoxesPosition(temp));

            Turtle.GetTurtles(i).GetTurtleObject().GetComponent<FloatEffect>().SetStartY(-.2f);


        }
    }

    private void GoDown()
    {
        for (int i = 0; i < Turtle.GetGoDownListSize(); i++)
        {
            Turtle.GetTurtles(Turtle.GetGoDownList(i)).GetTurtleObject().transform.position =
                Vector3.Lerp(Turtle.GetTurtles(Turtle.GetGoDownList(i)).GetTurtleObject().transform.position,
                    new Vector3(Turtle.GetTurtles(Turtle.GetGoDownList(i)).GetTurtleObject().transform.position.x,
                    -2f,
                    Turtle.GetTurtles(Turtle.GetGoDownList(i)).GetTurtleObject().transform.position.z),
                    0.3f * Time.deltaTime);

            if(Turtle.GetTurtles(Turtle.GetGoDownList(i)).GetTurtleObject().transform.position.y < -.8f)
            {
                Turtle.AddGoUpList(Turtle.GetGoDownList(i));

                int temp;

                do
                {
                    temp = Random.Range(0, 420);
                
                } while (Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).GetState() != 3);

                Turtle.GetTurtles(Turtle.GetGoDownList(i)).GetTurtleObject().transform.position = 
                    new Vector3(Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).GetBoxObject().transform.position.x,
                    -1f,
                    Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).GetBoxObject().transform.position.z);
                
                Box.GetBoxes(Box.GetAvailableBoxesPosition(temp)).SetState(4);

                Box.GetBoxes(Turtle.GetTurtles(Turtle.GetGoDownList(i)).GetPosition()).SetState(3);

                Turtle.GetTurtles(Turtle.GetGoDownList(i)).SetPosition(Box.GetAvailableBoxesPosition(temp));

                Turtle.RemoveGoDownListAt(i);


                if (Box.GetBoxes(PlayerManage.GetPlayerPosition()).GetState() == 3)
                {
                    PlayerManage.SetIsEnd(true);
                }

            }
        }
    }

    private void GoUp()
    {
        for (int i = 0; i < Turtle.GetGoUpListSize(); i++)
        {
            Turtle.GetTurtles(Turtle.GetGoUpList(i)).GetTurtleObject().transform.position =
                Vector3.Lerp(Turtle.GetTurtles(Turtle.GetGoUpList(i)).GetTurtleObject().transform.position,
                new Vector3(Turtle.GetTurtles(Turtle.GetGoUpList(i)).GetTurtleObject().transform.position.x, 
                1f, 
                Turtle.GetTurtles(Turtle.GetGoUpList(i)).GetTurtleObject().transform.position.z),
                0.3f * Time.deltaTime);

            if(Turtle.GetTurtles(Turtle.GetGoUpList(i)).GetTurtleObject().transform.position.y >= -.2f)
            {
                Turtle.GetTurtles(Turtle.GetGoUpList(i)).GetTurtleObject().GetComponent<FloatEffect>().enabled = true;

                Turtle.GetTurtles(Turtle.GetGoUpList(i)).SetState(0);

                Turtle.RemoveGoUpListAt(i);
            }
        }

    }

    private void Start()
    {
        CreateTurtleList();

        SetTurtles();
    }

    private void Update()
    {
        GoDown();

        GoUp();
    }
}
