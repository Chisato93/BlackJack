using UnityEngine;

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

    public void TurnOnBettingPanel(bool isActive)
    {
        BettingPanel.SetActive(isActive);
    }
}
