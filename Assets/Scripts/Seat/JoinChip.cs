using UnityEngine;

public class JoinChip : MonoBehaviour
{
    Outline outline;
    Seat seat;

    private void Awake()
    {
        seat = transform.parent.GetComponent<Seat>();
        outline = GetComponent<Outline>();
        
        Init();
    }

    private void Init()
    {
        outline.enabled = true;
    }

    private void OnMouseEnter()
    {
        if(seat.isEmptySeat)
            outline.OutlineColor = Color.green;
    }
    private void OnMouseExit()
    {
        if (outline.enabled)
            outline.OutlineColor = Color.red;
    }
    private void OnMouseDown()
    {
        seat.isEmptySeat = false;
        outline.enabled = false;
    }
}
