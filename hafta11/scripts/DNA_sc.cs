using UnityEngine;
using System.Collections.Generic;

public class DNA_sc
{
    List<int> genes = new List<int>();
    int dnaLength = 0;
    int maxValues = 0;

    public DNA_sc(int length, int maxV)
    {
        this.dnaLength = length;
        this.maxValues = maxV;
        SetRandom();
    }

    public void SetRandom()
    {
        genes.Clear();
        for (int i = 0; i < this.dnaLength; i++)
        {
            this.genes.Add(Random.Range(0, this.maxValues));
        }
    }

    public void SetGene(int pos, int value)
    {
        this.genes[pos] = value;
    }

    public int GetGene(int pos)
    {
        return this.genes[pos];
    }

    public void Combine(DNA_sc parent1, DNA_sc parent2)
    {
        for (int i = 0; i < this.dnaLength; i++)
        {
            if (i < this.dnaLength / 2.0f)
            {
                int c = parent1.GetGene(i);
                this.genes[i] = c ;
            }
            else
            {
                int c = parent2.GetGene(i);
                this.genes[i] = c ;
            }
        }
    }

    public void Mutate()
    {
        // Mevcut genetik koddaki rastgele bir konumdaki genin değerini rastgele olarak belirle
        this.genes[Random.Range(0, this.dnaLength)] = Random.Range(0, this.maxValues);
    }
}