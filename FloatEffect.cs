<<<<<<< HEAD
using UnityEngine;

public class FloatEffect : MonoBehaviour
{

    [SerializeField] 
    private float strenght;

    private float startY;

    private int mode;

    private void Start()
    {

        startY = this.transform.position.y;

        mode = Random.Range(0, 3);

    }

    private void Update()
    {
            if (mode == 0)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,
                startY + ((float)Mathf.Sin(Time.time) * strenght),
                transform.position.z), 0.3f * Time.deltaTime);
            }
            else if (mode == 1)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,
                startY - ((float)Mathf.Sin(Time.time) * strenght),
                transform.position.z), 0.3f * Time.deltaTime);
            }
            else if (mode == 2)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,
                startY - ((float)Mathf.Cos(Time.time) * strenght),
                transform.position.z), 0.3f * Time.deltaTime);

            }
    }

    public void SetStartY(float y)
    {
        startY = y;
    }
}
=======
using UnityEngine;

public class FloatEffect : MonoBehaviour
{

    [SerializeField] 
    private float strenght;

    private float startY;

    private int mode;

    private void Start()
    {

        startY = this.transform.position.y;

        mode = Random.Range(0, 3);

    }

    private void Update()
    {
            if (mode == 0)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,
                startY + ((float)Mathf.Sin(Time.time) * strenght),
                transform.position.z), 0.3f * Time.deltaTime);
            }
            else if (mode == 1)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,
                startY - ((float)Mathf.Sin(Time.time) * strenght),
                transform.position.z), 0.3f * Time.deltaTime);
            }
            else if (mode == 2)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,
                startY - ((float)Mathf.Cos(Time.time) * strenght),
                transform.position.z), 0.3f * Time.deltaTime);

            }
    }

    public void SetStartY(float y)
    {
        startY = y;
    }
}
>>>>>>> ca61e779ba700dbc0df8a95b287f6ea4b74a5e89
