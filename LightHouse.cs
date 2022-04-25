<<<<<<< HEAD
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
=======
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
>>>>>>> ca61e779ba700dbc0df8a95b287f6ea4b74a5e89
