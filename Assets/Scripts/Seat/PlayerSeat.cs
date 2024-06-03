using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerSeat : Seat
{
    public bool isEmptySeat = true;
    public int Bet_Amount { get; set; }

    public List<GameObject> chips;
    private float lerp_Speed = .5f;

    public IEnumerator SetBettingAmount()
    {
        UIController.instance.SetPlayerSeat(this);
        UIController.instance.TurnOnBettingPanel(true);

        bool bettingCompleted = false;
        UIController.instance.onBettingCompleted.AddListener(() => bettingCompleted = true);

        yield return new WaitUntil(() => bettingCompleted);

        UIController.instance.TurnOnBettingPanel(false);

        StartCoroutine(BettingAnimation());
    }

    IEnumerator BettingAnimation()
    {
        int index = 0;
        switch (Bet_Amount)
        {
            case 1:
                index = 0;
                break;
            case 5:
                index = 1;
                break;
            case 10:
                index = 2;
                break;
            case 50:
                index = 3;
                break;
            case 100:
                index = 4;
                break;
            case 500:
                index = 5;
                break;
        }

        GameObject chip = Instantiate(chips[index], this.transform);

        Vector3 originalPos = transform.localPosition;
        Vector3 targetPos = originalPos + new Vector3(0, 0, 0.3f);

        float dist = Vector3.Distance(originalPos, targetPos);
        while (dist >= .1f)
        {
            transform.localPosition = Vector3.Lerp(originalPos, targetPos, lerp_Speed);
            yield return null;
        }

        // Ensure chip is at the target position
        transform.localPosition = targetPos;
    }

}
