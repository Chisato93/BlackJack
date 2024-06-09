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

        if(seat.Card_Deck.Count >= Helper.MAXCARDCOUNT) yield break;

        while(seat.Card_Sum <= Helper.CONDITION)
        {
            // 카드 추가 받기
            CardPooling.instance.Phase(transform.GetChild(0), currentTurn);
            seat.AddCard(transform.GetChild(0).GetChild(currentTurn).GetComponent<Card>());
            currentTurn++;
            yield return new WaitForSeconds(delay);
        }
    }
}
