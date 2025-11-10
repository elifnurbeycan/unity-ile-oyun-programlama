using UnityEngine;

public class Player_sc : MonoBehaviour
{
    public int lives = 3;
    public int speed = 10;

    [Header("Shooting")]
    public GameObject laserPrefab;
    [SerializeField] private float fireRate = 0.25f;
    private float nextFire = 0f;

    [SerializeField] private Transform[] firePoints; 

    void Start()
    {
        transform.position = Vector3.zero;
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKey(KeyCode.Space) && Time.time >= nextFire)
        {
            FireLazer();
            nextFire = Time.time + fireRate;
        }
    }

    void FireLazer()
    {
        if (firePoints == null || firePoints.Length == 0)
        {
            Debug.LogWarning("Player_sc: FirePoints atanmamış!");
            return;
        }

        foreach (var fp in firePoints)
        {
            if (fp == null) continue;
           
            Instantiate(laserPrefab, fp.position, fp.rotation);
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput   = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * speed * Time.deltaTime);

        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(transform.position.y, -3.8f, 0),
            0
        );

        if (transform.position.x > 11.3f)
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        else if (transform.position.x < -11.3f)
            transform.position = new Vector3(11.3f, transform.position.y, 0);
    }

    public void Damage()
    {
        lives--;

        if (lives == 0)
        {
            var smObj = GameObject.Find("SpawnManager");
            if (smObj != null)
            {
                var spawnManager_sc = smObj.GetComponent<SpawnManager_sc>();
                if (spawnManager_sc != null) spawnManager_sc.OnPlayerDeath();
            }
            else
            {
                Debug.LogError("Player_sc::Damage SpawnManager bulunamadı.");
            }

            Destroy(this.gameObject);
        }
    }

    public void AddHealth(int amount)
    {
        lives += amount;
        if (lives > 3) lives = 3;
        Debug.Log("Health increased! Current lives: " + lives);
    }
}
