using System.Collections.Generic;
using UnityEngine;

public class CardPooling : MonoBehaviour
{
    public static CardPooling instance;

    public List<GameObject> cardList;
    const int deckCount = 10;

    private void Awake()
    {
        instance = this;

        CreatePool();
    }

    private void CreatePool()
    {
        for(int i = 0; i < cardList.Count; i++)
        {
            for(int j = 0; j < deckCount; j++)
            {
                GameObject card = Instantiate(cardList[i], this.transform);
                card.SetActive(false);
            }
        }
    }

    

}
