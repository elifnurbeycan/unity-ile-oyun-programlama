using UnityEngine;

public class PlayerMovement_Vertical : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        float v = Input.GetAxis("Vertical");          // ↑ = +1, ↓ = -1 (W/S de çalışır)
        Vector3 move = new Vector3(0f, v, 0f);        // Y ekseninde hareket
        transform.Translate(move * speed * Time.deltaTime, Space.World);
    }
}
