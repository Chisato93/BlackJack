using System.Collections;
using TMPro;
using UnityEngine;

public class JoinChip : MonoBehaviour
{
    Outline outline;
    PlayerSeat seat;
    const float lerp_Speed = 2f;

    private void Awake()
    {
        seat = transform.parent.GetComponent<PlayerSeat>();
        outline = GetComponent<Outline>();
        
        Init();
    }

    public void Init()
    {
        gameObject.SetActive(true);
        SetOutliner(true);
        transform.localPosition = Vector3.zero;
    }

    private void OnMouseEnter()
    {
        if(seat.isEmptySeat && GameController.instance.Flow == GameFlow.SELECT_SEAT)
            outline.OutlineColor = Color.green;
    }
    private void OnMouseExit()
    {
        if (outline.enabled && GameController.instance.Flow == GameFlow.SELECT_SEAT)
            outline.OutlineColor = Color.red;
    }
    private void OnMouseDown()
    {
        if (GameController.instance.Flow == GameFlow.SELECT_SEAT)
        {
            seat.isEmptySeat = false;
            seat.SetText(seat.isEmptySeat);
            SetOutliner(false);
        }
    }

    public void SetOutliner(bool isActive)
    {
        outline.enabled = isActive;
    }

    public void Ready()
    {
        this.gameObject.SetActive(false);
    }

}
