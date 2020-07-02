using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTurn

public class GameManager : MonoBehaviour
{
    List<IPlayer> players;


    int currPlayer = 0;
    int maxPlayers = 2;

    [SerializeField]
    private List<Card> turnedCards;

    private int maxCardsTurned = 2;

    private void Awake()
    {
        
        turnedCards = new List<Card>();
        players = new List<IPlayer>();
        AddPlayers();
        SubscribeToEvents();
    }

    private void Start()
    {
        
        players[0].StartTurn();

    }

    private void AddPlayers()
    {;
        Player p = Instantiate(new GameObject(), transform).AddComponent<Player>();
        p.Name = "Tomas";
        Player p2 = Instantiate(new GameObject(), transform).AddComponent<Player>();
        p2.Name = "Tasd";

        players.Add(p);
        players.Add(p2);
    }

    private void SubscribeToEvents()
    {
        foreach (Player p in players)
            p.OnCardSelect += AddCard;
    }


    private void AddCard(object sender, OnCardSelectEventArgs e)
    {
        turnedCards.Add(e.selectedCard);
        if (turnedCards.Count >= maxCardsTurned)
        {
            if (Check(turnedCards))
            {
                StartCoroutine(DestroyAllCards(turnedCards));
                IPlayer player = (IPlayer)sender;
                player.Score += 1;
            }
            else
            {
                StartCoroutine(TurnAllCards(turnedCards));
                NextPlayer();
            }
           
        }
    }

    private IEnumerator DestroyAllCards(List<Card> turnedCards)
    {
        yield return new WaitForSeconds(.5f);
        foreach (Card c in turnedCards)
            Destroy(c.gameObject);
        turnedCards.Clear();
       
    }

    private IEnumerator TurnAllCards(List<Card> turnedCards)
    {
        yield return new WaitForSeconds(.5f);
        foreach (Card c in turnedCards)
            c.FlipCard();
        turnedCards.Clear();
    }

    private bool Check(List<Card> cardsToCheck)
    {
        if (cardsToCheck.Count <= 0)
            return false;
        int id = cardsToCheck[0].cardId;
        for (int i = 1; i < cardsToCheck.Count; i++)
        {
            if (cardsToCheck[i].cardId != id)
                return false;
        }
        return true;

    }

    

    private void NextPlayer()
    {
        players[currPlayer].EndTurn();
        currPlayer++;
        if (currPlayer >= 2)
            currPlayer = 0;
        players[currPlayer].StartTurn();
    }
        
    
}
