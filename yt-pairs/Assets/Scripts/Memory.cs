using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory
{
    private List<Card> memory = new List<Card>();

    private int capacity;
    private float memoryAddSuccesRate;

    public Memory(int _capacity)
    {
        capacity = _capacity;
        memoryAddSuccesRate = 1;
    }

    public Memory(int _capacity, float succesRate)
    {
        capacity = _capacity;
        memoryAddSuccesRate = succesRate;
    }

    public List<Card> GetMemory() => memory;

    public void Add(Card card)
    {
        if (Random.Range(0, 1) > memoryAddSuccesRate)
            return;

        if (memory.Count >= capacity)
        {
            memory.RemoveAt(0);
        }
        memory.Add(card);
    }

    public void Remove(Card card)
    {
        if (memory.Contains(card))
            memory.Remove(card);
    }

    public bool Contains(Card card) => memory.Contains(card);





}
