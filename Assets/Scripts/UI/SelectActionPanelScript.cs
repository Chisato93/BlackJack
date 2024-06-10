using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectActionPanelScript : MonoBehaviour
{
    public Button splitBtn;
    public TMP_Text SeatNumber;
    public TMP_Text SeatScore;

    int currentTurn;
    Vector3 flip = new Vector3(0, 0, 180);

    public List<Image> cardImgs;
    public Transform cardImgBundle;

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
        if(currentSeat.HaveAceCard() != null && currentSeat.Card_Sum > Helper.MAXSUM) // 여기에 한번만 되게 만들어줘야함)
        {
            currentSeat.HaveAceCard().card_Number = Helper.ACEONE;
            currentSeat.Card_Sum -= Helper.ACEELEVEN + Helper.ACEONE;
        }
        if (cur.GetChild(currentTurn).GetComponent<Card>().IsAceCard() && (currentSeat.Card_Sum + Helper.ACEELEVEN <= Helper.MAXSUM))
            UIController.instance.TurnOnSelectAceCardPanel(true, cur.GetChild(currentTurn).GetComponent<Card>());
        SetImgBundle(currentTurn, cur.GetChild(currentTurn).GetComponent<Card>());

        if (currentSeat.Card_Sum == Helper.MAXSUM || currentSeat.Card_Deck.Count >= Helper.MAXCARDCOUNT) TurnFinish();
        if (IsBust(currentSeat.Card_Sum))
        {
            currentSeat.isBust = true;
            StartCoroutine(ShowBustPanel());
        }
    }

    private IEnumerator ShowBustPanel()
    {
        UIController.instance.bustPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        UIController.instance.bustPanel.SetActive(false);
        yield return null;
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
        UIController.instance.onTurnFinished?.Invoke();
        UIController.instance.TurnOnSelectActionPanel(false);
    }

}
