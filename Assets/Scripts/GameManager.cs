using System.Collections.Generic;
using Unity.VisualScripting;
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

    public int Heart { get; set; } = 5;
    public int Gold { get; set; } = 100;

    public void ChangeHeartToGold()
    {
        if (Heart - 1 < 0) return;
        Heart--;
        Gold += Helper.BuyGold;
    }
    public void ChangeGoldToHeart()
    {
        if(Gold  - 1 < 0) return;
        Gold -= Helper.BuyHeart;
        Heart++;
    }

    public bool EnughtGold()
    {
        if (Gold >= 10) return true;
        else return false;
    }

    public bool GameOver()
    {
        if(Heart < 0 && Gold <= 0)
        {
            GameController.instance.Flow = GameFlow.LAST_TURN;
            Time.timeScale = 0;
            Debug.Log("Die");
            return true;
        }

        return false;
    }

    public bool GameWin()
    {
        if(Heart >= 100)
        {
            GameController.instance.Flow = GameFlow.LAST_TURN;
            Time.timeScale = 0;
            Debug.Log("Win");
            return true;
        }

        return false;
    }

    #region 임시테스트
    public List<GameObject> Flows;


    public void NextTurn()
    {
        int overFlowCheck = (int)GameController.instance.Flow % (int)GameFlow.LAST_TURN;
        GameController.instance.Flow = (GameFlow)(overFlowCheck);

        if (overFlowCheck - 1 < 0)
        {
            Flows[overFlowCheck + (int)GameFlow.LAST_TURN - 1].gameObject.SetActive(false);
            Flows[overFlowCheck].gameObject.SetActive(true);
        }
        else
        {
            Flows[overFlowCheck - 1].gameObject.SetActive(false);
            Flows[overFlowCheck].gameObject.SetActive(true);
        }
    }
    #endregion
}
