using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }

    public GameFlow Flow { get; set; }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Flow = GameFlow.SELECT_SEAT;
        UIController.instance.TurnOnReadyButton(true);
    }

    public void NextStep()
    {
        Flow++;

        // 임시로 보여주기 위해서
        GameManager.instance.NextTurn();
        
        switch(Flow)
        {
            case GameFlow.SELECT_SEAT:
                Init();
                break;
            case GameFlow.BETTING:
                StartCoroutine(BettingTurn());
                break;
            case GameFlow.CARD_DISTRIBUTION:
                break;
            case GameFlow.CHOICE:
                break;
            case GameFlow.CARD_COMPARE:
                break;
            case GameFlow.PRIZE:
                break;

        }
    }

    private IEnumerator BettingTurn()
    {
        Seats seatList = FindObjectOfType<Seats>();
        foreach (PlayerSeat seat in seatList.seats)
        {
            if (!seat.isEmptySeat)
            {
                yield return StartCoroutine(seat.SetBettingAmount());
            }
        }

        NextStep();
    }
}
