using UnityEngine;
using UnityEngine.UI;

public class SelectActionPanel : MonoBehaviour
{
    public Button splitBtn;

    int currentTurn;
    public void Hit()
    {
        currentTurn = GameController.instance.Turn;
        PlayerSeat currentSeat = UIController.instance.GetCurrentPlayerSeat();
        CardPooling.instance.Phase(currentSeat.transform.GetChild(0), currentTurn);
        currentSeat.UpdateSum(currentSeat.transform.GetChild(0).GetChild(currentTurn).GetComponent<Card>().card_Number);
        OverTurn(currentSeat.Card_Sum);
        currentTurn++;
    }

    public void Stay()
    {

    }

    public void DoubleDonw()
    {

    }

    public void Split()
    {

    }

    private void OverTurn(int sum)
    {
        if (sum > Helper.MAXSUM)
        {
            UIController.instance.TurnOnSelectActionPanel(false);
        }
    }
}
