using System.Collections.Generic;
using UnityEngine;

public class CardPooling : MonoBehaviour
{
    public static CardPooling instance;

    public List<GameObject> cardList;
    const int deckCount = 20;

    private void Awake()
    {
        instance = this;

        CreatePool();
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
