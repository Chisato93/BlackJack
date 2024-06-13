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

    void ChangeHeartToGold()
    {
        Heart--;
        Gold += Helper.BuyGold;
    }
    void ChangeGoldToHeart()
    {
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
            GameController.instance.Flow = GameFlow.NONE;
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
            GameController.instance.Flow = GameFlow.NONE;
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
        int overFlowCheck = (int)GameController.instance.Flow % (int)GameFlow.NONE;
        GameController.instance.Flow = (GameFlow)(overFlowCheck);

        if (overFlowCheck - 1 < 0)
        {
            Flows[overFlowCheck + (int)GameFlow.NONE - 1].gameObject.SetActive(false);
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
