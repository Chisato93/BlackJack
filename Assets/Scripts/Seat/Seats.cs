using System.Collections.Generic;
using UnityEngine;

public class Seats : MonoBehaviour
{
    public List<Seat> seats;

    public bool AtLeastOneSeats()
    {
        foreach (PlayerSeat seat in seats)
        {
            if (!seat.isEmptySeat) 
                return true;
        }
        return false;
    }
}
