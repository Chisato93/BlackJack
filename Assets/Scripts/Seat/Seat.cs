using System.Collections.Generic;
using UnityEngine;

public abstract class Seat : MonoBehaviour
{
    public List<Card> Card_Deck { get; set; } = new List<Card>(); // Initialize the list
    public int Card_Sum { get; set; }
    public GameResult Result { get; set; }

    void AddCard(Card newCard)
    {
        Card_Deck.Add(newCard);
        Card_Sum += newCard.card_Number;

    }

}
