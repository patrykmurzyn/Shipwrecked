<<<<<<< HEAD
using UnityEngine;

public class ParabolaEnemy
{
    private readonly float heigh;


    public ParabolaEnemy(float heigh)
    {
        this.heigh = heigh;
    }

    public void Move(Transform target, Vector3 a, Vector3 b, float time, int index)
    {

        float lenght = Mathf.Abs(a.x - b.x);
        float distance;


        if (Enemy.GetEnemies(index).GetDirection() == 0 || Enemy.GetEnemies(index).GetDirection() == 2)
        {
            lenght = Mathf.Abs(a.x - b.x);
            distance = Mathf.Abs(target.transform.position.x - b.x);
        }
        else
        {
            lenght = Mathf.Abs(a.z - b.z);
            distance = Mathf.Abs(target.transform.position.z - b.z);
        }

        if (!target.GetComponent<EnemyMovment>().GetIsFalling() && distance < 0.7f * lenght)
        {
            target.GetComponent<EnemyMovment>().SetDuration(0.0001f);
            target.GetComponent<EnemyMovment>().SetIsFalling(true);
        }

        float target_X = a.x + (b.x - a.x) * time;
        float target_Y = a.y + ((b.y - a.y)) * time + 0.6f * (1 - (Mathf.Abs(0.5f - time) / 0.5f) * (Mathf.Abs(0.5f - time) / 0.5f));
        float target_Z = a.z + (b.z - a.z) * time;

        target.position = Vector3.Lerp(target.position, new Vector3(target_X, target_Y, target_Z), 1f);
    }
=======
using UnityEngine;

public class ParabolaEnemy
{
    private readonly float heigh;


    public ParabolaEnemy(float heigh)
    {
        this.heigh = heigh;
    }

    public void Move(Transform target, Vector3 a, Vector3 b, float time, int index)
    {

        float lenght = Mathf.Abs(a.x - b.x);
        float distance;


        if (Enemy.GetEnemies(index).GetDirection() == 0 || Enemy.GetEnemies(index).GetDirection() == 2)
        {
            lenght = Mathf.Abs(a.x - b.x);
            distance = Mathf.Abs(target.transform.position.x - b.x);
        }
        else
        {
            lenght = Mathf.Abs(a.z - b.z);
            distance = Mathf.Abs(target.transform.position.z - b.z);
        }

        if (!target.GetComponent<EnemyMovment>().GetIsFalling() && distance < 0.7f * lenght)
        {
            target.GetComponent<EnemyMovment>().SetDuration(0.0001f);
            target.GetComponent<EnemyMovment>().SetIsFalling(true);
        }

        float target_X = a.x + (b.x - a.x) * time;
        float target_Y = a.y + ((b.y - a.y)) * time + 0.6f * (1 - (Mathf.Abs(0.5f - time) / 0.5f) * (Mathf.Abs(0.5f - time) / 0.5f));
        float target_Z = a.z + (b.z - a.z) * time;

        target.position = Vector3.Lerp(target.position, new Vector3(target_X, target_Y, target_Z), 1f);
    }
>>>>>>> ca61e779ba700dbc0df8a95b287f6ea4b74a5e89
}