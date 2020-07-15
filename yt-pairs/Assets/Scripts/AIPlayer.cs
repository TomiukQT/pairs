using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class AIPlayer : MonoBehaviour, IPlayer
{
    private const float TIME_BETWEEN_TURN = 1f;
    private float currTimeBetweenTurn = 0f;

    private string _name;
    private int _score = 0;

    public string Name { get => _name; set => _name = "AI_" + UnityEngine.Random.Range(150, 1000).ToString(); }
    public int Score { get => _score; set => _score = value; }

    public event EventHandler<OnCardSelectEventArgs> OnCardSelect;

    private bool isTurn = false;

    private Memory memory;

    private List<Card> cardsToChoose;
    private Card chosen;

    private Transform cardsParent;

    public void Prepare()
    {
        cardsToChoose = new List<Card>();
        cardsParent = GameObject.Find("Cards").transform;
        _name = "AI_" + UnityEngine.Random.Range(150, 1000).ToString();
    }

    public void SetMemory(Memory _memory) => memory = _memory;


    public void EndTurn()
    {
        isTurn = false;
    }

    public void StartTurn()
    {
        isTurn = true;
        chosen = null;
        currTimeBetweenTurn = 0f;
        
    }

    private void Update()
    {
        if(isTurn && currTimeBetweenTurn >= TIME_BETWEEN_TURN)
        {
            Card choose = ChooseCard();
            //choose.FlipCard();
            OnCardSelect?.Invoke(this, new OnCardSelectEventArgs { selectedCard = choose });
            currTimeBetweenTurn = 0f;
        }
        currTimeBetweenTurn += Time.deltaTime;
    }

    private void UpdateCardsToChoose()
    {
        cardsToChoose.Clear();
        foreach (Transform t in cardsParent)
        {
            Card c;
            if(t.TryGetComponent<Card>(out c))
                cardsToChoose.Add(c);
        }
        memory.GetMemory().RemoveAll(x => !cardsToChoose.Contains(x));
    }

    private Card ChooseCard()
    {
        UpdateCardsToChoose();
        int id;
        Card pair;
        if (CheckMemory(out id))
        {
            Card c = memory.GetMemory().Find(x => x.cardId == id);
            chosen = c;
            memory.Remove(c);
            return c;
        }
        else if(chosen == null)
        {
            chosen = ChooseRandomCard();
            memory.Add(chosen);
            return chosen; 
        }
        else if(chosen != null && CheckChosenWithMemory(out pair))
        {
            if (memory.Contains(chosen))
                memory.Remove(chosen);
            memory.Remove(pair);
            return pair;
        }
        else
        {
            Card c2 = ChooseRandomCard();
            memory.Add(c2);
            return c2;
        }
        //Check if in memory arent 2 same cards.
        // then choose random card
        // at second turn, compare chosen card with memory.
    }

    private bool CheckMemory(out int id)
    {
        Dictionary<int, Card> memCheck = new Dictionary<int, Card>();
        id = -1;
        foreach (Card c in memory.GetMemory())
        {
            if (memCheck.ContainsKey(c.cardId)) // 2 same cards in memory
            {
                id = c.cardId;
                return true;
            }
            else
                memCheck.Add(c.cardId, c);
        }


        return false;
    }

    private bool CheckChosenWithMemory(out Card pair)
    {
        int chosenId = chosen.cardId;
        pair = null;
        for (int i = 0; i < memory.GetMemory().Count; i++)
        {
            if(memory.GetMemory()[i].cardId == chosenId && memory.GetMemory()[i] != chosen)
            {
                pair = memory.GetMemory()[i];
                return true;
            }
        }
        return false;
    }

    private Card ChooseRandomCard()
    {
        List<Card> cards = new List<Card>(cardsToChoose);
        cards.RemoveAll(x => memory.Contains(x));
        int rng = UnityEngine.Random.Range(0,cards.Count);
        return cards[rng];
    }

    

}
