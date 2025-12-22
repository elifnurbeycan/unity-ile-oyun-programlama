using UnityEngine;

public class Brain_sc : MonoBehaviour
{
    int DNALength = 2;
    public float timeAlive = 0;
    public DNA_sc dna_sc;
    
    [SerializeField]
    GameObject eyes; // Unity Editörden 'Eyes' objesini buraya sürüklemeyi unutma!

    bool isAlive = true;
    bool canSeeGround = true;

    // Start is called once before the first execution of Update
    void Start()
    {
        // Init fonksiyonu PopulationManager tarafından çağırılıyor, burası boş kalabilir.
    }

    // Update is called once per frame
    void Update()
    {
        // DÜZELTİLDİ: Eğer ölü ise (isAlive false ise) return etmeli.
        if (!isAlive) return;

        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 10, Color.red, 0.1f);
        canSeeGround = false;
        RaycastHit hit;

        // DÜZELTİLDİ: RayCast -> Raycast (küçük c ile)
        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward * 10, out hit))
        {
            // Işın bir şeye çarptı, bu çarptığı şey platform mu?
            if (hit.collider.gameObject.tag == "platform")
            {
                canSeeGround = true;
            }
        }

        // Hayatta kalma süresini güncelle
        timeAlive = PopulationManager_sc.elapsed;

        // DNA'dan okuma yap ve ona göre hareket et
        float turn = 0;
        float move = 0;

        if (canSeeGround)
        {
            // Yeri görüyorsa 0. gen'e göre hareket et
            if (dna_sc.GetGene(0) == 0) move = 1;
            else if (dna_sc.GetGene(0) == 1) turn = -90;
            else if (dna_sc.GetGene(0) == 2) turn = 90;
        }
        else
        {
            // Yeri görmüyorsa 1. gen'e göre hareket et
            if (dna_sc.GetGene(1) == 0) move = 1;
            else if (dna_sc.GetGene(1) == 1) turn = -90;
            else if (dna_sc.GetGene(1) == 2) turn = 90;
        }

        // DÜZELTİLDİ: Dönme işlemi (Rotate) eklendi.
        // Karakterin kendi ekseni etrafında dönmesini sağlar.
        this.transform.Rotate(0, turn, 0);
        
        // İleri hareket
        this.transform.Translate(0, 0, move * 0.1f);
    }

    void OnCollisionEnter(Collision other)
    {
        // "dead" tag'ine sahip bir şeye (örneğin zemin dışı alan veya engel) çarparsa ölür.
        if (other.gameObject.tag == "dead")
        {
            isAlive = false;
            // İstersen burada hareket ve dönüşü sıfırlayabilirsin
            // move = 0; turn = 0;
        }
    }

    public void Init()
    {
        // DNA initialize
        // 0: forward (ileri)
        // 1: left (sola dön)
        // 2: right (sağa dön)
        dna_sc = new DNA_sc(DNALength, 3);
        timeAlive = 0;
        isAlive = true;
    }
}
