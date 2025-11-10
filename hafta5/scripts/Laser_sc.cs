
using UnityEngine;

public class Laser_sc : MonoBehaviour
{
    [SerializeField] private float speed = 8f;

    void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime);

        if (transform.position.y > 7f)
            Destroy(gameObject);
    }
}
