using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<GameObject> decks;
    void Start()
    {
        RandomActiveCardDeck();
    }

    private void RandomActiveCardDeck()
    {
        foreach (GameObject deck in decks)
        {
            deck.SetActive(false);
        }
        int rnd = Random.Range(0, decks.Count);
        decks[rnd].SetActive(true);
    }

}
