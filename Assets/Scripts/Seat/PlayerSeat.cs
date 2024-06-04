using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerSeat : Seat
{
    public bool isEmptySeat = true;
    public int Bet_Amount { get; set; }

    public List<GameObject> chips;
    private float lerp_Speed = .05f;

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
        
        Vector3 originalPos = chip.transform.localPosition;
        Vector3 targetTransform = FindObjectOfType<DealerSeat>().transform.position;
        Vector3 targetDir = (targetTransform - transform.localPosition).normalized;
        Vector3 targetPos = originalPos + targetDir;

        float dist = Vector3.Distance(originalPos, targetPos);
        while (dist >= .1f)
        {
            chip.transform.localPosition = Vector3.Lerp(originalPos, targetPos, lerp_Speed);
            yield return null;
        }

        // Ensure chip is at the target position
        chip.transform.localPosition = targetPos;
    }

}
