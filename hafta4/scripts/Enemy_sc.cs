using UnityEngine;

public class Enemy_sc : MonoBehaviour
{
    [SerializeField]
    int speed = 1;

    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        
        if (transform.position.y < -5.5f)
        {
            Destroy(this.gameObject);
   
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //TODO :Player 'ın canını bir eksilt
            Player_sc player_sc = other.transform.GetComponent<Player_sc>();
            player_sc.Damage();

            Destroy(this.gameObject);
        }
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
    
    

}
