using UnityEngine;

public class PlayerMovement_UpdateConstant : MonoBehaviour
{
    // Her frame bu kadar birim hareket eder (deltaTime yok!)
    public float speed = 0.1f;

    void Update()
    {
        // Z ekseninde ileri
        transform.Translate(Vector3.forward * speed);
    }
}
