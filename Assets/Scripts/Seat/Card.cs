using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    CardShape card_Shape;
    public int card_Number { get; private set; }
    bool isSelected = false;
    public bool IsPlayerCard { get; set; } = false;

    private void Awake()
    {
        InitCardInfo();
    }

    private void InitCardInfo()
    {
        const string prefix = "PlayingCards_";
        const string suffix = "_00";
        int startIndex = gameObject.name.IndexOf(prefix) + prefix.Length;
        int lastIndex = gameObject.name.LastIndexOf(suffix) - startIndex;
        string shapeAndNumber = gameObject.name.Substring(startIndex, lastIndex);
        Debug.Log(shapeAndNumber);

        string[] parts = new string[2];
        const int SHAPE = 0, NUMBER = 1;
        const int NUMBER_LENGTH = 2;

        int boundary = shapeAndNumber.Length - NUMBER_LENGTH;
        parts[SHAPE] = shapeAndNumber.Substring(0, boundary);
        parts[NUMBER] = shapeAndNumber.Substring(boundary);

        card_Shape = (CardShape)Enum.Parse(typeof(CardShape), parts[SHAPE].ToUpper());
        card_Number = int.Parse(parts[NUMBER]);
    }

    private void Start()
    {
        if(card_Number == 1 && isSelected && IsPlayerCard)
            AceCard();
    }

    void AceCard()
    {
        // 1 or 11 선택해서 카드 값을 바꾼다.
    }
}
