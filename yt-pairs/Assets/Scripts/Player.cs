using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Player : MonoBehaviour, IPlayer
{
    private int _score = 0;
    private string _name;

    public event EventHandler<OnCardSelectEventArgs> OnCardSelect;

    public string Name { get => _name; set => _name = value; }
    public int Score { get => _score; set => _score = value; }

    private bool isTurn;

    public Player(string name)
    {
        _name = name;
    }

    private void Awake()
    {
        isTurn = false;
    }

    private void Update()
    {
        if (isTurn)
        {         
            GetInput();
        }
    }

    public void StartTurn() => isTurn = true;

    public void EndTurn() => isTurn = false;



    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Card card = ClickOnCard();
            if (card != null && !card.isFlipped)
            {
                OnCardSelect?.Invoke(this, new OnCardSelectEventArgs { selectedCard = card });
                card.FlipCard();
            }
        }
        return;
    }

    private Card ClickOnCard()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);


        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null)
        {
            Card selectedCard;
            if (hit.collider.gameObject.TryGetComponent<Card>(out selectedCard))
            {
                //selectedCard.FlipCard();
                return selectedCard;
            }
        }

        return null;
    }


    public string GetName()
    {
        return Name;
    }

    
}
