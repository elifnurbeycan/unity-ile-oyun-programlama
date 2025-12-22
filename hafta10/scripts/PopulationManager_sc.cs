using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PopulationManager_sc : MonoBehaviour
{
    [SerializeField]
    GameObject botPrefab;

    public int populationSize = 10;

    public List<GameObject> population = new List<GameObject>();

    public static float elapsed = 0;
    public float trialTime = 5;
    int generation = 1;

    GUIStyle guiStyle = new GUIStyle();

    void OnGUI()
    {
        guiStyle.fontSize = 25;
        guiStyle.normal.textColor = Color.white;
        GUI.BeginGroup(new Rect(10, 10, 250, 150));
        GUI.Box(new Rect(0, 0, 140, 140), "Stats", guiStyle);
        GUI.Label(new Rect(10, 25, 200, 30), "Gen: " + generation, guiStyle);
        GUI.Label(new Rect(10, 50, 200, 30), string.Format("Time: {0:0.00}", elapsed), guiStyle);
        GUI.Label(new Rect(10, 75, 200, 30), "Population: " + population.Count, guiStyle);
        GUI.EndGroup();
    }

    void Start()
    {
        // İlk popülasyonu oluştur
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 pos = new Vector3(this.transform.position.x + Random.Range(-2.0f, 2.0f),
                                       this.transform.position.y,
                                       this.transform.position.z + Random.Range(-2.0f, 2.0f));
            
            GameObject bot = Instantiate(botPrefab, pos, this.transform.rotation);
            bot.GetComponent<Brain_sc>().Init();
            population.Add(bot);
        }
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        
        if (elapsed >= trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
    }

    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(this.transform.position.x + Random.Range(-2.0f, 2.0f),
                                       this.transform.position.y,
                                       this.transform.position.z + Random.Range(-2.0f, 2.0f));
        
        GameObject child = Instantiate(botPrefab, pos, this.transform.rotation);
        Brain_sc child_b = child.GetComponent<Brain_sc>();
        child_b.Init(); // Çocuğun beynini başlat

        // Mutasyon veya DNA birleştirme
        if (Random.Range(0, 100) == 1)
        {
            child_b.dna_sc.Mutate();
        }
        else
        {
            child_b.dna_sc.Combine(parent1.GetComponent<Brain_sc>().dna_sc,
                                   parent2.GetComponent<Brain_sc>().dna_sc);
        }
        return child;
    }

    void BreedNewPopulation()
    {
        // En uzun süre hayatta kalanlar listenin sonuna gelir.
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<Brain_sc>().timeAlive).ToList();

        population.Clear();

        for (int i = (int)(sortedList.Count / 2.0f); i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        // Eski jenerasyonu yok et
        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        generation++;
    }
}
