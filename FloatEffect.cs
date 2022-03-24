using UnityEngine;

public class FloatEffect : MonoBehaviour
{

    [SerializeField] 
    private float strenght;

    private float startY;

    private int mode;

    void Start()
    {

        startY = this.transform.position.y;

        mode = Random.Range(0, 3);

    }

    void FixedUpdate()
    {
            if (mode == 0)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,
                startY + ((float)Mathf.Sin(Time.time) * strenght),
                transform.position.z), 0.3f * Time.deltaTime);

                //transform.position = new Vector3(transform.position.x,
                //startY + ((float)Mathf.Sin(Time.time) * strenght),
                //transform.position.z);
            }
            else if (mode == 1)
            {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,
            startY - ((float)Mathf.Sin(Time.time) * strenght),
            transform.position.z), 0.3f * Time.deltaTime);

            //transform.position = new Vector3(transform.position.x,
            //    startY - ((float)Mathf.Sin(Time.time) * strenght),
            //    transform.position.z);
            }
            else if (mode == 2)
            {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x,
            startY - ((float)Mathf.Cos(Time.time) * strenght),
            transform.position.z), 0.3f * Time.deltaTime);

            //transform.position = new Vector3(transform.position.x,
            //    startY - ((float)Mathf.Cos(Time.time) * strenght),
            //    transform.position.z);
            }
    }

    public void SetStartY(float y)
    {
        startY = y;
    }
}
