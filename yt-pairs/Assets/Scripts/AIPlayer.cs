using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class AIPlayer : MonoBehaviour, IPlayer
{
    private string _name = "AI_" + UnityEngine.Random.Range(150, 1000).ToString();
    private int _score = 0;

    public string Name { get => _name; set => _name = "AI_" + UnityEngine.Random.Range(150, 1000).ToString(); }
    public int Score { get => _score; set => _score = value; }

    public event EventHandler<OnCardSelectEventArgs> OnCardSelect;

    private bool isTurn = false;

    private List<Card> memory;
    private List<Card> cardsToChoose;

    private Transform cardsParent;

    private void Awake()
    {
        cardsParent = GameObject.Find("Cards").transform;
    }

    public void EndTurn()
    {
        isTurn = false;
    }

    public void StartTurn()
    {
        isTurn = true;
        while(isTurn)
            OnCardSelect?.Invoke(this, new OnCardSelectEventArgs { selectedCard = ChooseCard() });
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
    }

    private Card ChooseCard()
    {
        UpdateCardsToChoose();
        int id;
        if (CheckMemory(out id))
        {
            Card c = memory.Find(x => x.cardId == id);
            memory.Remove(c);
            return c;
        }
        else if(true)
        {

        }
        //Check if in memory arent 2 same cards.
        // then choose random card
        // at second turn, compare chosen card with memory.
        return null;
    }

    private bool CheckMemory(out int id)
    {
        Dictionary<int, Card> memCheck = new Dictionary<int, Card>();
        id = -1;
        foreach (Card c in memory)
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

    private void ChooseRandomCard()
    {
        List<Card> cards = new List<Card>(cardsToChoose);
        cards.RemoveAll(x => !memory.Contains(x));
    }

}
