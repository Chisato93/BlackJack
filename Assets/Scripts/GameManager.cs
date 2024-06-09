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

    public int Gold { get; set; }

    #region 임시테스트
    public List<GameObject> Flows;


    public void NextTurn()
    {
        int overFlowCheck = (int)GameController.instance.Flow % (int)GameFlow.TOTAL;
        GameController.instance.Flow = (GameFlow)(overFlowCheck);

        if (overFlowCheck - 1 < 0)
        {
            Flows[overFlowCheck + (int)GameFlow.TOTAL - 1].gameObject.SetActive(false);
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
