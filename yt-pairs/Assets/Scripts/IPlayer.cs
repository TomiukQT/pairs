using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCardSelectEventArgs : EventArgs
{
    public Card selectedCard;
}


public interface IPlayer
{

    public event EventHandler<OnCardSelectEventArgs> OnCardSelect;

    public void Prepare();
    public void StartTurn();
    public void EndTurn();
    public string Name { get; set; }
    public int Score { get; set; }

    
}
