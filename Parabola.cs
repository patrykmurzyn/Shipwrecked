using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parabola
{
    readonly float heigh;


    public Parabola(float heigh)
    {
        this.heigh = heigh;
    }

    public void Move(Transform target, Vector3 a, Vector3 b, float time)
    {

        float lenght = Mathf.Abs(a.x - b.x);
        float distance;


        if (target.GetComponent<PlayerManage>().GetDirection() == 0 || target.GetComponent<PlayerManage>().GetDirection() == 2)
        {
            lenght = Mathf.Abs(a.x - b.x);
            distance = Mathf.Abs(target.transform.position.x - b.x);
        }
        else
        {
            lenght = Mathf.Abs(a.z - b.z);
            distance = Mathf.Abs(target.transform.position.z - b.z);
        }

        if (!target.GetComponent<PlayerManage>().GetIsFalling() && distance < 0.7f * lenght)
        {
            target.GetComponent<PlayerManage>().SetDuration(0.0001f);
            target.GetComponent<PlayerManage>().SetIsFalling(true);
        }

        float target_X = a.x + (b.x - a.x) * time;
        float target_Y = a.y + ((b.y - a.y)) * time + 0.6f * (1 - (Mathf.Abs(0.5f - time) / 0.5f) * (Mathf.Abs(0.5f - time) / 0.5f));
        float target_Z = a.z + (b.z - a.z) * time;

        target.position = Vector3.Lerp(target.position, new Vector3(target_X, target_Y, target_Z), 1f);
    }
}
