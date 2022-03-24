using UnityEngine;

public class LightHouse : MonoBehaviour
{
    [SerializeField]
    private float rotSpeed = 10f;

    void FixedUpdate()
    {
        transform.Rotate(0, rotSpeed * Time.fixedDeltaTime, 0, Space.World);
    }
}
