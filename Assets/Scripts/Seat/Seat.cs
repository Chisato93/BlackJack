using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Seat : MonoBehaviour
{
    public Action action;
    public List<Card> Card_Deck { get; set; } = new List<Card>(); // Initialize the list
    public int Card_Sum { get; set; }
    public GameResult Result { get; set; }
    public TMP_Text sumText;
    const int maxSplitcount = 2;
    public bool isBust = false;

    private void Start()
    {
        sumText.text = "0";
        action += SetText;
    }

    void SetText()
    {
        sumText.text = Card_Sum.ToString();
    }

    public void InitCardDeck()
    {
        Card_Deck.Clear();
        Card_Sum = 0;
    }

    public bool CanSplit()
    {
        if (Card_Deck.Count <= maxSplitcount)
        {
            return Card_Deck[0].cardRealNumber == Card_Deck[1].cardRealNumber;
        }
        return false;
    }

    public void AddCard(Card newCard)
    {
        Card_Deck.Add(newCard);
        Card_Sum += newCard.card_Number;

        if (Card_Sum > Helper.MAXSUM)
        {
            sumText.text = Helper.BUST;
        }
        else sumText.text = Card_Sum.ToString();
    }

    public Card HaveAceCard()
    {
        foreach (Card card in Card_Deck)
        {
            if (card.cardRealNumber == 1)
                return card;
        }

        return null;
    }


    public void AddCard(Card newCard, bool doubleDown)
    {
        if (!doubleDown)
        {
            Card_Deck.Add(newCard);
            Card_Sum += newCard.card_Number;

            if (Card_Sum > Helper.MAXSUM)
            {
                sumText.text = Helper.BUST;
                isBust = true;
            }
            else sumText.text = Card_Sum.ToString();
        }
        else
        {
            Card_Deck.Add(newCard);
            Card_Sum += newCard.card_Number;
            sumText.text = "??";
        }
    }

    public bool isBlackJack()
    {
        int first = Card_Deck[Helper.FIRSTCARD].cardRealNumber;
        int second = Card_Deck[Helper.SECONDCARD].cardRealNumber;
        if ((first == 1 && (second == 10 || second == 11 || second == 12 || second == 13)) || ((first == 10 || first == 11 || first == 12 || first == 13) && second == 1))
        {
            sumText.text = Helper.BLACKJACK;
            return true;
        }
        else
            return false;
    }

}
