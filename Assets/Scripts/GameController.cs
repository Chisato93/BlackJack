using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameFlow Flow { get; set; }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Flow = GameFlow.SELECT_SEAT;
    }
}
