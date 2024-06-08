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
        if (currentPlayerSeat != null)
        {
            currentPlayerSeat.Bet_Amount = amount;
            UIController.instance.onBettingCompleted?.Invoke(); // 베팅 완료 이벤트 호출
        }
    }
}
