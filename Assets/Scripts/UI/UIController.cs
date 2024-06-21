using System;
using System.Collections;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    private void Awake()
    {
        instance = this;
    }

    public GameObject ReadyButton;
    public GameObject BettingPanel;
    public GameObject SelectActionPanel;
    public GameObject SelectAceCard;
    public GameObject NoticePanel;
    public GameObject ResultPanel;
    public GameObject CurrentPanel;
    public GameObject ExchangePanel;
    public GameObject GameResultPanel;
    public TMP_Text bettingText;
    public TMP_Text resultText;
    public TMP_Text heartText;
    public TMP_Text goldText;
    public TMP_Text ex_heartText;
    public TMP_Text ex_goldText;
    public TMP_Text gr_resultText;
    public TMP_Text saveText;
    public UnityEvent onBettingCompleted;
    public UnityEvent onTurnFinished;
    private PlayerSeat currentPlayerSeat;
    private string r_text = "";

    //public IEnumerator ShowSaveDataText()
    //{
    //    string txt = "Save";
    //    for(int i = 0; i < 3; i ++)
    //    {
    //        saveText.text = txt;
    //        txt += "..";
    //        yield return null;
    //    }

    //    txt = "";
    //    saveText.text = txt;
    //}

    public void OpenGameReuslt(bool win)
    {
        GameResultPanel.SetActive(true);
        if (win) gr_resultText.text = "Finally!! You Can Escape there!! \n Game Clear !!";
        else gr_resultText.text = "Sadly, You are Dead !! \n Game Over !!";
    }

    public void SetExchange()
    {
        heartText.text = "Herat : " + GameManager.instance.Heart.ToString();
        goldText.text = "Gold : " + GameManager.instance.Gold.ToString();
        ex_heartText.text = GameManager.instance.Heart.ToString();
        ex_goldText.text = GameManager.instance.Gold.ToString();
    }
    public void OpenExchangePanel()
    {
        ExchangePanel.SetActive(true);
        SetExchange();
    }
    public void OpenCurrencyPanel()
    {
        CurrentPanel.SetActive(true);
        SetExchange();
    }
    public void CloseExchangePanel()
    {
        ExchangePanel.SetActive(false);
        GameController.instance.NextStep();
    }
    public void CloseCurrencyPanel()
    {
        CurrentPanel.SetActive(false);
    }
    public void SetText(string text)
    {
        r_text += text;
    }

    public void ShowResultPanel()
    {
        ResultPanel.SetActive(true);
        Debug.Log(r_text);
        resultText.text = r_text;
    }

    public void CloseReusltPanel()
    {
        r_text = "";
        resultText.text = r_text;
        ResultPanel.SetActive(false);
        GameController.instance.NextStep();
        
    }

    public IEnumerator SetNoticePanel(string text)
    {
        NoticePanel.SetActive(true);
        NoticePanel.GetComponentInChildren<TMP_Text>().text = text;
        yield return new WaitForSeconds(0.5f);
        NoticePanel.GetComponentInChildren<TMP_Text>().text = "";
        NoticePanel.SetActive(false);
        yield return null;
    }

    public void TurnOnSelectAceCardPanel(bool isActive, Card card)
    {
        SelectAceCard.SetActive(isActive);

        if (isActive)
        {
            SelectAceCard acecard = SelectAceCard.GetComponent<SelectAceCard>();
            acecard.currentAceCard = card;
        }    
    }

    public void TurnOnSelectActionPanel(bool isActive)
    {
        SelectActionPanel.SetActive(isActive);
        if(isActive)
        {
            SelectActionPanelScript select = SelectActionPanel.GetComponent<SelectActionPanelScript>();
            select.SeatNumber.text = $"Seat : {currentPlayerSeat.gameObject.name.Substring(currentPlayerSeat.gameObject.name.Length - 1)}";
            if (currentPlayerSeat.CanSplit()) select.splitBtn.interactable = true;
            select.SetImgBundle();
        }
    }    

    public void TurnOnReadyButton(bool isActive)
    {
        ReadyButton.SetActive(isActive);
    }

    public void TurnOnBettingPanel(bool isActive)
    {
        if (isActive) bettingText.text = $"Seat : {currentPlayerSeat.gameObject.name.Substring(currentPlayerSeat.gameObject.name.Length-1)}\nChoose Betting Amount......";
        BettingPanel.SetActive(isActive);
    }

    public void SetPlayerSeat(PlayerSeat curPlayerSeat)
    {
        currentPlayerSeat = curPlayerSeat;
    }

    public PlayerSeat GetCurrentPlayerSeat()
    {
        return currentPlayerSeat;
    }
}
