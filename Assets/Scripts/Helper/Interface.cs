using System.Collections;
using UnityEngine;

public interface I_SmoothMove
{
    public IEnumerator SmoothMove(Vector3 startPos, Vector3 target, float lerp_Speed);
}
