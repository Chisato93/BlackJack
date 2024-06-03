using UnityEngine;

public class Buttons : MonoBehaviour
{
    public void SitDone()
    {
        JoinChip[] seats = FindObjectsOfType<JoinChip>();
        foreach (JoinChip seat in seats)
        {
            seat.Ready();
            seat.SetOutliner(false);
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
