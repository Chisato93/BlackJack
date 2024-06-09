using UnityEngine;

public class SelectAceCard : MonoBehaviour
{
    public Card currentAceCard;
    public void Button1()
    {
        currentAceCard.card_Number = Helper.ACEONE;
        currentAceCard.isSelected = true;
        UIController.instance.TurnOnSelectAceCardPanel(false, null);
    }
    public void Button11()
    {
        currentAceCard.card_Number = Helper.ACEELEVEN;
        UIController.instance.GetCurrentPlayerSeat().Card_Sum += (Helper.ACEELEVEN - Helper.ACEONE);
        currentAceCard.isSelected = true;
        UIController.instance.TurnOnSelectAceCardPanel(false, null);
    }
}
