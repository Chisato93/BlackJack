using System.Collections.Generic;
using UnityEngine;

public class Seat : MonoBehaviour
{
    public bool isEmptySeat = false;
    public int Bet_Amount { get; set; }
    public List<Card> Card_Deck { get; set; }
    public int Card_Sum { get; set; }
    public GameResult Result {  get; set; }


}
