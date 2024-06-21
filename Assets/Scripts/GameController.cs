using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private void Awake()
    {
        instance = this;
    }

    public GameFlow Flow { get; set; }
    Seat CurrentTurn { get; set; }


    CameraController camController;
    DealerSeat dealerSeat;
    Seats seatList;
    JoinChip[] joinChips;

    public int Turn { get; set; }
    float delayTime = .5f;

    private void Start()
    {
        camController = Camera.main.GetComponent<CameraController>();
        joinChips = FindObjectsOfType<JoinChip>();
        dealerSeat = FindObjectOfType<DealerSeat>();

        Init();

        GameFlow();
    }

    private void Init()
    {
        Turn = 0;
        if(seatList == null)
            seatList = FindObjectOfType<Seats>();
        foreach (PlayerSeat seat in seatList.seats)
        {
            while (seat.transform.GetChild(0).childCount > 0)
                CardPooling.instance.ReturnCard(seat.transform.GetChild(0).GetChild(0).gameObject);
            seat.InitCardDeck();
            seat.DestroyChip();
            seat.isEmptySeat = true;
            seat.SetText(seat.isEmptySeat);
        }
        dealerSeat.SetActiveDummyCard(true);
        dealerSeat.InitCardDeck();
        while (dealerSeat.transform.GetChild(0).childCount > 0)
            CardPooling.instance.ReturnCard(dealerSeat.transform.GetChild(0).GetChild(0).gameObject);
        foreach (JoinChip joinChip in joinChips)
        {
            joinChip.Init();
        }
        UIController.instance.TurnOnReadyButton(true);
        CameraChange((int)CamerasNumber.CAM_NORMAL);

    }

    public void NextStep()
    {
        Flow++;
        int flow = (int)Flow % ((int)global::GameFlow.LAST_TURN + 1);
        Flow = (GameFlow)flow;
        GameFlow();
    }

    private void GameFlow()
    {
        switch (Flow)
        {
            case global::GameFlow.BUYGOLD:
                ExchangeTurn();
                break;
            case global::GameFlow.SELECT_SEAT:
                Init();
                break;
            case global::GameFlow.BETTING:
                SelectSeatComplete();
                StartCoroutine(BettingTurn());
                break;
            case global::GameFlow.CARD_DISTRIBUTION:
                StartCoroutine(CardDistribue());
                break;
            case global::GameFlow.CHOICE:
                StartCoroutine(TakeOrPass());
                break;
            case global::GameFlow.CARD_COMPARE:
                Compare();
                break;
            case global::GameFlow.LAST_TURN:
                UIController.instance.ShowResultPanel();
                UIController.instance.OpenCurrencyPanel();
                break;
        }
    }

    private void ExchangeTurn()
    {
        if (GameManager.instance.GameOver() || GameManager.instance.GameWin()) return;
        else
        {
            if (GameManager.instance.EnughtGold())
            {
                NextStep();
            }
            else
            {
                UIController.instance.OpenExchangePanel();
            }
        }
    }

    private void Compare()
    {
        if(Flow != global::GameFlow.CARD_COMPARE)
            Flow = global::GameFlow.CARD_COMPARE;
        foreach(PlayerSeat seat in seatList.seats)
        {
            if(!seat.isEmptySeat && seat.haveDoubleDownCard)
            {
                seat.Card_Deck[seat.Card_Deck.Count - 1].transform.rotation = Quaternion.Euler(Vector3.zero);
                seat.sumText.text = seat.Card_Sum.ToString();
            }
        }
        int dealerSum = dealerSeat.Card_Sum;

        // µô·¯°¡ ºí·¢ÀèÀÏ¶§
        if (dealerSum == Helper.MAXSUM)
        {
            foreach (PlayerSeat seat in seatList.seats)
            {
                if (!seat.isEmptySeat)
                {
                    if(seat.Card_Sum == Helper.MAXSUM)
                    {
                        UIController.instance.SetText($" Player {seat.gameObject.name.Substring(seat.gameObject.name.Length - 1)} is Draw\n");
                    }
                    else
                    {
                        UIController.instance.SetText($" Player {seat.gameObject.name.Substring(seat.gameObject.name.Length - 1)} is Lose\n");
                    }
                }
            }
        }
        // µô·¯°¡ ¹ö½ºÆ®ÀÏ¶§
        else if(dealerSum > Helper.MAXSUM)
        {
            foreach (PlayerSeat seat in seatList.seats)
            {
                if (!seat.isEmptySeat)
                {
                    if (!seat.isBust)
                    {
                        if (seat.Card_Sum == Helper.MAXSUM)
                        {
                            UIController.instance.SetText($" Player  {seat.gameObject.name.Substring(seat.gameObject.name.Length - 1)} is BLACK JACK\n");
                            GameManager.instance.Gold += (int)(seat.Bet_Amount * 2.9);
                        }
                        else if(seat.Card_Sum < Helper.MAXSUM)
                        {
                            UIController.instance.SetText($" Player  {seat.gameObject.name.Substring(seat.gameObject.name.Length - 1)} is Win\n");
                            GameManager.instance.Gold += (int)(seat.Bet_Amount * 1.9f);
                        }
                        else
                            UIController.instance.SetText($" Player   {seat.gameObject.name.Substring(seat.gameObject.name.Length - 1)} is Lose\n");
                    }
                }
            }
        }
        // µô·¯°¡ ¹ö½ºÆ®³ª ºí·¢ÀèÀÌ ¾Æ´Ò¶§
        else
        {
            foreach (PlayerSeat seat in seatList.seats)
            {
                if (!seat.isEmptySeat)
                {
                    if(seat.isBust)
                    {
                        UIController.instance.SetText($" Player   {seat.gameObject.name.Substring(seat.gameObject.name.Length - 1)} is Lose\n");
                        continue;
                    }
                    if(seat.Card_Deck.Count == 5 && seat.Card_Sum < Helper.MAXSUM)
                    {
                        UIController.instance.SetText($" Player   {seat.gameObject.name.Substring(seat.gameObject.name.Length - 1)} is Win\n");
                        GameManager.instance.Gold += (int)(seat.Bet_Amount * 1.9f);
                        continue;
                    }
                    if(seat.Card_Sum == Helper.MAXSUM)
                    {
                        UIController.instance.SetText($" Player  {seat.gameObject.name.Substring(seat.gameObject.name.Length - 1)} is BLACK JACK\n");
                        GameManager.instance.Gold += (int)(seat.Bet_Amount * 2.9);
                    }
                    if (seat.Card_Sum > dealerSum /*&& seat.Card_Deck.Count == 5*/)
                    {
                        UIController.instance.SetText($" Player   {seat.gameObject.name.Substring(seat.gameObject.name.Length - 1)} is Win\n");
                        GameManager.instance.Gold += (int)(seat.Bet_Amount * 1.9f);
                    }
                    else if (seat.Card_Sum == dealerSum)
                        UIController.instance.SetText($" Player   {seat.gameObject.name.Substring(seat.gameObject.name.Length - 1)} is Draw\n");
                    else
                            UIController.instance.SetText($" Player   {seat.gameObject.name.Substring(seat.gameObject.name.Length - 1)} is Lose\n");
                }
            }
        }
        DataManager.instance.SaveDataFun();

        NextStep();
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
        if (dealerSeat.isBlackJack())
        {
            NextStep();
            yield break;
        }
        else
        {
            foreach (PlayerSeat seat in seatList.seats)
            {
                if (seat.isBlackJack()) continue;
                if (seat.Card_Deck.Count == 2 && seat.HaveAceCard() && seat.Card_Sum == Helper.MAXSUM)
                {
                    seat.sumText.text = Helper.MAXSUM.ToString();
                    continue;
                }

                if (seat.isEmptySeat)
                {
                    yield return StartCoroutine(seat.GetComponent<AIDecision>().Decision(delayTime));
                }
                if (!seat.isEmptySeat)
                {
                    if (seat.HaveAceCard() != null && !seat.HaveAceCard().isSelected)
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
        int tempGold = 0;
        foreach (PlayerSeat seat in seatList.seats)
        {
            if (GameManager.instance.Gold < 10)
            {
                if(!seat.isEmptySeat)
                {
                    tempGold += 10;
                    seat.isEmptySeat = true;
                    seat.SetText(seat.isEmptySeat);
                    seat.AddComponent<AIDecision>();
                    continue;
                }
            }
            else
            {
                if (!seat.isEmptySeat)
                {
                    yield return StartCoroutine(seat.SetBettingAmount(delayTime));
                }
            }
        }
        GameManager.instance.Gold += tempGold;
        yield return new WaitForSeconds(delayTime);
        UIController.instance.CloseCurrencyPanel();
        DataManager.instance.SaveDataFun();
        NextStep();
    }
}
