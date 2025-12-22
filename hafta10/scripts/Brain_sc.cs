using UnityEngine;

public class Brain_sc : MonoBehaviour
{
    int DNALength = 2;
    public float timeAlive = 0;
    public DNA_sc dna_sc;
    
    [SerializeField]
    GameObject eyes; 

    public bool isAlive = true;
    public bool canSeeGround = true;
    
    Rigidbody rb; 

    //Start yerine Awake kullandık
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!isAlive) return;

        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 10, Color.red, 0.1f);
        canSeeGround = false;
        RaycastHit hit;

        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward * 10, out hit))
        {
            if (hit.collider.gameObject.tag == "platform")
            {
                canSeeGround = true;
            }
        }

        timeAlive = PopulationManager_sc.elapsed;

        float turn = 0;
        float move = 0;

        if (canSeeGround)
        {
            if (dna_sc.GetGene(0) == 0) move = 1;
            else if (dna_sc.GetGene(0) == 1) turn = -90;
            else if (dna_sc.GetGene(0) == 2) turn = 90;
        }
        else
        {
            if (dna_sc.GetGene(1) == 0) move = 1;
            else if (dna_sc.GetGene(1) == 1) turn = -90;
            else if (dna_sc.GetGene(1) == 2) turn = 90;
        }

        this.transform.Rotate(0, turn * Time.deltaTime * 100f, 0);

        if(move > 0)
        {
            Vector3 targetVelocity = this.transform.forward * move * 2f;
            rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "dead")
        {
            isAlive = false; 
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void Init()
    {
        dna_sc = new DNA_sc(DNALength, 3);
        timeAlive = 0;
        isAlive = true;
    }
}

