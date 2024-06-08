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
    public TMP_Text bettingText;
    public UnityEvent onBettingCompleted;
    private PlayerSeat currentPlayerSeat;

    public void TurnOnSelectActionPanel(bool isActive)
    {
        SelectActionPanel.SetActive(isActive);
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
