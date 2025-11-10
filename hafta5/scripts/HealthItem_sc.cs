using UnityEngine;

public class HealthItem_sc : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f; 

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

      
        if (transform.position.y < -5.5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player_sc player = other.GetComponent<Player_sc>();
            if (player != null)
            {
                player.AddHealth(1); 
            }
            Destroy(this.gameObject);
        }
    }
}
