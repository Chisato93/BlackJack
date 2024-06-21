using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region -SINGLETON
    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
    #endregion

    [field: SerializeField] public int Heart { get; set; } = 5;
    [field: SerializeField] public int Gold { get; set; } = 100;

    private void Start()
    {
        DataManager.instance.LoadDataFun();
    }
    public void Init()
    {
        Gold = 100;
        Heart = 5;
        UIController.instance.SetExchange();
    }

    public void ChangeHeartToGold()
    {
        if (Heart - 1 < 0) return;
        Heart--;
        Gold += Helper.BuyGold;
    }
    public void ChangeGoldToHeart()
    {
        if(Gold  - Helper.BuyHeart < 0) return;
        Gold -= Helper.BuyHeart;
        Heart++;
        GameWin();
    }

    public bool EnughtGold()
    {
        if (Gold > 10) return true;
        else return false;
    }

    public bool GameOver()
    {
        if(Heart <= 0 && Gold <= 10)
        {
            GameController.instance.Flow = GameFlow.LAST_TURN;
            Time.timeScale = 0;
            UIController.instance.OpenGameReuslt(false);
            return true;
        }

        return false;
    }

    public bool GameWin()
    {
        if(Heart >= 100 || (Heart == 99 && Gold >= Helper.BuyHeart))
        {
            GameController.instance.Flow = GameFlow.LAST_TURN;
            Time.timeScale = 0;
            UIController.instance.OpenGameReuslt(true);
            return true;
        }

        return false;
    }


}
