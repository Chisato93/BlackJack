using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerSeat : Seat
{
    public bool isEmptySeat = true;
    public int Bet_Amount { get; set; }

    public List<GameObject> chips;
    private float lerp_Speed = 1f;

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
         
        // 딜러 자리 말고 그냥 현재 자리에서 오른쪽에 넣어야 카드가 보일듯
        Vector3 originalPos = chip.transform.localPosition;
        Vector3 targetDir = originalPos + new Vector3(.05f,0,0);
        // Helper.SmoothMove(시작, 도착, 속도) 함수로 구현 가능

        yield return StartCoroutine(Helper.SmoothMove(originalPos, targetDir, lerp_Speed));
        
    }

}
