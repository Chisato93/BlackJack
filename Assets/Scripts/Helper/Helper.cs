using System.Collections;
using UnityEngine;

public class Helper : MonoBehaviour
{

    public static IEnumerator SmoothMove(Vector3 startPos, Vector3 target, float lerp_Speed)
    {
        while (Vector3.Distance(startPos, target) > 0.001f)
        {
            startPos = Vector3.Lerp(startPos, target, lerp_Speed * Time.deltaTime);
            yield return null;
        }

        startPos = target;
    }
}
