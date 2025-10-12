using UnityEngine;

public class PlayerMovement_Normalized : MonoBehaviour
{
    // Saniyede speed birim
    public float speed = 1f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
