using UnityEngine;

public class PlayerMovement_HorizontalVertical : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");        // ←/→ veya A/D
        float v = Input.GetAxis("Vertical");          // ↑/↓ veya W/S
        Vector3 move = new Vector3(h, v, 0f);         // X ve Y eksenleri
        transform.Translate(move * speed * Time.deltaTime, Space.World);
    }
}
