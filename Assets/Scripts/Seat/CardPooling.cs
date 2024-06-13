using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CardPooling : MonoBehaviour, I_SmoothMove
{
    public static CardPooling instance;

    List<Card> Card_Package = new List<Card>();

    public List<GameObject> cardList;
    const int minimumCardCount = 30;

    private void Awake()
    {
        CreateCardPackage();

        instance = this;
    }
    const int packageCount = 3;
    private void CreateCardPackage()
    {
        CardShuffle();

        for (int i = 0; i < packageCount; i++)
        {
            for(int j = 0; j < cardList.Count; j++)
            {
                GameObject card = Instantiate(cardList[j], this.transform);
            }
        }

    }

    private void CardShuffle()
    {
        // 새로운 리스트를 만들어서 전부 섞은 다음 다시 넣는다.
        List<GameObject> tempCardList = new List<GameObject>();
        while(cardList.Count > 0)
        {
            int rand = Random.Range(0, cardList.Count);
            tempCardList.Add(cardList[rand]);
            cardList.RemoveAt(rand);
        }

        cardList = tempCardList;
    }

    private GameObject GetCard()
    {
        const int topIndex = 0;

        return transform.GetChild(topIndex).gameObject;
    }

    public void ReturnCard(GameObject card)
    {
        card.transform.SetParent(this.transform, true);
        card.transform.position = this.transform.position;
        card.transform.rotation = Quaternion.identity;
    }

    [SerializeField] float lerpSpeed;
    public void Phase(Transform sit, int phase)
    {
        GameObject card = GetCard() as GameObject;
        card.transform.SetParent(sit);

        const float distX = 0.04f;
        const float distY = 0.01f;
        Vector3 cardDistance = new Vector3(distX * phase, distY*phase, 0);
        card.transform.localPosition = cardDistance;
        card.transform.localRotation = Quaternion.identity;
        StartCoroutine(SmoothMove(card.transform.localPosition, cardDistance, lerpSpeed));
    }

    public IEnumerator SmoothMove(Vector3 startPos, Vector3 target, float lerp_Speed)
    {
        while (Vector3.Distance(startPos, target) > 0.001f)
        {
            startPos = Vector3.Lerp(startPos, target, lerp_Speed * Time.deltaTime);
            yield return null;
        }

        startPos = target;
    }
}
