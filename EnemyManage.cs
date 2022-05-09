using UnityEngine;

[DefaultExecutionOrder(1)]
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
                EnemyMovment.CheckDistanceToPlayer(temp) <= 12);

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

    private void Start()
    {
        CreateEnemyList();

        SetEnemies();
    }
}
