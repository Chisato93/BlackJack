using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }

    public GameFlow Flow { get; set; }

    CameraController camController;
    DealerSeat dealerSeat;
    Seats seatList;

    public int Turn { get; set; }
    float delayTime = .5f;

    private void Start()
    {
        camController = Camera.main.GetComponent<CameraController>();
        dealerSeat = FindObjectOfType<DealerSeat>();
        Init();
    }
    private void Init()
    {
        Turn = 0;
        seatList = FindObjectOfType<Seats>();
        foreach (PlayerSeat seat in seatList.seats)
        {
            while (seat.transform.GetChild(0).childCount > 0)
                CardPooling.instance.ReturnCard(seat.transform.GetChild(0).GetChild(0).gameObject);
            seat.InitCardDeck();
            seat.SetText(seat.isEmptySeat);
        }
        Flow = GameFlow.SELECT_SEAT;
        dealerSeat.SetActiveDummyCard(true);
        dealerSeat.InitCardDeck();
        while (dealerSeat.transform.GetChild(0).childCount > 0)
            CardPooling.instance.ReturnCard(dealerSeat.transform.GetChild(0).GetChild(0).gameObject);
        UIController.instance.TurnOnReadyButton(true);
        CameraChange((int)CamerasNumber.CAM_NORMAL);
    }

    public void NextStep()
    {
        Flow++;
        int flow = (int)Flow % (int)GameFlow.TOTAL;
        Flow = (GameFlow)flow;

        // 임시로 보여주기 위해서
        GameManager.instance.NextTurn();

        switch (Flow)
        {
            case GameFlow.SELECT_SEAT:
                Init();
                break;
            case GameFlow.BETTING:
                SelectSeatComplete();
                StartCoroutine(BettingTurn());
                break;
            case GameFlow.CARD_DISTRIBUTION:
                StartCoroutine(CardDistribue());
                break;
            case GameFlow.CHOICE:
                StartCoroutine(TakeOrPass());
                break;
            case GameFlow.CARD_COMPARE:
                Compare();
                break;
            case GameFlow.TOTAL:
                NextStep();
                break;
        }
    }

    private void Compare()
    {
        Flow = GameFlow.CARD_COMPARE;
        int dealerSum = dealerSeat.Card_Sum;
        if (dealerSum == 21) NextStep();
        else
        {
            foreach (PlayerSeat seat in seatList.seats)
            {
                if (!seat.isBust && !seat.isEmptySeat)
                {
                    if (seat.Card_Sum > dealerSum)
                    {
                        if (seat.isBlackJack()) GameManager.instance.Gold += (int)(seat.Bet_Amount * WinRate.BLACKJACK);
                        GameManager.instance.Gold += (int)(seat.Bet_Amount * WinRate.NORMALWIN);
                    }
                    else
                    {
                        NextStep();
                    }
                }
            }
        }
    }

    private void SelectSeatComplete()
    {
        CameraChange((int)CamerasNumber.CAM_TABLE);

        dealerSeat.SetActiveDummyCard(false);
        foreach (PlayerSeat seat in seatList.seats)
        {
            if (seat.isEmptySeat)
                seat.AddComponent<AIDecision>();
        }
        dealerSeat.AddComponent<AIDecision>();
    }

    private void CameraChange(int cameranumber)
    {
        camController.SetCamera(cameranumber);
    }

    private IEnumerator TakeOrPass()
    {
        if (dealerSeat.isBlackJack()) Compare();
        else
        {
            foreach (PlayerSeat seat in seatList.seats)
            {
                if (seat.isBlackJack()) continue;

                if (seat.isEmptySeat)
                {
                    yield return StartCoroutine(seat.GetComponent<AIDecision>().Decision(delayTime));
                }
                if (!seat.isEmptySeat)
                {
                    if (!seat.HaveAceCard().isSelected)
                        UIController.instance.TurnOnSelectAceCardPanel(true, seat.HaveAceCard());
                    bool turnFinished = false;
                    UIController.instance.SetPlayerSeat(seat);
                    UIController.instance.TurnOnSelectActionPanel(true);
                    UIController.instance.onTurnFinished.AddListener(() => turnFinished = true);
                    yield return new WaitUntil(() => turnFinished);
                }
            }
            if (!dealerSeat.isBlackJack())
                yield return StartCoroutine(dealerSeat.GetComponent<AIDecision>().Decision(delayTime));

            NextStep();
        }
    }

    IEnumerator CardDistribue()
    {
        while (Turn < 2)
        {
            foreach (PlayerSeat seat in seatList.seats)
            {
                CardPooling.instance.Phase(seat.transform.GetChild(0), Turn);
                seat.AddCard(seat.transform.GetChild(0).GetChild(Turn).GetComponent<Card>());
                yield return new WaitForSeconds(delayTime);
            }
            CardPooling.instance.Phase(dealerSeat.transform.GetChild(0), Turn);
            dealerSeat.AddCard(dealerSeat.transform.GetChild(0).GetChild(Turn).GetComponent<Card>());
            Turn++;
            yield return new WaitForSeconds(delayTime);
        }


        NextStep();
    }

    private IEnumerator BettingTurn()
    {
        foreach (PlayerSeat seat in seatList.seats)
        {
            if (!seat.isEmptySeat)
            {
                yield return StartCoroutine(seat.SetBettingAmount(delayTime));
            }
        }
        yield return new WaitForSeconds(delayTime);
        NextStep();
    }
}
