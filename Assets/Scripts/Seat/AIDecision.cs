using System.Collections;
using UnityEngine;

public class AIDecision : MonoBehaviour
{
    Seat seat;
    int currentTurn;

    private void Start()
    {
        seat = GetComponent<Seat>();
    }

    public IEnumerator Decision(float delay)
    {
        currentTurn = GameController.instance.Turn;

        while(seat.Card_Sum < Helper.MAXSUM || seat.Card_Sum <= Helper.CONDITION)
        {
            // 카드 추가 받기
            CardPooling.instance.Phase(transform.GetChild(0), currentTurn);
            seat.UpdateSum(transform.GetChild(0).GetChild(currentTurn).GetComponent<Card>().card_Number);
            currentTurn++;
            yield return new WaitForSeconds(delay);
        }
        //Debug.Log($"Name : {seat.gameObject.name} / Sum : {seat.Card_Sum}");
    }
}
