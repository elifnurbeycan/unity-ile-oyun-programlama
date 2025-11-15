using System.Collections;
using NUnit.Framework.Internal;
using UnityEngine;

public class Player_sc : MonoBehaviour
{
    [SerializeField]
    float speed = 10;

    float speedMultiplier = 2;

    float nextFire = 0;

    [SerializeField]
    float fireRate = 0.25f;

    [SerializeField]
    bool isTripleShotActive = false;

    [SerializeField]
    bool isSpeedBonusActive = false;

    [SerializeField]
    GameObject laserPrefab;

    [SerializeField]
    GameObject tripleLaserPrefab;

    [SerializeField]
    int lives = 3;

    [SerializeField]

    bool isShieldBonusActive = false;

    [SerializeField]
    GameObject shieldVisualizer;
    

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        if (shieldVisualizer != null)
            shieldVisualizer.SetActive(false);

        
    }

    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && (Time.time > nextFire))
        {
            nextFire = Time.time + fireRate;
            FireLaser();
        }

    }

    void FireLaser()
    {
        if (!isTripleShotActive)
        {
            Instantiate(laserPrefab,
                    (this.transform.position + new Vector3(0, 1.05f, 0)),
                    Quaternion.identity);
        }
        else
        {
            Instantiate(tripleLaserPrefab,
                    (this.transform.position),
                    Quaternion.identity);
        }

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
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        // Önce kalkan kontrolü
        if (isShieldBonusActive)
        {
            isShieldBonusActive = false;

            if (shieldVisualizer != null)
                shieldVisualizer.SetActive(false);

            return;
        }

        lives--;

        if (lives == 0)
        {
            SpawnManager_sc spawnManager_sc = GameObject
                .Find("Spawn_Manager")
                .GetComponent<SpawnManager_sc>();

            if (spawnManager_sc != null)
            {
                spawnManager_sc.OnPlayerDeath();
            }
            else
            {
                Debug.LogError("Player_sc::Damage spawnManager_sc is NULL");
            }

            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        isTripleShotActive = true;
        StartCoroutine(TripleShotCancelRoutine());
    }

    public void SpeedBonusActive()
    {
        isSpeedBonusActive = true;
        speed *= speedMultiplier;
        StartCoroutine(SpeedBonusCancelRoutine());
    }

    public void ShieldBonusActive() //shield şeklinde yap
    {
        isShieldBonusActive = true;
        if (shieldVisualizer != null)
            shieldVisualizer.SetActive(true);
    }

    IEnumerator TripleShotCancelRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isTripleShotActive = false;
    }

    IEnumerator SpeedBonusCancelRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBonusActive = false;
        speed /= speedMultiplier;
    }

    //shield için cancel eklenebilir


}
