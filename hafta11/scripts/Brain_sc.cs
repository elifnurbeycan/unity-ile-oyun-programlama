using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;


[RequireComponent(typeof(ThirdPersonCharacter))]
public class Brain_sc : MonoBehaviour
{
    int DNALength = 1;
    public float timeAlive = 0;
    public DNA_sc dna_sc;
     
    ThirdPersonCharacter character;

    public bool isAlive = true;
    
    Vector3 mVector;
    bool isJumping;
    
     Vector3 startPos;
    public float distanceTravelled =0;
    
    void Start()
    {
        
    }

    void Update()
    {
        // Read DNA
        float h =0;
        float v =0;
        bool crouch = false ; //çökme
        if (dna_sc.GetGene(0) == 0 ) v=1; //ileri gitme
        else if (dna_sc.GetGene(0) == 1 ) v=-1; // geri gitme 
        else if (dna_sc.GetGene(0) == 2 ) h=-1;
        else if (dna_sc.GetGene(0) == 3 ) h=1;
        else if (dna_sc.GetGene(0) == 4 ) isJumping= true;
        else if (dna_sc.GetGene(0) == 5 ) crouch= true;

        mVector = v*Vector3.forward + h*Vector3.right;
        
        isJumping=false;
        if (isAlive)
        {
            character.Move(mVector, crouch , isJumping);
            timeAlive+=Time.deltaTime;
            distanceTravelled = Vector3.Distance(this.transform.position, startPos);
        }
        isJumping = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "dead")
        {
            isAlive = false; 
        }
    }

    public void Init()
    {
        // 0 ileri
        //1 geri 
        //2 sol
        //3 sağ
        //4 zıpla
        //5 çökme kapanma
        dna_sc = new DNA_sc(DNALength, 6);
        character = GetComponent<ThirdPersonCharacter>();
        timeAlive = 0;
        isAlive = true;
        startPos = this.transform.position;
    }
}

