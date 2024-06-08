using System.Collections;
using TMPro;
using UnityEngine;

public class JoinChip : MonoBehaviour, I_SmoothMove
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

    private void Init()
    {
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
            SetOutliner(false);
        }
    }

    public void SetOutliner(bool isActive)
    {
        outline.enabled = isActive;
    }

    public void Ready()
    {
        Vector3 targetPosition = transform.localPosition - new Vector3(0, 0.015f, 0);
        StartCoroutine(SmoothMove(this.transform.localPosition, targetPosition, lerp_Speed));
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
