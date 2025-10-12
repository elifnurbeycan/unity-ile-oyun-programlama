using UnityEngine;

public class PlayerMovement_Start : MonoBehaviour
{
    void Start()
    {
        // Nesnenin pozisyonunu oyun başında bir kez ayarla
        transform.position = new Vector3(2f, 1f, 0f);
    }
}
