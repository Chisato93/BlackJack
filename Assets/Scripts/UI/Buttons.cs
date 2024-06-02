using UnityEngine;

public class Buttons : MonoBehaviour
{
    public void SitDone()
    {
        JoinChip[] seats = FindObjectsOfType<JoinChip>();
        foreach (JoinChip seat in seats)
        {
            seat.Ready();
            seat.SetOutliner(false);
        }
        UIController.instance.TurnOnReadyButton(false);

        GameController.instance.NextStep();
    }
}
