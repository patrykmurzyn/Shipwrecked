<<<<<<< HEAD
using UnityEngine;

public class SkyRotation : MonoBehaviour
{
    [SerializeField]
    private  float speed;

    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", speed * Time.time);
    }
}
=======
using UnityEngine;

public class SkyRotation : MonoBehaviour
{
    [SerializeField]
    private  float speed;

    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", speed * Time.time);
    }
}
>>>>>>> ca61e779ba700dbc0df8a95b287f6ea4b74a5e89
