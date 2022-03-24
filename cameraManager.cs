using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float positionSpeed;

    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private Vector3 offset0;

    [SerializeField]
    private Quaternion rot0;

    [SerializeField]
    private Vector3 offset1;

    [SerializeField]
    private Quaternion rot1;

    [SerializeField]
    private Vector3 offset2;

    [SerializeField]
    private Quaternion rot2;

    [SerializeField]
    private Vector3 offset3;

    [SerializeField]
    private Quaternion rot3;

    private Vector3 endPos;
    private Quaternion rotation;

    void Update()
    {
        if (Player.direction == 0)
        {
            endPos = new Vector3(target.position.x, 1.5f, target.position.z) + offset0;
            rotation = rot0;
        }

        if (Player.direction == 1)
        {
            endPos = new Vector3(target.position.x, 1.5f, target.position.z) + offset1;
            rotation = rot1;
        }

        if (Player.direction == 2)
        {
            endPos = new Vector3(target.position.x, 1.5f, target.position.z) + offset2;
            rotation = rot2;
        }

        if (Player.direction == 3)
        {
            endPos = new Vector3(target.position.x, 1.5f, target.position.z) + offset3;
            rotation = rot3;
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, endPos, positionSpeed * Time.fixedDeltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);
    }
}
