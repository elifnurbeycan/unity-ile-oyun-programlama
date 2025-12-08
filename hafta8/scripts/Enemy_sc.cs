using UnityEngine;

public class Enemy_sc : MonoBehaviour
{
    [SerializeField]
    int speed = 4;

    Player_sc player_sc;


    Animator animator;

    void Start()
    {
        player_sc = GameObject.Find("Player").GetComponent<Player_sc>();

        animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (this.transform.position.y < -5.5f)
        {
            this.transform.position = new Vector3(Random.Range(-9.5f, 9.5f),
                                                    7.4f,
                                                    0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Çarpışma: " + other.tag);

        if (other.tag == "Player")
        {
            //Player'ın canını bir eksilt
            //Player_sc player_sc = other.transform.GetComponent<Player_sc>();

            if (player_sc != null)
            {
                player_sc.Damage();
            }
            //patlama animasyonunu göster
            animator.SetTrigger("OnEnemyDeath");
            //hızı sıfırla
            speed = 0;
            //kendini yok et
            Destroy(this.gameObject, 2.3f);
        }
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);

            if (player_sc != null)
            {
                player_sc.AddScore(10);
            }
            //patlama animasyonunu göster
            animator.SetTrigger("OnEnemyDeath");
            //hızı sıfırla 
            speed = 0;
            //kendini yok et
            Destroy(this.gameObject);
        }
    }

}
