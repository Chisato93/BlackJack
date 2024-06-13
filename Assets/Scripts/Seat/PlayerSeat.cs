using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSeat : Seat
{
    public bool isEmptySeat = true;
    public int Bet_Amount { get; set; }

    public List<GameObject> chips;
    public bool haveDoubleDownCard = false;

    public void SetText(bool isempty)
    {
        sumText.text = isempty ? "O" : "X";
        sumText.color = isempty ? Color.red : Color.green;
    }
    public IEnumerator SetBettingAmount(float delay)
    {
        UIController.instance.SetPlayerSeat(this);

        yield return new WaitForSeconds(delay);

        UIController.instance.TurnOnBettingPanel(true);

        bool bettingCompleted = false;
        UIController.instance.onBettingCompleted.AddListener(() => bettingCompleted = true);

        yield return new WaitUntil(() => bettingCompleted);

        UIController.instance.TurnOnBettingPanel(false);

        BettingAnimation();
    }

    void BettingAnimation()
    {
        int index = 0;
        switch (Bet_Amount)
        {
            case 10:
                index = 0;
                break;
            case 20:
                index = 1;
                break;
            case 30:
                index = 2;
                break;
            case 40:
                index = 3;
                break;
            case 50:
                index = 4;
                break;
            case 60:
                index = 5;
                break;
        }

        GameObject chip = Instantiate(chips[index], this.transform);
        chip.name = "Chip";
        chip.transform.localPosition += chip.transform.localPosition;
        
    }

    public void DestroyChip()
    {
        if(this.transform.Find("Chip"))
            Destroy(this.transform.Find("Chip").gameObject);
    }

}
