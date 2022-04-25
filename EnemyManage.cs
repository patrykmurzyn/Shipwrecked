<<<<<<< HEAD
using UnityEngine;

public class EnemyManage : MonoBehaviour
{
    void CreateEnemyList()
    {
        foreach (Transform i in transform)
        {
            Enemy temp = new Enemy();

            temp.SetEnemyObject(i.gameObject);

            Enemy.AddEnemies(temp);
        }
    }

    void SetEnemies()
    {
        for(int i = 0; i < 4; i++)
        {

            int temp;

            do
            {
                temp = Random.Range(0, 420);

            } while (Box.GetBoxes(temp).GetState() != 0 ||
                PlayerManage.CheckDistanceToPlayer(temp) >= 12);

            Enemy.GetEnemies(i).GetEnemyObject().transform.position = Box.GetBoxes(temp).GetBoxObject().transform.position
            + new Vector3(0, 0.6f, 0.05f);

            Enemy.GetEnemies(i).SetPosition(Box.GetBoxes(temp).GetPosition());

            Enemy.GetEnemies(i).SetDirection(Random.Range(0, 4));

            Box.GetBoxes(temp).SetState(5);

            if (Enemy.GetEnemies(i).GetDirection() == 0)
            {
                Enemy.GetEnemies(i).GetEnemyObject().transform.eulerAngles = new Vector3(0, 90, 0);
            }
            else if (Enemy.GetEnemies(i).GetDirection() == 1)
            {
                Enemy.GetEnemies(i).GetEnemyObject().transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (Enemy.GetEnemies(i).GetDirection() == 2)
            {
                Enemy.GetEnemies(i).GetEnemyObject().transform.eulerAngles = new Vector3(0, 270, 0);
            }
            else if (Enemy.GetEnemies(i).GetDirection() == 3)
            {
                Enemy.GetEnemies(i).GetEnemyObject().transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    private void Awake()
    {
        CreateEnemyList();

        SetEnemies();
    }
}
=======
using UnityEngine;

public class EnemyManage : MonoBehaviour
{
    void CreateEnemyList()
    {
        foreach (Transform i in transform)
        {
            Enemy temp = new Enemy();

            temp.SetEnemyObject(i.gameObject);

            Enemy.AddEnemies(temp);
        }
    }

    void SetEnemies()
    {
        for(int i = 0; i < 4; i++)
        {

            int temp;

            do
            {
                temp = Random.Range(0, 420);

            } while (Box.GetBoxes(temp).GetState() != 0 ||
                PlayerManage.CheckDistanceToPlayer(temp) >= 12);

            Enemy.GetEnemies(i).GetEnemyObject().transform.position = Box.GetBoxes(temp).GetBoxObject().transform.position
            + new Vector3(0, 0.6f, 0.05f);

            Enemy.GetEnemies(i).SetPosition(Box.GetBoxes(temp).GetPosition());

            Enemy.GetEnemies(i).SetDirection(Random.Range(0, 4));

            Box.GetBoxes(temp).SetState(5);

            if (Enemy.GetEnemies(i).GetDirection() == 0)
            {
                Enemy.GetEnemies(i).GetEnemyObject().transform.eulerAngles = new Vector3(0, 90, 0);
            }
            else if (Enemy.GetEnemies(i).GetDirection() == 1)
            {
                Enemy.GetEnemies(i).GetEnemyObject().transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else if (Enemy.GetEnemies(i).GetDirection() == 2)
            {
                Enemy.GetEnemies(i).GetEnemyObject().transform.eulerAngles = new Vector3(0, 270, 0);
            }
            else if (Enemy.GetEnemies(i).GetDirection() == 3)
            {
                Enemy.GetEnemies(i).GetEnemyObject().transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    private void Awake()
    {
        CreateEnemyList();

        SetEnemies();
    }
}
>>>>>>> ca61e779ba700dbc0df8a95b287f6ea4b74a5e89
