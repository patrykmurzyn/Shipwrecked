using UnityEngine;

public class SkyRotation : MonoBehaviour
{
    [SerializeField]
    private  float speed;

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", speed * Time.time);
    }
}
