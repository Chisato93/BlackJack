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

    public void TurnOnReadyButton(bool isActive)
    {
        ReadyButton.SetActive(isActive);
    }

    public GameObject BettingPanel;
    public UnityEvent onBettingCompleted;
    private PlayerSeat currentPlayerSeat;

    public void TurnOnBettingPanel(bool isActive)
    {
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
