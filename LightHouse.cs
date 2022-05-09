using UnityEngine;

public class LightHouse : MonoBehaviour
{
    [SerializeField]
    private float rotSpeed = 10f;

    void Update()
    {
        transform.Rotate(0, rotSpeed * Time.deltaTime, 0, Space.World);
    }
}
