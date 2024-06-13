using UnityEngine;

public class SelectAceCard : MonoBehaviour
{
    public Card currentAceCard;

    public void Button1()
    {
        currentAceCard.card_Number = Helper.ACEONE;
        SelectActionPanelScript action = FindObjectOfType<SelectActionPanelScript>();
        action.Refresh?.Invoke();
        currentAceCard.isSelected = true;
        UIController.instance.TurnOnSelectAceCardPanel(false, null);
    }
    public void Button11()
    {
        currentAceCard.card_Number = Helper.ACEELEVEN;
        SelectActionPanelScript action = FindObjectOfType<SelectActionPanelScript>();
        UIController.instance.GetCurrentPlayerSeat().Card_Sum += currentAceCard.card_Number - Helper.ACEONE;
        action.Refresh?.Invoke();
        currentAceCard.isSelected = true;
        UIController.instance.TurnOnSelectAceCardPanel(false, null);
    }
}
