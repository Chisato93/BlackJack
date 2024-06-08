using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Seat : MonoBehaviour
{
    public List<Card> Card_Deck { get; set; } = new List<Card>(); // Initialize the list
    public int Card_Sum { get; set; }
    public GameResult Result { get; set; }
    TMP_Text sumText;

    private void Start()
    {
        sumText = GetComponentInChildren<TMP_Text>();
        sumText.text = "0";
    }

    public void UpdateSum(int amt)
    {
        Card_Sum += amt;
        sumText.text = Card_Sum.ToString();
    }

    void AddCard(Card newCard)
    {
        Card_Deck.Add(newCard);
        Card_Sum += newCard.card_Number;

    }

}
