using UnityEditor;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public void SitDone()
    {
        Seats seats = FindObjectOfType<Seats>();
        if (!seats.AtLeastOneSeats()) return;

        JoinChip[] joinChips = FindObjectsOfType<JoinChip>();
        foreach (JoinChip joinChip in joinChips)
        {
            joinChip.Ready();
            joinChip.SetOutliner(false);
        }
        UIController.instance.TurnOnReadyButton(false);

        GameController.instance.NextStep();
    }

    public void ChooseBettingAmount(int amount)
    {
        PlayerSeat currentPlayerSeat = UIController.instance.GetCurrentPlayerSeat();
        if (currentPlayerSeat != null && GameManager.instance.Gold >= amount)
        {
            currentPlayerSeat.Bet_Amount = amount;
            GameManager.instance.Gold -= amount;
            UIController.instance.SetExchange();
            UIController.instance.onBettingCompleted?.Invoke(); // 베팅 완료 이벤트 호출
        }
    }

    public void ExChangeHeartToGold()
    {
        GameManager.instance.ChangeHeartToGold();
        UIController.instance.SetExchange();
    }
    public void ExChangeGoldToHeart()
    {
        GameManager.instance.ChangeGoldToHeart();
        UIController.instance.SetExchange();
    }

    public void CloseExchange()
    {
        UIController.instance.CloseExchangePanel();
    }
}
