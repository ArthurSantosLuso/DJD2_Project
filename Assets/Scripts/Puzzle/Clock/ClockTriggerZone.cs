using UnityEngine;

public class ClockTriggerZone : MonoBehaviour
{
    [SerializeField] private ClockController clockController;
    [SerializeField] private string handId;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("ClockHand"))
            return;

        clockController.OnHandEnterZone(handId);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("ClockHand"))
            return;

        clockController.OnHandExitZone(handId);
    }
}
