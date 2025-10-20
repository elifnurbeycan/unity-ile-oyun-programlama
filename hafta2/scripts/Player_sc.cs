using UnityEngine;

public class Player_sc : MonoBehaviour
{
    public int speed = 10;
    public GameObject laserPrefab;
    [SerializeField] 
    private float nextFire = 0;  // bir sonraki ateş zamanı
    [SerializeField] 
    private float fireRate = 0.25f;


    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {
            FireLazer();
            nextFire = Time.time + fireRate;
        }

    }

    void FireLazer()
    {
        Instantiate(laserPrefab,
            this.transform.position,
            Quaternion.identity);
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x,
                                        Mathf.Clamp(transform.position.y, -3.8f, 0),
                                        0);
        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x< -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }
}
