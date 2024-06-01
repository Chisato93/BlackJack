using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    CardShape card_Shape;
    int card_Number;
    bool isSelected = false;

    private void Start()
    {
        int startIndex = gameObject.name.IndexOf("PlayingCards_") + "PlayingCards_".Length;
        string shapeAndNumber = gameObject.name.Substring(startIndex);
        string[] parts = shapeAndNumber.Split('_');
        
        const int SHAPE = 0, NUMBER = 1;

        if (Enum.TryParse(parts[SHAPE], out CardShape shape))
        {
            card_Shape = shape;
        }
        if (int.TryParse(parts[NUMBER], out int number))
        {
            card_Number = number;
        }
    }

    void Temp()
    {

        if (card_Number == 1 && !isSelected)
        {
            Debug.Log("1 or 11 입력 받고 받은 숫자를 가져와서 여기에 넣음");
            isSelected = true;
        }
    }

}
