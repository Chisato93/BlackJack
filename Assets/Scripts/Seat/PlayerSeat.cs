public class PlayerSeat : Seat
{
    public bool isEmptySeat = true;
    public int Bet_Amount { get; set; }

    public void SetBettingAmount()
    {
        UIController.instance.TurnOnBettingPanel(true);


    }
}
