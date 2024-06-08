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
        Flow = GameFlow.SELECT_SEAT;
        dealerSeat.SetActiveDummyCard(true);
        UIController.instance.TurnOnReadyButton(true);
        CameraChange((int)CamerasNumber.CAM_NORMAL);
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
                break;
            case GameFlow.PRIZE:
                break;

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
        foreach (PlayerSeat seat in seatList.seats)
        {
            if (seat.isEmptySeat)
            {
               yield return StartCoroutine(seat.GetComponent<AIDecision>().Decision(delayTime));
            }
            if (!seat.isEmptySeat)
            {
                UIController.instance.TurnOnSelectActionPanel(true);
                yield return null;
            }
        }
        yield return StartCoroutine(dealerSeat.GetComponent<AIDecision>().Decision(delayTime));
    }

    IEnumerator CardDistribue()
    {
        while(Turn < 2)
        {
            foreach (PlayerSeat seat in seatList.seats)
            {
                CardPooling.instance.Phase(seat.transform.GetChild(0), Turn);
                seat.UpdateSum(seat.transform.GetChild(0).GetChild(Turn).GetComponent<Card>().card_Number);
                yield return new WaitForSeconds(delayTime);
            }
            CardPooling.instance.Phase(dealerSeat.transform.GetChild(0), Turn);
            dealerSeat.UpdateSum(dealerSeat.transform.GetChild(0).GetChild(Turn).GetComponent<Card>().card_Number);
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
