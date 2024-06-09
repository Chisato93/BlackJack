using System;
using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    CardShape card_Shape;
    public int cardRealNumber {  get; private set; }
    public int card_Number { get; set; }
    public bool isSelected = false;

    private void Awake()
    {
        InitCardInfo();
    }
    public string GetCardShape()
    {
        switch (card_Shape)
        {
            case CardShape.CLUB:
                return Helper.CLUB;
            case CardShape.DIAMOND:
                return Helper.DIAMOND;
            case CardShape.HEART:
                return Helper.HEART;
            case CardShape.SPADE:
                return Helper.SPADE;
        }
        return null;
    }
    private void InitCardInfo()
    {
        const string prefix = "PlayingCards_";
        const string suffix = "_00";
        int startIndex = gameObject.name.IndexOf(prefix) + prefix.Length;
        int lastIndex = gameObject.name.LastIndexOf(suffix) - startIndex;
        string shapeAndNumber = gameObject.name.Substring(startIndex, lastIndex);

        string[] parts = new string[2];
        const int SHAPE = 0, NUMBER = 1;
        const int NUMBER_LENGTH = 2;

        int boundary = shapeAndNumber.Length - NUMBER_LENGTH;
        parts[SHAPE] = shapeAndNumber.Substring(0, boundary);
        parts[NUMBER] = shapeAndNumber.Substring(boundary);

        card_Shape = (CardShape)Enum.Parse(typeof(CardShape), parts[SHAPE].ToUpper());
        card_Number = int.Parse(parts[NUMBER]);
        cardRealNumber = card_Number;
        if (card_Number >= 10) card_Number = 10;
    }

    public bool IsAceCard()
    {
        if (cardRealNumber == 1 && !isSelected) return true;
        else return false;
    }

}
