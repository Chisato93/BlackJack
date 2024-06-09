using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject ReadyButton;
    public GameObject BettingPanel;
    public GameObject SelectActionPanel;
    public GameObject SelectAceCard;
    public GameObject bustPanel;
    public TMP_Text bettingText;
    public UnityEvent onBettingCompleted;
    public UnityEvent onTurnFinished;
    private PlayerSeat currentPlayerSeat;

    public void TurnOnSelectAceCardPanel(bool isActive, Card card)
    {
        SelectAceCard.SetActive(isActive);

        if (isActive)
        {
            SelectAceCard acecard = SelectAceCard.GetComponent<SelectAceCard>();
            acecard.currentAceCard = card;
        }    
    }

    public void TurnOnSelectActionPanel(bool isActive)
    {
        SelectActionPanel.SetActive(isActive);
        if(isActive)
        {
            SelectActionPanelScript select = SelectActionPanel.GetComponent<SelectActionPanelScript>();
            select.SeatNumber.text = $"Seat : {currentPlayerSeat.gameObject.name.Substring(currentPlayerSeat.gameObject.name.Length - 1)}";
            if (currentPlayerSeat.CanSplit()) select.splitBtn.interactable = true;
            select.SetImgBundle();
        }
    }    

    public void TurnOnReadyButton(bool isActive)
    {
        ReadyButton.SetActive(isActive);
    }

    public void TurnOnBettingPanel(bool isActive)
    {
        if (isActive) bettingText.text = $"Seat : {currentPlayerSeat.gameObject.name.Substring(currentPlayerSeat.gameObject.name.Length-1)}\nChoose Betting Amount......";
        BettingPanel.SetActive(isActive);
    }

    public void SetPlayerSeat(PlayerSeat curPlayerSeat)
    {
        currentPlayerSeat = curPlayerSeat;
    }

    public PlayerSeat GetCurrentPlayerSeat()
    {
        return currentPlayerSeat;
    }
}
