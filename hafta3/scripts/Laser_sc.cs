using UnityEngine;

public class Laser_sc : MonoBehaviour
{
    public int speed = 3;

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);

        if(this.transform.position.y> 7)
        {
            Destroy(gameObject);
        }
    }
}
