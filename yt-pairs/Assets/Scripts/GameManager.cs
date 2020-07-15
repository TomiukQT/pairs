using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class OnTurnStartEventArgs : EventArgs
{
    public string playerName;
    public bool next = true;
}

public class OnGameEndEventArgs : EventArgs
{
    public List<IPlayer> players;
}

public class GameManager : MonoBehaviour
{
    public event EventHandler<OnTurnStartEventArgs> OnTurnStart;
    public event EventHandler<OnGameEndEventArgs> OnGameEnd; 

    List<IPlayer> players;


    int currPlayer = 0;
    int maxPlayers = 2;

    [SerializeField]
    private List<Card> turnedCards;

    private int totalCards = 64;

    private int maxCardsTurned = 2;

    private Transform playersParent;

    [SerializeField]
    private GameObject particleSystemFound;

    private void Awake()
    {
        //Time.timeScale = 5f;
        turnedCards = new List<Card>();
        players = PlayerSelect.Instance().players;
        AddPlayers();
        SubscribeToEvents();
    }

    private void Start()
    {
        
        players[0].StartTurn();

    }

    private void AddPlayers()
    {
        foreach (IPlayer p in players)
            p.Prepare();    
    }

    public List<IPlayer> GetPlayers() => players;

    private void SubscribeToEvents()
    {
        foreach (IPlayer p in players)
            p.OnCardSelect += AddCard;
    }


    private void AddCard(object sender, OnCardSelectEventArgs e)
    {
        if (turnedCards.Count >= maxCardsTurned)
            return;
        e.selectedCard.FlipCard();
        turnedCards.Add(e.selectedCard);
        if (turnedCards.Count >= maxCardsTurned)
        {
            if (Check(turnedCards))
            {
                StartCoroutine(DestroyAllCards(turnedCards));
                IPlayer player = (IPlayer)sender;
                player.Score += 1;
                if (totalCards <= 0)
                {
                    GameEnd();
                    return;
                }
                else
                    OnTurnStart?.Invoke(this, new OnTurnStartEventArgs { playerName = players[currPlayer].Name, next = false });
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
        totalCards -= turnedCards.Count; 
        yield return new WaitForSeconds(.3f);
        foreach (Card c in turnedCards)
            Instantiate(particleSystemFound, c.transform.position, Quaternion.Euler(90, 0, 0));
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
        if (currPlayer >= players.Count)
            currPlayer = 0;
        OnTurnStart?.Invoke(this, new OnTurnStartEventArgs { playerName = players[currPlayer].Name});

        players[currPlayer].StartTurn();
    }

    private void GameEnd()
    {
        foreach (IPlayer p in players)
            p.EndTurn();
        OnGameEnd?.Invoke(this, new OnGameEndEventArgs { players = this.players });
            foreach (IPlayer p in players)
                p.OnCardSelect -= AddCard;
    }
    
}
