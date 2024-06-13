using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectActionPanelScript : MonoBehaviour
{
    public Action Refresh;

    public List<Button> buttons;
    const int HitBtn = 0, StayBtn = 1, DoubleDownBtn = 2;
    public Button splitBtn;
    public TMP_Text SeatNumber;
    public TMP_Text SeatScore;

    int currentTurn;
    Vector3 flip = new Vector3(0, 0, 180);

    public List<Image> cardImgs;
    public Transform cardImgBundle;

    private void Start()
    { 
        Refresh += SetImgBundle;
    }

    private void OnEnable()
    {
        ButtonOnOff(true);
    }

    private void ButtonOnOff(bool isActive)
    {
        foreach(Button button in buttons)
        {
            button.interactable = isActive;
        }
    }

    private string GetCardInfo(Card card)
    {
        string shape = card.GetCardShape();
        string cardNum = card.cardRealNumber.ToString("00");
        return shape+cardNum;
    }
    public void SetImgBundle()
    {
        InitImage();

        List<Card> currentCard = UIController.instance.GetCurrentPlayerSeat().Card_Deck;
        for(int i = 0; i < currentCard.Count; i++)
        {
            string cardInfo = GetCardInfo(currentCard[i]);
            cardImgs[i].sprite = Resources.Load<Sprite>("PlayingCards/" + cardInfo);
        }

        SeatScore.text = $"Sum : {UIController.instance.GetCurrentPlayerSeat().Card_Sum}";
    }public void SetImgBundle(bool isAce)
    {
        if(isAce)
        {
            List<Card> currentCard = UIController.instance.GetCurrentPlayerSeat().Card_Deck;
            for (int i = 0; i < currentCard.Count; i++)
            {
                string cardInfo = GetCardInfo(currentCard[i]);
                cardImgs[i].sprite = Resources.Load<Sprite>("PlayingCards/" + cardInfo);
            }

            SeatScore.text = $"Sum : {UIController.instance.GetCurrentPlayerSeat().Card_Sum}";
        }
    }
    public void SetImgBundle(int phase, Card card)
    {
        string cardInfo = GetCardInfo(card);
        cardImgs[phase].sprite = Resources.Load<Sprite>("PlayingCards/" + cardInfo);
        SeatScore.text = $"Sum : {UIController.instance.GetCurrentPlayerSeat().Card_Sum}";
    }
    public void SetImgBundle(int phase, Card card, bool doubledown)
    {
        if(!doubledown)
        {
            string cardInfo = GetCardInfo(card);
            cardImgs[phase].sprite = Resources.Load<Sprite>("PlayingCards/" + cardInfo);
        }
        else
        {
            cardImgs[phase].sprite = Resources.Load<Sprite>("PlayingCards/BackColor_Red");
        }
        SeatScore.text = $"Sum : {UIController.instance.GetCurrentPlayerSeat().Card_Sum}";
    }

    private void InitImage()
    {
        foreach(Image img in cardImgs)
        {
            img.sprite = Resources.Load<Sprite>("PlayingCards/BackColor_Black");
            SeatScore.text = $"Sum : {UIController.instance.GetCurrentPlayerSeat().Card_Sum}";
        }
    }

    public void Hit()
    {
        PlayerSeat currentSeat = UIController.instance.GetCurrentPlayerSeat();

        currentTurn = currentSeat.Card_Deck.Count;
        Transform cur = currentSeat.transform.GetChild(0);
        CardPooling.instance.Phase(cur, currentTurn);
        currentSeat.AddCard(cur.GetChild(currentTurn).GetComponent<Card>());

        if (currentSeat.HaveAceCard() != null && currentSeat.HaveAceCard().card_Number == Helper.ACEELEVEN && currentSeat.Card_Sum > Helper.MAXSUM)
        {
            currentSeat.HaveAceCard().card_Number = Helper.ACEONE;
            currentSeat.Card_Sum -= Helper.ACEELEVEN - currentSeat.HaveAceCard().card_Number;
        }

        if (cur.GetChild(currentTurn).GetComponent<Card>().IsAceCard() && (currentSeat.Card_Sum + Helper.ACEELEVEN <= Helper.MAXSUM))
            UIController.instance.TurnOnSelectAceCardPanel(true, cur.GetChild(currentTurn).GetComponent<Card>());

        SetImgBundle(currentTurn, cur.GetChild(currentTurn).GetComponent<Card>());

        StartCoroutine(HandleEndOfTurn(currentSeat));
    }

    private IEnumerator HandleEndOfTurn(PlayerSeat currentSeat)
    {
        if (currentSeat.Card_Sum == Helper.MAXSUM)
        {
            yield return StartCoroutine(ShowNoticePanel(Helper.BLACKJACK));
        }
        else if (currentSeat.Card_Deck.Count >= Helper.MAXCARDCOUNT)
        {
            if (IsBust(currentSeat.Card_Sum))
            {
                currentSeat.isBust = true;
                yield return StartCoroutine(ShowNoticePanel(Helper.BUST));
            }
            else
            {
                TurnFinish();
            }
        }
        else if (IsBust(currentSeat.Card_Sum))
        {
            currentSeat.isBust = true;
            yield return StartCoroutine(ShowNoticePanel(Helper.BUST));
        }
    }


    private IEnumerator ShowNoticePanel(string txt)
    {
        ButtonOnOff(false);
        yield return StartCoroutine(UIController.instance.SetNoticePanel(txt));
        TurnFinish();
    }

    public void Stay()
    {
        TurnFinish();
    }

    public void DoubleDown()
    {
        PlayerSeat currentSeat = UIController.instance.GetCurrentPlayerSeat();
        currentTurn = currentSeat.Card_Deck.Count;
        Transform cur = currentSeat.transform.GetChild(0);
        CardPooling.instance.Phase(cur, currentTurn);
        cur.GetChild(currentTurn).transform.rotation = Quaternion.Euler(flip);
        currentSeat.haveDoubleDownCard = true;
        currentSeat.AddCard(cur.GetChild(currentTurn).GetComponent<Card>(), true);
        SetImgBundle(currentTurn, cur.GetChild(currentTurn).GetComponent<Card>(), true);
        TurnFinish();
    }

    public void Split()
    {

    }

    private bool IsBust(int sum)
    {
        if (sum > Helper.MAXSUM)
        {
            return true;
        }
        return false;
    }

    private void TurnFinish()
    {
        ButtonOnOff(false);
        UIController.instance.onTurnFinished?.Invoke();
        UIController.instance.TurnOnSelectActionPanel(false);
    }

}
