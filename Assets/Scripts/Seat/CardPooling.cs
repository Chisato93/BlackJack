using System.Collections.Generic;
using UnityEngine;

public class CardPooling : MonoBehaviour
{
    public static CardPooling instance;

    List<Card> Card_Package = new List<Card>();

    public List<GameObject> cardList;
    const int deckCount = 20;

    private void Awake()
    {
        CreateCardPackage();

        instance = this;

        CreatePool();
    }
    const int packageCount = 3;
    const int cardCount = 13;
    const int cardShapeCount = 4;
    private void CreateCardPackage()
    {
        for(int i = 0; i < packageCount; i++)
        {
            for (int j = 0; j < cardShapeCount; j++)
            {
                for (int k = 0; k < cardCount; k++)
                {
                    // 카드 모양 갯수 미리해서 3패키지를 만들까?
                }
            }
        }
    }

    private void CreatePool()
    {
        for(int i = 0; i < deckCount; i++)
        {
            int randomCard = Random.Range(0, cardList.Count);
            GameObject card = Instantiate(cardList[randomCard], this.transform);
        }
    }

    public GameObject GetCard()
    {
        const int topIndex = 0;

        // 일단 null로 처리했는데 증설하는쪽이 좋아보임.
        if (transform.childCount <= 0)
        {
            CreatePool();
        }

        return transform.GetChild(topIndex).gameObject;
    }

    public void ReturnCard(GameObject card)
    {
        card.transform.SetParent(this.transform, false);
    }



    //public GameObject temp;
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        GameObject card = GetCard();
    //        card.transform.SetParent(temp.transform);
    //    }

    //    if (Input.GetKeyDown(KeyCode.H))
    //    {
    //        if (temp.transform.childCount > 0)
    //            ReturnCard(temp.transform.GetChild(0).gameObject);
    //        else
    //            Debug.Log("카드가 없습니다");
    //    }
    //}
}
