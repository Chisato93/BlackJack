using UnityEngine;

public class SelectAceCard : MonoBehaviour
{
    public Card currentAceCard;

    public void Button1()
    {
        currentAceCard.card_Number = Helper.ACEONE;
        SelectActionPanelScript action = FindObjectOfType<SelectActionPanelScript>();
        action.SetImgBundle(true);
        currentAceCard.isSelected = true;
        UIController.instance.TurnOnSelectAceCardPanel(false, null);
    }
    public void Button11()
    {
        currentAceCard.card_Number = Helper.ACEELEVEN;
        SelectActionPanelScript action = FindObjectOfType<SelectActionPanelScript>();
        action.SetImgBundle(true);
        UIController.instance.GetCurrentPlayerSeat().Card_Sum += Helper.ACEELEVEN;
        currentAceCard.isSelected = true;
        UIController.instance.TurnOnSelectAceCardPanel(false, null);
    }
}
