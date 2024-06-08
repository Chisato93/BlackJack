using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerSeat : Seat, I_SmoothMove
{
    public bool isEmptySeat = true;
    public int Bet_Amount { get; set; }

    public List<GameObject> chips;
    private float lerp_Speed = 1f;

    public IEnumerator SetBettingAmount(float delay)
    {
        UIController.instance.SetPlayerSeat(this);

        yield return new WaitForSeconds(delay);

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
        Vector3 targetDir = originalPos + new Vector3(.05f,0,0);

        yield return StartCoroutine(SmoothMove(originalPos, targetDir, lerp_Speed));
        
    }

    public IEnumerator SmoothMove(Vector3 startPos, Vector3 target, float lerp_Speed)
    {
        while (Vector3.Distance(startPos, target) > 0.001f)
        {
            startPos = Vector3.Lerp(startPos, target, lerp_Speed * Time.deltaTime);
            yield return null;
        }

        startPos = target;
    }
}
