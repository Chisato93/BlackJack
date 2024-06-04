using UnityEngine;

public class DealerSeat : Seat
{
    public GameObject dummy_card;

    public void SetActiveDummyCard(bool isActive)
    {
        dummy_card.SetActive(isActive);
    }
}
